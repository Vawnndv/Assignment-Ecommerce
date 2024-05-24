interface LoginModel {
  username: string;
  password: string;
}

interface UserInfoModel {
  email: string;
  username: string;
  token: string;
}

export type {LoginModel, UserInfoModel};