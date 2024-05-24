import { ThunkAction } from 'redux-thunk';
import { RootState } from '../store';
import * as authConstants from '../constants/authConstants';
import { loginService } from '../services/authServices';
import { Action } from 'redux';
import { ErrorsAction } from '../protection';
import { LoginModel } from '../../Models/AuthModels';
import Cookies from 'js-cookie';

const loginAction = (data: LoginModel): ThunkAction<void, RootState, unknown, Action<string>> => async dispatch => {
  try {
    dispatch({ type: authConstants.USER_LOGIN_REQUEST });
    const response = await loginService(data);
    dispatch({
      type: authConstants.USER_LOGIN_SUCCESS,
      payload: response
    });
  } catch (error) {
    console.log('ERROR', error)
    ErrorsAction(error, dispatch, authConstants.USER_LOGIN_FAIL);
  }
};

const logoutAction = (): ThunkAction<void, RootState, unknown, Action<string>> => async dispatch => {
  // await authApi.logoutService()
  dispatch({ type: authConstants.USER_LOGOUT })
  Cookies.remove('userInfo');
}

export {
  loginAction,
  logoutAction,
}