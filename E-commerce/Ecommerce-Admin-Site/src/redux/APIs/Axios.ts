import axios from 'axios';
import Cookies from 'js-cookie';

const Axios = axios.create({
  baseURL: process.env.BASE_URL,
  headers: {
    'Content-Type': 'application/json',
    Accept: 'application/json',
  },
});

Axios.interceptors.request.use(
  async (config) => {
    const userInfoString = Cookies.get('userInfo');

    if (userInfoString) {
      try {
        const userInfo = JSON.parse(userInfoString);

        config.headers.Authorization = `Bearer ${userInfo.token}`;
      } catch (error) {
        console.error('Error parsing userInfo from cookie:', error);
      }
    }

    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

Axios.interceptors.response.use(
  (response) => {
    return response.data;
  },
  (error) => {
    if (error.response && error.response.data && error.response.data.message) {
      throw new Error(error.response.data.message);
    } else {
      throw new Error('Network Error');
    }
  }
);

export default Axios;
