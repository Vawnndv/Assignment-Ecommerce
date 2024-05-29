import { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import ProductDetailsForm from './ProductDetailsForm';
import CategorySelection from './CategorySelection';
import ProductTypeSelection from './ProductTypeSelection';
import {
  getProductByIdService,
  createNewProductService,
  updateProductByIdService,
} from '../../../redux/services/productServices';
import { Product, ProductType } from '../../../Models/ProductModel';
import { Category } from '../../../Models/CategoryModels';
import { Container, Grid, Paper, Button } from '@mui/material';
import Title from '../../../components/Title';

const emptyProduct: Product = {
  id: 0, // Hoặc bất kỳ giá trị mặc định nào cho các thuộc tính
  name: '',
  description: '',
  price: 0,
  discount: 0,
  categoryId: 0,
  productTypes: [],
  ratings: [],
};

function isProductValid(product: Product): boolean {
  const isNameValid = product.name.trim() !== '';
  const isDescriptionValid = product.description.trim() !== '';
  const isPriceValid = product.price > 0;
  const isDiscountValid = product.discount >= 0 && product.discount <= 100;
  const isCategoryIdValid = product.categoryId !== 0;
  const areProductTypesValid = product.productTypes.length > 0 &&
    product.productTypes.every(type =>
      type.type.trim() !== '' &&
      type.description.trim() !== '' &&
      type.productImages.length > 0 &&
      type.productImages.every(image => image.imageUrl.trim() !== '')
    );

  return (
    isNameValid &&
    isDescriptionValid &&
    isPriceValid &&
    isDiscountValid &&
    isCategoryIdValid &&
    areProductTypesValid
  );
}

function ProductFormPage() {
  const navigate = useNavigate();
  const { productId } = useParams();
  const isEditing = !!productId;
  const [product, setProduct] = useState<Product>(emptyProduct);
  const [isEditMode, setIsEditMode] = useState<boolean>(!isEditing);
  const [isValid, setIsValid] = useState<boolean>(false);

  useEffect(() => {
    // If editing, fetch product data by ID
    if (isEditing && productId) {
      fetchProductData(productId);
    }
  }, [isEditing, productId]);

  useEffect(() => {
    if (isProductValid(product)) {
      setIsValid(true);
    } else {
      setIsValid(false);
    }
  }, [product]);

  const fetchProductData = async (id: string) => {
    try {
      const productData = await getProductByIdService(parseInt(id));
      setProduct(productData);
    } catch (error) {
      console.error('Error fetching product data:', error);
    }
  };

  const handleSubmit = async (formData: Partial<Product>) => {
    try {
      const updatedProduct = { ...product, ...formData } as Product;
      if (isEditing && productId && product) {
        // If editing, update existing product
        await updateProductByIdService(updatedProduct);
      } else {
        // If creating new, create a new product
        await createNewProductService(updatedProduct);
      }
      // Redirect to product management page after submission
      // You may replace this navigation logic with your own
      navigate('/products');
    } catch (error) {
      console.error('Error submitting product:', error);
      // Handle error
    }
  };

  const handleCategorySelect = (category: Category) => {
    setProduct({ ...product, categoryId: category.id });
  };

  const handleProductTypeSelect = (productTypes: ProductType[]) => {
    setProduct({ ...product, productTypes });
  };

  const toggleEditMode = () => {
    setIsEditMode((prevMode) => !prevMode);
  };

  return (
    <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
      <Grid container spacing={3}>
        {
          isEditing ?
          <Grid item xs={12}>
            <Button variant="contained" onClick={toggleEditMode}>
              {isEditMode ? 'Close Edit Mode' : 'Open Edit Mode'}
            </Button>
          </Grid> : null
        }
        <Grid item xs={12} md={8} lg={9}>
          <Paper
            sx={{
              p: 2,
              display: 'flex',
              flexDirection: 'column',
              height: 'auto',
            }}
          >
            <Title>{isEditing ? 'Edit Product' : 'Add New Product'}</Title>
            <ProductDetailsForm
              isValid={isValid}
              product={product}
              setProduct={setProduct}
              onSubmit={handleSubmit}
              isEditMode={isEditMode}
            />
          </Paper>
        </Grid>
        <Grid item xs={12} md={4} lg={3}>
          <Paper
            sx={{
              p: 2,
              display: 'flex',
              flexDirection: 'column',
              height: 'auto',
            }}
          >
            <Title>Category</Title>
            <CategorySelection
              categoryId={product.categoryId}
              onSelect={handleCategorySelect}
              isEditMode={isEditMode}
            />
          </Paper>
        </Grid>
        <Grid item xs={12}>
          <Paper sx={{ p: 2, display: 'flex', flexDirection: 'column' }}>
            <Title>Variant</Title>
            <ProductTypeSelection
              productTypes={product.productTypes}
              handleProductTypeChange={handleProductTypeSelect}
              isEditMode={isEditMode}
            />
          </Paper>
        </Grid>
      </Grid>
    </Container>
  );
}

export default ProductFormPage;
