import { Box, Button, CircularProgress, Container, Grid, Paper, Tooltip } from '@mui/material';
import Title from '../../components/Title';
import AddIcon from '@mui/icons-material/Add';
import { useEffect, useState } from 'react';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import { getAllProductService } from '../../redux/services/productServices';
import { Product, ProductQuery } from '../../Models/ProductModel';
import { IconButton } from '@mui/material';
import ViewIcon from '@mui/icons-material/Visibility';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';

const query: ProductQuery = {
  PageSize: 1000,
};

function ProductManagement() {
  const [isLoading, setIsLoading] = useState(false);
  const [products, setProducts] = useState<Product[]>([]);

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
    // Implement add product functionality
  };

  const handleViewProduct = (id: number) => {
    // Implement view product functionality
  };

  const handleEditProduct = (id: number) => {
    // Implement edit product functionality
  };

  const handleDeleteProduct = (id: number) => {
    // Implement delete product functionality
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

  return (
    <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
      <Grid container spacing={1}>
        <Grid item xs={12}>
          <Paper sx={{ p: 2, display: 'flex', flexDirection: 'column' }}>
            <Title>PRODUCT MANAGEMENT</Title>
            <Box sx={{ display: 'flex', justifyContent: 'flex-end', alignItems: 'center', my: 2 }}>
              <Button variant="outlined" startIcon={<AddIcon />} onClick={handleAddProduct}>
                Add new product
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
                  getRowId={(row) => row.id}
                  initialState={{
                    pagination: {
                      paginationModel: { page: 0, pageSize: 10 },
                    },
                  }}
                  checkboxSelection
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
