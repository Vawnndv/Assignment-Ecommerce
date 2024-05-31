import { useState, useEffect } from 'react';
import { Box, Button, CircularProgress, Container, Grid, Paper } from '@mui/material';
import Title from '../../components/Title';
import CategoryBreadcrumbs from './CategoryBreadcrumbs';
import AddIcon from '@mui/icons-material/Add';
import CategoryDetails from './CategoryDetails';
import CategoryModal from './CategoryModal';
import ConfirmDialog from '../../components/ConfirmDialog'; // Đảm bảo đường dẫn đúng tới file ConfirmDialog
import { Category } from '../../Models/CategoryModels';
import {
  getAllCategoryService,
  getCategoryByIdService,
  updateCategoryByIdService,
  createNewCategoryService,
  deleteCategoryByIdService
} from '../../redux/services/categoryServices';
import { useNavigate, useParams } from 'react-router-dom';
import { toast } from 'react-toastify';

const CategoryManagement = () => {
  const [categories, setCategories] = useState<Category[]>([]);
  const [selectedCategory, setSelectedCategory] = useState<Category | null>(null);
  const [currentCategory, setCurrentCategory] = useState<Category | null>(null);
  const [modalOpen, setModalOpen] = useState(false);
  const [isFetchData, setIsFetchData] = useState(true);
  const [isLoading, setIsLoading] = useState(false);
  const [confirmDialogOpen, setConfirmDialogOpen] = useState(false);
  const [categoryToDelete, setCategoryToDelete] = useState<number | null>(null);
  const { id } = useParams(); // Get the ID from route params

  const navigate = useNavigate();

  useEffect(() => {
    fetchCategories();
  }, [isFetchData]);

  useEffect(() => {
    if (id !== undefined) {
      // If ID is not null, fetch the category by ID
      fetchCategoryById(parseInt(id));
    }
  }, [id, isFetchData]);

  const fetchCategories = async () => {
    setIsLoading(true);
    try {
      const categoriesData = await getAllCategoryService();
      setCategories(categoriesData);
    } catch (error) {
      console.error('Error fetching categories:', error);
      toast.error('Error fetching categories');
    } finally {
      setIsLoading(false);
    }
  };

  const fetchCategoryById = async (categoryId: number) => {
    setIsLoading(true);
    try {
      const categoryData = await getCategoryByIdService(categoryId);
      setCurrentCategory(categoryData);
    } catch (error) {
      navigate('/categories')
      window.location.reload();
    } finally {
      setIsLoading(false);
    }
  };

  const handleEditCategory = async (categoryId: number) => {
    try {
      const categoryData = await getCategoryByIdService(categoryId);
      setSelectedCategory(categoryData);
      setModalOpen(true);
    } catch (error) {
      console.error('Error fetching category for editing:', error);
      toast.error('Error fetching category for editing');
    }
  };

  const handleDeleteCategory = async (categoryId: number) => {
    try {
      await deleteCategoryByIdService(categoryId);
      setIsFetchData(!isFetchData);
      toast.success('Category deleted successfully');
    } catch (error) {
      console.error('Error deleting category:', error);
      toast.error('Error deleting category');
    }
  };

  const handleAddCategory = () => {
    setSelectedCategory(null);
    setModalOpen(true);
  };

  const handleSaveCategory = async (category: Category) => {
    try {
      if (category.id) {
        await updateCategoryByIdService(category.id, category.name, category.description);
      } else {
        await createNewCategoryService(category.name, category.description, currentCategory?.id);
      }
      setIsFetchData(!isFetchData);
      toast.success('Category action successfully');
    } catch (error) {
      console.error('Error saving category:', error);
      toast.error('Error saving category');
    }
  };

  const handleOpenConfirmDialog = (categoryId: number) => {
    setCategoryToDelete(categoryId);
    setConfirmDialogOpen(true);
  };

  const handleConfirmDelete = () => {
    if (categoryToDelete !== null) {
      handleDeleteCategory(categoryToDelete);
    }
    setConfirmDialogOpen(false);
  };

  return (
    <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
      <Grid container spacing={1}>
        <Grid item xs={12}>
          <Paper sx={{ p: 2, display: 'flex', flexDirection: 'column' }}>
            <Title>CATEGORY MANAGEMENT</Title>
            <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', my: 2 }}>
              <CategoryBreadcrumbs categoryId={id ? parseInt(id) : null} />
              <Button variant="contained" startIcon={<AddIcon />} onClick={handleAddCategory}>
                Add new category
              </Button>
            </Box>
            {isLoading ? (
              <Box sx={{ display: 'flex', justifyContent: 'center' }}>
                <CircularProgress />
              </Box>
            ) : (
              id !== null ? ( // If ID is not null, show category details
                <CategoryDetails
                  category={currentCategory ?? null}
                  onEditCategory={handleEditCategory}
                  onDeleteCategory={handleOpenConfirmDialog}
                  categories={currentCategory == null ? categories : currentCategory.subCategories }
                />
              ) : null
            )}
          </Paper>
        </Grid>
      </Grid>
      <CategoryModal
        open={modalOpen}
        onClose={() => setModalOpen(false)}
        onSave={handleSaveCategory}
        category={selectedCategory}
      />
      <ConfirmDialog
        open={confirmDialogOpen}
        title="Confirm Delete"
        content="Are you sure you want to delete this category?"
        onClose={() => setConfirmDialogOpen(false)}
        onConfirm={handleConfirmDelete}
      />
    </Container>
  );
};

export default CategoryManagement;
