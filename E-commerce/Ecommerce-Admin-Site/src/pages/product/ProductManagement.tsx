import { useEffect, useState } from 'react';
import { Box, Button, CircularProgress, Container, Grid, Paper, Tooltip } from '@mui/material';
import Title from '../../components/Title';
import AddIcon from '@mui/icons-material/Add';
import { DataGrid, GridColDef, GridRowSelectionModel } from '@mui/x-data-grid';
import { deleteProductByIdService, getAllProductService } from '../../redux/services/productServices';
import { Product, ProductQuery } from '../../Models/ProductModel';
import { IconButton } from '@mui/material';
import ViewIcon from '@mui/icons-material/Visibility';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';

const query: ProductQuery = {
  PageSize: 1000,
};

function ProductManagement() {
  const navigate = useNavigate();
  const [isLoading, setIsLoading] = useState(false);
  const [products, setProducts] = useState<Product[]>([]);
  const [selectedRows, setSelectedRows] = useState<GridRowSelectionModel>([]); // State to store selected rows

  useEffect(() => {
    const fetchProducts = async () => {
      setIsLoading(true);
      try {
        const data = await getAllProductService(query);
        setProducts(data);
      } catch (error) {
        console.error('Error fetching products:', error);
      } finally {
        setIsLoading(false);
      }
    };

    fetchProducts();
  }, []);

  const handleAddProduct = () => {
    navigate('/products/new');
  };

  const handleViewProduct = (id: number) => {
    navigate(`/products/${id}/edit`);
  };

  const handleEditProduct = (id: number) => {
    navigate(`/products/${id}/edit`);
  };

  const handleDeleteProduct = async (id: number) => {
    try {
      await deleteProductByIdService(id);
      // Update the state to remove the deleted product
      setProducts(products.filter(product => product.id !== id));
      toast.success('Product deleted successfully');
    } catch (error) {
      console.error('Error deleting product:', error);
      toast.error('Error deleting product');
    }
  };

  const handleDeleteSelected = async () => {
    try {
      await Promise.all(selectedRows.map(id => deleteProductByIdService(parseInt(id.toString()))));

      // Update the state to remove the deleted products
      setProducts(products.filter(product => !selectedRows.includes(product.id.toString())));
      setSelectedRows([]);
      toast.success('Selected products deleted successfully');
    } catch (error) {
      console.error('Error deleting selected products:', error);
      toast.error('Error deleting selected products');
    }
  };

  const columns: GridColDef[] = [
    {
      field: 'name',
      headerName: 'Name',
      width: 300,
      renderCell: (params) => (
        <div style={{ display: 'flex', alignItems: 'center' }}>
          <img src={params.row.productTypes[0]?.productImages[0]?.imageUrl} alt={params.row.name} style={{ width: 50, height: 50, marginRight: 10 }} />
          {params.row.name}
        </div>
      ),
    },
    {
      field: 'description',
      headerName: 'Description',
      width: 400,
      renderCell: (params) => (
        <div style={{ whiteSpace: 'pre-wrap' }}>{params.row.description}</div>
      ),
    },
    {
      field: 'price',
      headerName: 'Price',
      width: 150,
      valueFormatter: (params) => `$${params.value.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`, // Format as currency with commas
    },
    {
      field: 'discount',
      headerName: 'Discount',
      width: 150,
      valueFormatter: (params) => `${params.value}%`, // Add percentage sign
    },
    {
      field: 'totalRating',
      headerName: 'Total Rating',
      width: 150,
      valueGetter: (params) => params.row.ratings.reduce((acc: number, rating: any) => acc + rating.rating, 0),
    },
    {
      field: 'categoryId',
      headerName: 'Category ID',
      width: 150,
      valueGetter: (params) => params.row.categoryId,
    },
    {
      field: 'options',
      headerName: 'Options',
      width: 150,
      renderCell: (params) => (
        <div>
          <IconButton onClick={() => handleViewProduct(params.row.id)}>
            <Tooltip title="View detail product">
              <ViewIcon />
            </Tooltip>
          </IconButton>
          <IconButton onClick={() => handleEditProduct(params.row.id)}>
            <Tooltip title="Edit product">
              <EditIcon />
            </Tooltip>
          </IconButton>
          <IconButton onClick={() => handleDeleteProduct(params.row.id)} color="secondary">
            <Tooltip title="Delete product">
              <DeleteIcon />
            </Tooltip>
          </IconButton>
        </div>
      ),
    },
  ];

  const isDeleteButtonDisabled = selectedRows.length === 0; // Disable delete button if no rows are selected

  return (
    <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
      <Grid container spacing={1}>
        <Grid item xs={12}>
          <Paper sx={{ p: 2, display: 'flex', flexDirection: 'column' }}>
            <Title>PRODUCT MANAGEMENT</Title>
            <Box sx={{ display: 'flex', justifyContent: 'flex-end', alignItems: 'center', my: 2 }}>
              <Button variant="contained" startIcon={<AddIcon />} onClick={handleAddProduct}>
                Add new product
              </Button>
              <Button
                variant="contained"
                startIcon={<DeleteIcon />}
                onClick={handleDeleteSelected}
                disabled={isDeleteButtonDisabled}
                sx={{ ml: 2 }}
              >
                Delete selected
              </Button>
            </Box>
            {isLoading ? (
              <CircularProgress />
            ) : (
              <div style={{ height: 'auto', width: '100%' }}>
                <DataGrid
                  rows={products}
                  columns={columns}
                  pageSizeOptions={[5, 10, 20, 50, 100, 500]}
                  autoHeight
                  getRowId={(row) => row.id.toString()}
                  checkboxSelection
                  onRowSelectionModelChange={(newRowSelectionModel) => {
                    setSelectedRows(newRowSelectionModel);
                  }}
                  initialState={{
                    pagination: {
                      paginationModel: { page: 0, pageSize: 20 },
                    },
                  }}
                />
              </div>
            )}
          </Paper>
        </Grid>
      </Grid>
    </Container>
  );
}

export default ProductManagement;

