import * as yup from 'yup';

const CategoryValidation = yup.object().shape({
  name: yup.string().required('Name is required').max(30, 'Name must be at most 30 characters'),
  description: yup.string().required('Description is required').max(255, 'Description must be at most 255 characters'),
});

export {
  CategoryValidation
};
