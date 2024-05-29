import * as yup from 'yup';

// Login validation
const LoginValidation = yup.object().shape({
  username: yup.string().required('Username is required'),
  password: yup.string().required('Password is required')
});

// Register validation
const RegisterValidation = yup.object().shape({
  username: yup.string().required('Username is required'),
  password: yup.string().required('Password is required'),
  email: yup.string().email('Invalid email format').required('Email is required'),
});

export {
  LoginValidation,
  RegisterValidation
};