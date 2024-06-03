import React, { useEffect, useState } from 'react';
import { Box, Button, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, IconButton } from '@mui/material';
import { ProductType } from '../../../Models/ProductModel';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import EditProductTypeModal from './EditProductTypeModal';
import AddIcon from '@mui/icons-material/Add';

interface ProductTypeFormProps {
  deleteImages: (url: string) => void;
  productTypes: ProductType[];
  handleProductTypeChange: (types: ProductType[]) => void;
  isEditMode: boolean; // Adding isEditMode prop
}

const ProductTypeForm: React.FC<ProductTypeFormProps> = ({ deleteImages, productTypes, handleProductTypeChange, isEditMode }) => {
  const [types, setTypes] = useState<ProductType[]>(productTypes);
  const [editType, setEditType] = useState<ProductType | null>(null);
  const [isModalOpen, setIsModalOpen] = useState(false);

  useEffect(() => {
    if (productTypes) {
      setTypes(productTypes);
    }
  }, [productTypes]);

  const handleOpenModal = (type: ProductType | null) => {
    if (isEditMode) { // Check if in edit mode
      setEditType(type);
      setIsModalOpen(true);
    }
  };

  const handleCloseModal = () => {
    setEditType(null);
    setIsModalOpen(false);
  };

  const handleSaveType = (type: ProductType) => {
    const newTypes = editType
      ? types.map(t => t.id === type.id ? type : t)
      : [...types, type];
    setTypes(newTypes);
    handleProductTypeChange(newTypes);
  };

  const handleDeleteType = (id: number) => {
    const newTypes = types.filter(t => t.id !== id);
    setTypes(newTypes);
    handleProductTypeChange(newTypes);
  };

  return (
    <Box>
      <Box sx={{ display: 'flex', justifyContent: 'flex-end' }}>
        {isEditMode && ( // Render only if in edit mode
          <Button variant="contained" startIcon={<AddIcon />} color="primary" onClick={() => handleOpenModal(null)}>
            Add Product Type
          </Button>
        )}
      </Box>
      <TableContainer component={Paper} sx={{ mt: 2 }}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Image</TableCell>
              <TableCell>Type</TableCell>
              <TableCell>Description</TableCell>
              <TableCell>Options</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {types.map(type => (
              <TableRow key={type.id}>
                <TableCell>
                  <img src={type.productImages[0]?.imageUrl} alt={type.type} style={{ width: 50, height: 50 }} />
                </TableCell>
                <TableCell>{type.type}</TableCell>
                <TableCell>{type.description}</TableCell>
                <TableCell>
                  {isEditMode && ( // Render only if in edit mode
                    <>
                      <IconButton onClick={() => handleOpenModal(type)}>
                        <EditIcon />
                      </IconButton>
                      <IconButton onClick={() => handleDeleteType(type.id)}>
                        <DeleteIcon />
                      </IconButton>
                    </>
                  )}
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
      <EditProductTypeModal
        deleteImages={deleteImages}
        open={isModalOpen}
        productType={editType}
        onClose={handleCloseModal}
        onSave={handleSaveType}
      />
    </Box>
  );
};

export default ProductTypeForm;
