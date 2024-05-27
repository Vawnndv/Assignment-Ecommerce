import React, { useEffect } from 'react';
import { Dialog, DialogActions, DialogContent, DialogTitle, TextField, Button } from '@mui/material';
import { Category } from '../../Models/CategoryModels';
import { useForm } from 'react-hook-form';
import { CategoryValidation } from '../../validation/categoryValidation';
import { yupResolver } from '@hookform/resolvers/yup';

interface CategoryModalProps {
  open: boolean;
  onClose: () => void;
  onSave: (category: Category) => void;
  category?: Category | null;
}

type CategorySubmitForm = {
  name: string;
  description: string;
};

const CategoryModal: React.FC<CategoryModalProps> = ({ open, onClose, onSave, category }) => {
  const {
    register,
    handleSubmit,
    formState: { errors },
    reset,
  } = useForm<CategorySubmitForm>({
    resolver: yupResolver(CategoryValidation),
  });

  useEffect(() => {
    if (category) {
      reset({
        name: category.name,
        description: category.description,
      });
    } else {
      reset({
        name: '',
        description: '',
      });
    }
  }, [category, reset]);

  const handleSave = (data: CategorySubmitForm) => {
    const formData: Category = {
      ...category!,
      ...data,
      id: category ? category.id : 0, // Ensure id is always defined
    };
    onSave(formData);
    onClose();
    reset();
  };

  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle>{category ? 'Edit Category' : 'Add Category'}</DialogTitle>
      <form onSubmit={handleSubmit(handleSave)}>
        <DialogContent>
          <TextField
            margin="dense"
            label="Category Name"
            type="text"
            fullWidth
            variant="outlined"
            {...register('name')}
            error={!!errors.name}
            helperText={errors.name?.message}
          />
          <TextField
            margin="dense"
            label="Description"
            type="text"
            fullWidth
            variant="outlined"
            {...register('description')}
            error={!!errors.description}
            helperText={errors.description?.message}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={onClose} color="primary">
            Cancel
          </Button>
          <Button type="submit" color="primary">
            Save
          </Button>
        </DialogActions>
      </form>
    </Dialog>
  );
};

export default CategoryModal;
