import { combineReducers, configureStore } from '@reduxjs/toolkit';
import { useDispatch } from 'react-redux';
import * as Auth from './reducers/authReducers';
import Cookies from 'js-cookie';

const rootReducer = combineReducers({
  // User reducer
  userLogin: Auth.userLoginReducer,

});

const userInfoString = Cookies.get('userInfo');

let userInfoFromStorage = null;

if (userInfoString) {
  try {
    userInfoFromStorage = JSON.parse(userInfoString);
  } catch (error) {
    console.error('Error parsing userInfo from cookie:', error);
    userInfoFromStorage = null;
  }
}

// initialState
const initialState = {
  userLogin: { userInfo: userInfoFromStorage  }
};

export const store = configureStore({
  reducer: rootReducer,
  preloadedState: initialState
});

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch
export const useAppDispatch = () => useDispatch<AppDispatch>()