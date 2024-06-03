import React from 'react';
import { TextField, Button, Grid, InputAdornment } from '@mui/material';
import { Product } from '../../../Models/ProductModel';

interface ProductDetailsFormProps {
  isLoading: boolean;
  product: Product;
  onSubmit: (data: Product) => void;
  setProduct: (data: Product | any) => void;
  isEditMode: boolean;
  isValid: boolean;
}

const ProductDetailsForm: React.FC<ProductDetailsFormProps> = ({ isLoading, product, setProduct, onSubmit, isEditMode, isValid }) => {
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setProduct((prevData: Product) => ({ ...prevData, [name]: value } as Product));
  };

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    onSubmit(product);
  };

  return (
    <form onSubmit={handleSubmit}>
      <Grid container spacing={2} alignItems="center">
        <Grid item xs={12}>
          <TextField
            name='name'
            label="Name"
            value={product.name || ''}
            fullWidth
            required
            disabled={!isEditMode}
            onChange={handleChange}
          />
        </Grid>
        <Grid item xs={12}>
          <TextField
            name='description'
            label="Description"
            value={product.description || ''}
            fullWidth
            multiline
            required
            disabled={!isEditMode}
            onChange={handleChange}
          />
        </Grid>
        <Grid item xs={6}>
          <TextField
            name='price'
            label="Price"
            type="number"
            value={product.price || ''}
            fullWidth
            required
            disabled={!isEditMode}
            onChange={handleChange}
            InputProps={{
              startAdornment: <InputAdornment position="start">$</InputAdornment>,
            }}
          />
        </Grid>
        <Grid item xs={6}>
          <TextField
            name='discount'
            label="Discount"
            type="number"
            value={product.discount || ''}
            fullWidth
            disabled={!isEditMode}
            onChange={handleChange}
            InputProps={{
              startAdornment: <InputAdornment position="start">%</InputAdornment>,
            }}
          />
        </Grid>
        {isEditMode && (
          <Grid item xs={12} sx={{ display: 'flex', justifyContent: 'flex-end' }}>
            <Button type="submit" variant="contained" color="primary" disabled={!isValid || isLoading}>
              {isLoading ? 'Wait a minute' : 'Save'}
            </Button>
          </Grid>
        )}
      </Grid>
    </form>
  );
};

export default ProductDetailsForm;