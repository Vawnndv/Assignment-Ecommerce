import React, { useEffect, useState } from 'react';
import { TextField, Button, Dialog, DialogTitle, DialogContent, DialogActions, Grid, IconButton } from '@mui/material';
import { ProductType, ProductImage } from '../../../Models/ProductModel';
import DeleteIcon from '@mui/icons-material/Delete';
import { imageDb } from '../../../FirebaseStorage/config';
import { toast } from 'react-toastify';
import { v4 as uuidv4 } from 'uuid';
import { ref, uploadBytes, getDownloadURL } from 'firebase/storage';

interface EditProductTypeModalProps {
  open: boolean;
  productType: ProductType | null;
  onClose: () => void;
  onSave: (productType: ProductType) => void;
}

const EditProductTypeModal: React.FC<EditProductTypeModalProps> = ({ open, productType, onClose, onSave }) => {
  const [type, setType] = useState('');
  const [description, setDescription] = useState('');
  const [productImages, setProductImages] = useState<ProductImage[]>([]);

  useEffect(() => {
    if (productType) {
      setType(productType.type);
      setDescription(productType.description);
      setProductImages(productType.productImages || []);
    } else {
      setType('');
      setDescription('');
      setProductImages([]);
    }
  }, [productType]);

  // Function to upload image to Firebase storage
  const handleImageUpload = async (e: React.ChangeEvent<HTMLInputElement>) => {
    const files = e.target.files;
    const uniqueId = uuidv4();
    
    if (files) {
      try {
        const uploadPromises = Array.from(files).map(async (file) => {
          const storageRef = ref(imageDb, `${uniqueId}_${file.name}`);
          await uploadBytes(storageRef, file);
          const imageUrl = await getDownloadURL(ref(imageDb, `${uniqueId}_${file.name}`));
          return {
            id: Math.floor(Math.random() * 10000000), // Random id, replace with actual logic if needed
            imageUrl: imageUrl,
            productTypeId: productType ? productType.id : 0,
          };
        });
  
        const uploadedImages = await Promise.all(uploadPromises);
        setProductImages([...productImages, ...uploadedImages]);
      } catch (error) {
        toast.error('Error uploading images');
        console.error('Error uploading images:', error);
      }
    }
  };

  const handleImageDelete = (imageId: number) => {
    setProductImages(productImages.filter(img => img.id !== imageId));
  };

  const handleSave = () => {
    if (productType) {
      onSave({
        ...productType,
        type,
        description,
        productImages,
      });
    } else {
      onSave({
        id: Math.floor(Math.random() * 10000000),
        type,
        description,
        productId: 0,
        productImages,
      });
    }
    setType('');
    setDescription('');
    setProductImages([]);
    onClose();
  };

  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle>{productType ? 'Edit Product Type' : 'Add Product Type'}</DialogTitle>
      <DialogContent>
        <TextField
          label="Type"
          name="type"
          value={type}
          onChange={(e) => setType(e.target.value)}
          fullWidth
          margin="normal"
          required
        />
        <TextField
          label="Description"
          name="description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          fullWidth
          margin="normal"
          required
        />
        <input
          accept="image/*"
          type="file"
          multiple
          onChange={handleImageUpload}
          style={{ display: 'none' }}
          id="product-images-upload"
        />
        <label htmlFor="product-images-upload">
          <Button sx={{my: 3}} variant="contained" color="primary" component="span">
            Upload Images
          </Button>
        </label>
        <Grid container spacing={2} sx={{ mt: 2 }}>
          {productImages.map(image => (
            <Grid item key={image.id}>
              <img src={image.imageUrl} alt="product" style={{ width: 100, height: 100 }} />
              <IconButton onClick={() => handleImageDelete(image.id)}>
                <DeleteIcon />
              </IconButton>
            </Grid>
          ))}
        </Grid>
      </DialogContent>
      <DialogActions>
        <Button variant="contained" color="primary" onClick={handleSave} disabled={!(type !== '' && description !== '' && productImages.length !== 0)}>
          Save
        </Button>
        <Button onClick={onClose} color="primary">
          Cancel
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default EditProductTypeModal;
