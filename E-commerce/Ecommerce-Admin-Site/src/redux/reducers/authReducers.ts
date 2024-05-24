import { UserInfoModel } from '../../Models/AuthModels';
import * as authConstants from '../constants/authConstants';

interface AuthState {
  isLoading?: boolean;
  isSuccess?: boolean;
  isError?: any;
  userInfo?: UserInfoModel;
}

export const userLoginReducer = (state: AuthState = {}, action: { type: string; payload?: any }): AuthState => {
  switch (action.type) {
    case authConstants.USER_LOGIN_REQUEST:
      return { isLoading: true };
    case authConstants.USER_LOGIN_SUCCESS:
      return { isLoading: false, userInfo: action.payload, isSuccess: true };
    case authConstants.USER_LOGIN_FAIL:
      return { isLoading: false, isError: action.payload };
    case authConstants.USER_LOGIN_RESET:
      return {};
    case authConstants.USER_LOGOUT:
      return {};
    default:
      return state;
  }
};
