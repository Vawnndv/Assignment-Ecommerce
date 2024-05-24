import { LoginModel } from '../../Models/AuthModels';
import Axios from '../APIs/Axios';
import Cookies from 'js-cookie';

// Login user API
const loginService = async (user: LoginModel): Promise<any> => {
  const data = await Axios.post('/account/login', {...user});
  if (data) {
    // Save userInfo in cookie
    Cookies.set('userInfo', JSON.stringify(data), { expires: 1 / 1440 }); // Expire Set temp in 1"
  }

  return data;
};

// Forgot password API
const forgotPasswordService = async (email: string) => {
    const data = await Axios.post('/auth/forgotPassword', {email})
    return data;
}

const logoutService = async (): Promise<any> => {
  await Axios.post('/auth/logout', {}, { withCredentials: true });
};

export { loginService, logoutService, forgotPasswordService };
