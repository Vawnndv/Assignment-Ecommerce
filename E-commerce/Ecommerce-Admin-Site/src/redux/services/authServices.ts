import { LoginModel, RegisterModel } from '../../Models/AuthModels';
import Axios from '../APIs/Axios';
import Cookies from 'js-cookie';

// Login user API
const loginService = async (user: LoginModel): Promise<any> => {
  const data = await Axios.post('/account/login', {...user});
  if (data) {
    // Save userInfo in cookie
    Cookies.set('userInfo', JSON.stringify(data), { expires: 1 }); // Expire Set temp in 1 day
  }

  return data;
};

const getAllUsersService = async (): Promise<any> => {
  const data = await Axios.get('/account/users-in-role/user');
  return data;
};

const getAllAdminsService = async (): Promise<any> => {
  const data = await Axios.get('/account/users-in-role/admin');
  return data;
};

const createAdminAccountService = async (account: RegisterModel): Promise<any> => {
  const data = await Axios.post('/account/register-admin', {...account});
  return data;
};

// Forgot password API
const forgotPasswordService = async (email: string) => {
    const data = await Axios.post('/account/forgotPassword', {email})
    return data;
}

const logoutService = async (): Promise<any> => {
  await Axios.post('/account/logout', {}, { withCredentials: true });
};

export {
  loginService,
  logoutService,
  forgotPasswordService,
  getAllUsersService,
  getAllAdminsService, 
  createAdminAccountService
};
