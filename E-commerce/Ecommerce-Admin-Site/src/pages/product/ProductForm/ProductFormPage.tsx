import { useState, useEffect, useCallback } from 'react';
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
import { imageDb } from '../../../FirebaseStorage/config';
import {
  ref,
  uploadBytes,
  getDownloadURL,
  deleteObject,
} from "firebase/storage";
import { v4 as uuidv4 } from 'uuid';

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

const handleImageUpload = async (base64Image: string): Promise<string> => {
  const uniqueId = uuidv4();
  const response = await fetch(base64Image);
  const blob = await response.blob();
  const storageRef = ref(imageDb, `${uniqueId}.png`); // Adjust the file extension as needed
  await uploadBytes(storageRef, blob);
  const imageUrl = await getDownloadURL(storageRef);
  return imageUrl;
};

const deleteImage = async (imageUrl: string) => {
  const imageRef = ref(imageDb, imageUrl);
  try {
    await deleteObject(imageRef);
  } catch (error) {
    console.error('Error deleting image:', error);
  }
};

function ProductFormPage() {
  const navigate = useNavigate();
  const { productId } = useParams();
  const isEditing = !!productId;
  const [product, setProduct] = useState<Product>(emptyProduct);
  const [isEditMode, setIsEditMode] = useState<boolean>(!isEditing);
  const [isValid, setIsValid] = useState<boolean>(false);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [imagesDelete, setImagesDelete] = useState<string[]>([]);

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

  const deleteImages = useCallback((url: string) => {
    setImagesDelete((prevImagesDelete) => [...prevImagesDelete, url]);
  }, []);

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
      setIsLoading(true)

      // Check and upload base64 images
      for (const productType of updatedProduct.productTypes) {
        for (const productImage of productType.productImages) {
          if (productImage.imageUrl.startsWith('data:image/')) {
            // Upload base64 image to Firebase and replace with the URL
            productImage.imageUrl = await handleImageUpload(productImage.imageUrl);
          }
        }
      }

      // Delete old images not referenced in the updated product
      for (const imageUrl of imagesDelete) {
        await deleteImage(imageUrl);
      }

      if (isEditing && productId && product) {
        // If editing, update existing product
        await updateProductByIdService(updatedProduct);
      } else {
        // If creating new, create a new product
        await createNewProductService(updatedProduct);
      }
      setIsLoading(false);
      // Redirect to product management page after submission
      navigate('/products');
    } catch (error) {
      console.error('Error submitting product:', error);
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
              isLoading={isLoading}
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
              deleteImages={deleteImages}
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
