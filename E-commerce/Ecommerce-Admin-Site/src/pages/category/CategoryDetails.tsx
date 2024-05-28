import React from 'react';
import { Link } from 'react-router-dom';
import { Box, Typography, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, IconButton, Tooltip } from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import ViewIcon from '@mui/icons-material/Visibility';
import { Category } from '../../Models/CategoryModels';

interface CategoryDetailsProps {
  category: Category | null;
  onEditCategory: (id: number) => void;
  onDeleteCategory: (id: number) => void;
  categories: Category[];
}

const CategoryDetails: React.FC<CategoryDetailsProps> = ({ category, onEditCategory, onDeleteCategory, categories}) => {
  return (
    <Box sx={{ mt: 4 }}>
      {
        category &&
        <>
          <Box sx={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between' }}>
            <Typography variant="h6" gutterBottom>
              CATEGORY DETAIL
            </Typography>
            <Box>
              <IconButton onClick={() => onEditCategory(category.id)} color="primary">
                <Tooltip title="Edit Category">
                  <EditIcon />
                </Tooltip>
              </IconButton>
              <IconButton
                onClick={() => onDeleteCategory(category.id)}
                color="secondary"
                disabled={categories.length > 0}
              >
                <Tooltip title="Delete Category">
                  <DeleteIcon />
                </Tooltip>
              </IconButton>
            </Box>
          </Box>
          <Box sx={{ mb: 2 }}>
            <Typography variant="body1"><strong>ID:</strong> {category.id}</Typography>
            <Typography variant="body1"><strong>Name:</strong> {category.name}</Typography>
            <Typography variant="body1"><strong>Description:</strong> {category.description}</Typography>
            <Typography variant="body1"><strong>Parent Category ID:</strong> {category.parentCategoryId ? category.parentCategoryId : 'None'}</Typography>
          </Box>
        </>
      }
      <Typography variant="h6" gutterBottom>
        SUB-CATEGORIES
      </Typography>
      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>ID</TableCell>
              <TableCell>Name</TableCell>
              <TableCell>Description</TableCell>
              <TableCell>Actions</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {categories.length === 0 ? (
              <TableRow>
                <TableCell colSpan={4}>No subcategories available</TableCell>
              </TableRow>
            ) : (
              categories.map((subCategory) => (
                <TableRow key={subCategory.id}>
                  <TableCell>{subCategory.id}</TableCell>
                  <TableCell>{subCategory.name}</TableCell>
                  <TableCell>{subCategory.description}</TableCell>
                  <TableCell>
                    <IconButton onClick={() => onEditCategory(subCategory.id)}>
                      <Tooltip title="Edit Subcategory">
                        <EditIcon />
                      </Tooltip>
                    </IconButton>
                    <IconButton onClick={() => onDeleteCategory(subCategory.id)} color="secondary">
                      <Tooltip title="Delete Subcategory">
                        <DeleteIcon />
                      </Tooltip>
                    </IconButton>
                    <IconButton component={Link} to={`/categories/${subCategory.id}`}>
                      <Tooltip title="View Subcategory">
                        <ViewIcon />
                      </Tooltip>
                    </IconButton>
                  </TableCell>
                </TableRow>
            ))
)}
          </TableBody>
        </Table>
      </TableContainer>
    </Box>
  );
};

export default CategoryDetails;
