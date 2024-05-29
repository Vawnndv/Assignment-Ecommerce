import * as yup from 'yup';

const ProductInfomationValidation = yup.object().shape({
  name: yup.string().required('Name is required').max(30, 'Name must be at most 30 characters'),
  description: yup.string().required('Description is required').max(100, 'Description must be at most 100 characters'),
  price: yup.number().required('Price is required'),
  discount: yup.number().required('Discount is required'),
});

const ProductCategoryValidation = yup.object().shape({
  category: yup.number().required('Category is required'),
});

const ProductTypeValidation = yup.object().shape({
  name: yup.string().required('Name is required').max(30, 'Type of product cannot be over 30 over characters'),
  description: yup.string().required('Description is required').max(100, 'Description of product type cannot be over 100 over characters'),
});

export {
  ProductInfomationValidation,
  ProductCategoryValidation,
  ProductTypeValidation
};
