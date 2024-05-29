import React, { useState, useEffect } from 'react';
import {
  Select,
  MenuItem,
  InputLabel,
  FormControl,
  SelectChangeEvent,
  ListSubheader,
} from '@mui/material';
import { getAllCategoryService } from '../../../redux/services/categoryServices';
import { Category } from '../../../Models/CategoryModels';

interface CategorySelectionProps {
  categoryId: number | null;
  onSelect: (category: Category) => void;
  isEditMode: boolean; // Adding isEditMode prop
}

const CategorySelection: React.FC<CategorySelectionProps> = ({ categoryId, onSelect, isEditMode }) => {
  const [categories, setCategories] = useState<Category[]>([]);

  useEffect(() => {
    const fetchCategories = async () => {
      try {
        const data = await getAllCategoryService();
        setCategories(data);
      } catch (error) {
        console.error('Error fetching categories:', error);
      }
    };

    fetchCategories();
  }, []);

  const handleChange = (e: SelectChangeEvent<number>) => {
    const categoryID = Number(e.target.value);
    const allCategories = categories.flatMap((cat) => [cat, ...cat.subCategories]);
    const selectedCategory = allCategories.find((cat) => cat.id === categoryID);
    if (selectedCategory) {
      onSelect(selectedCategory);
    }
  };

  const renderMenuItems = () => {
    const items: JSX.Element[] = [];
    categories.forEach((category) => {
      items.push(
        <ListSubheader key={category.id}>{category.name}</ListSubheader>
      );
      items.push(
        <MenuItem key={`category-${category.id}`} value={category.id} style={{ paddingLeft: 16 }}>
          {category.name}
        </MenuItem>
      );
      category.subCategories.forEach((subCategory) => {
        items.push(
          <MenuItem key={`subcategory-${subCategory.id}`} value={subCategory.id} style={{ paddingLeft: 32 }}>
            {subCategory.name}
          </MenuItem>
        );
      });
    });
    return items;
  };

  return (
    <FormControl fullWidth>
      <InputLabel>Select Category</InputLabel>
      <Select
        required
        value={categoryId !== null ? categoryId : ''}
        onChange={handleChange}
        label="Select Category"
        disabled={!isEditMode} // Disable the Select component based on isEditMode
      >
        {renderMenuItems()}
      </Select>
    </FormControl>
  );
};

export default CategorySelection;
