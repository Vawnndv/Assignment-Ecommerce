import { useState, useEffect } from 'react';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import Link from '@mui/material/Link';
import Paper from '@mui/material/Paper';
import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import { ThemeProvider, createTheme } from '@mui/material';
import { useSelector } from 'react-redux';
import { useNavigate, Link as RouterLink } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import InputAdornment from '@mui/material/InputAdornment';
import Visibility from '@mui/icons-material/Visibility';
import VisibilityOff from '@mui/icons-material/VisibilityOff';
import { FiLogIn } from 'react-icons/fi';
import { LoginValidation } from '../../../validation/userValidation';
import { AppDispatch, RootState, useAppDispatch } from '../../../redux/store';
import { loginAction } from '../../../redux/actions/authActions';
import { LoginModel } from '../../../Models/AuthModels';
import Cookies from 'js-cookie';
import BackgroundImage from '../../../assets/images/Background.png';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const defaultTheme = createTheme({
  palette: {
    primary: {
      main: '#466874'
    },
    secondary: {
      main: '#f2f2f2'
    }
  }
})

type UserLoginSubmitForm = {
  username: string;
  password: string;
};

interface Props {
  rememberMe: boolean;
  setRememberMe: (value: boolean) => void;
}

function Login(props: Props) {
  const { rememberMe, setRememberMe } = props;

  const [showPassword, setShowPassword] = useState(false);

  const handleClickShowPassword = () => setShowPassword((show) => !show);
  const handleMouseDownPassword = (event: { preventDefault: () => void }) => {
    event.preventDefault();
  };

  const dispatch: AppDispatch = useAppDispatch();
  const navigate = useNavigate()

  const { isLoading, isError, userInfo, isSuccess } = useSelector(
    (state: RootState) => state.userLogin
  );
  
  // Validate user
  const {
    register,
    setValue,
    handleSubmit,
    formState: { errors }
  } = useForm<UserLoginSubmitForm>({ resolver: yupResolver(LoginValidation) });

  const onSubmit = (data: LoginModel) => {
    dispatch(loginAction(data))
  
    if (rememberMe) {
      Cookies.set('rememberedCheck', 'true', { expires: 7 });
      Cookies.set('rememberedCredentials', JSON.stringify({ username: data.username, password: data.password }), { expires: 7 });
    } else {
      Cookies.remove('rememberedCheck');
      Cookies.remove('rememberedCredentials');
    }
  };
  

  const handleRememberMeChange = () => {
    setRememberMe(!rememberMe);
  };

  useEffect(() => {
    const rememberedCheck = Cookies.get('rememberedCheck');
    const rememberedCredentialsString = Cookies.get('rememberedCredentials');

    const rememberedCredentials = rememberedCredentialsString ? JSON.parse(rememberedCredentialsString) : null;

    if (rememberMe) {
      setRememberMe(rememberedCheck === 'true');
      setValue('username', rememberedCredentials.username);
      setValue('password', rememberedCredentials.password);
    }
  }, []);

  useEffect(() => {
      if (userInfo) {
        navigate('/')
      }
      if (isSuccess) {
        toast.success(`Welcome back my friend!`)
      }
      if (isError) {
        toast.error(isError)
        dispatch({ type: 'USER_LOGIN_RESET' })
      }
  }, [userInfo, isSuccess, isError, navigate, dispatch])

  return (
    <ThemeProvider theme={defaultTheme}>
      <ToastContainer />
      <Grid container component="main" sx={{ 
        height: '100vh', 
        overflow: 'hidden',
        backgroundImage: `url(${BackgroundImage})`,
        backgroundRepeat: 'no-repeat',
        backgroundColor: (t) => (t.palette.mode === 'light' ? t.palette.grey[50] : t.palette.grey[900]),
        backgroundSize: 'cover',
      }}>
        <Grid
          item
          xs={false}
          sm={false}
          md={7}
        />
        <Grid
          xs={12}
          sm={12}
          md={5}
          item
          component={Paper}
          elevation={6}
          square
          sx={{
            display: 'flex',
            alignItems: 'center'
          }}
        >
          <Box
            sx={{
              my: 8,
              mx: 4,
              display: 'flex',
              flexDirection: 'column',
              alignItems: 'center',
              width: '100%'
            }}
          >
            <Typography
              component="h1"
              variant="h5"
              sx={{
                fontSize: '60px',
                fontWeight: 'bold',
                color: 'primary.main'
              }}
            >
              LOGIN
            </Typography>
            <Box component="form" noValidate onSubmit={handleSubmit(onSubmit)} sx={{ mt: 1, width: '100%' }}>
              <TextField
                margin="normal"
                required
                fullWidth
                id="username"
                label="Username"
                autoComplete="username"
                autoFocus
                {...register('username')}
                error={!!errors.username}
                helperText={errors.username?.message || ''}
              />
              <TextField
                margin="normal"
                required
                fullWidth
                label="Password"
                id="password"
                autoComplete="current-password"
                {...register('password')}
                error={!!errors.password}
                helperText={errors.password?.message || ''}
                type={showPassword ? 'text' : 'password'}
                InputProps={{
                  endAdornment: (
                    <InputAdornment position="end">
                      <IconButton
                        aria-label="toggle password visibility"
                        onClick={handleClickShowPassword}
                        onMouseDown={handleMouseDownPassword}
                        edge="end"
                      >
                        {showPassword ? <Visibility /> : <VisibilityOff />}
                      </IconButton>
                    </InputAdornment>
                  )
                }}
              />

              <FormControlLabel sx={{ display: 'flex', justifyContent: 'flex-start' }}
                control={<Checkbox onChange={handleRememberMeChange} checked={rememberMe} value="remember" color="primary" />}
                label="Remember"
              />
              <Button
                type="submit"
                fullWidth
                variant="contained"
                sx={{ mt: 3, mb: 2, p: 2 }}
                disabled={isLoading}
              >
                {isLoading ? (
                      'Loading...'
                  ) : (
                <>
                  <FiLogIn />
                  <Box style={{ marginLeft: '4px' }}>LOGIN</Box>
                </>
              )} 
              </Button>
              <Grid container sx={{ justifyContent: 'flex-end' }}>
                <Grid item>
                  <Link component={RouterLink} to="/register" variant="body2">
                    Do not have an account? Register
                  </Link>
                </Grid>
              </Grid>
            </Box>
          </Box>
        </Grid>
      </Grid>
    </ThemeProvider>
  );
}

export default Login;
