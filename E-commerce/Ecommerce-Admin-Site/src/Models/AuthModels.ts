interface LoginModel {
  username: string;
  password: string;
}

interface RegisterModel {
  username: string;
  password: string;
  email: string;
}

interface UserInfoModel {
  email: string;
  username: string;
  role: string;
  token: string;
}

interface UserModel {
  email: string;
  userName: string;
}

export type {LoginModel, UserInfoModel, UserModel, RegisterModel};