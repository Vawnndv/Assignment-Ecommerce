import * as yup from 'yup';

// Login validation
const LoginValidation = yup.object().shape({
  username: yup.string().required('Username is required'),
  password: yup.string().required('Password is required')
});

export {
  LoginValidation
};