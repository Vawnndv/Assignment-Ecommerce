import MuiAppBar, { AppBarProps as MuiAppBarProps } from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import IconButton from '@mui/material/IconButton';
import Badge from '@mui/material/Badge';
import MenuIcon from '@mui/icons-material/Menu';
import NotificationsIcon from '@mui/icons-material/Notifications';
import { Avatar, Box, Menu, MenuItem, Typography, styled } from '@mui/material';
import Logo from '../../assets/images/Logo.png'
import { useNavigate } from 'react-router-dom';
import { useState } from 'react';
import { RiLogoutCircleLine } from 'react-icons/ri'
import { AppDispatch, useAppDispatch } from '../../redux/store'
import { logoutAction } from '../../redux/actions/authActions'
import { toast } from 'react-toastify'
import 'react-toastify/dist/ReactToastify.css'

const drawerWidth: number = 240;

interface AppBarProps extends MuiAppBarProps {
  open?: boolean;
}

const AppBar = styled(MuiAppBar, {
  shouldForwardProp: (prop) => prop !== 'open',
})<AppBarProps>(({ theme, open }) => ({
  zIndex: theme.zIndex.drawer + 1,
  transition: theme.transitions.create(['width', 'margin'], {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.leavingScreen,
  }),
  ...(open && {
    marginLeft: drawerWidth,
    width: `calc(100% - ${drawerWidth}px)`,
    transition: theme.transitions.create(['width', 'margin'], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
  }),
}));

export default function Header({open, toggleDrawer} :any) {
  const [anchorEl, setAnchorEl] = useState(null)

  const isMenuOpen = Boolean(anchorEl)
  
  const MyDispatch: AppDispatch = useAppDispatch();
  const navigate = useNavigate();

  const handleNavigation = (path: string) => {
    navigate(path);
  };

  const handleProfileMenuOpen = (event: any) => {
    setAnchorEl(event.currentTarget)
  }

  const handleMenuClose = () => {
    setAnchorEl(null)
  }

  const logoutHandler = () => {
    MyDispatch(logoutAction())
    toast.success('Logged out successfully')
    navigate('/login')
  }

  const menuId = 'primary-search-account-menu'
  const renderMenu = (
    <Menu
      anchorEl={anchorEl}
      anchorOrigin={{
        vertical: 'top',
        horizontal: 'right'
      }}
      id={menuId}
      keepMounted
      transformOrigin={{
        vertical: 'top',
        horizontal: 'right'
      }}
      open={isMenuOpen}
      onClose={handleMenuClose}
    >
      <MenuItem
        onClick={logoutHandler}
        style={{
          textDecoration: 'none',
          display: 'flex',
          alignItems: 'center'
        }}
      >
        <RiLogoutCircleLine />
        <Typography style={{ marginLeft: '4px' }}>
          Logout
        </Typography>
      </MenuItem>
    </Menu>
  )

  return (
    <>
      <AppBar position="absolute" open={open}>
        <Toolbar
          sx={{
            pr: '24px', // keep right padding when drawer closed
          }}
        >
          <IconButton
            edge="start"
            color="inherit"
            aria-label="open drawer"
            onClick={() => toggleDrawer()}
            sx={{
              marginRight: '36px',
              ...(open && { display: 'none' }),
            }}
          >
            <MenuIcon />
          </IconButton>

          <Box sx={{ display: 'flex', flexGrow: 1 , justifyContent: 'space-between' }}>
            <IconButton
              component="div"
              sx={{ flexFlow: 1}}
              onClick={() => handleNavigation('/')}
            >
              <img alt='logo' src={Logo} style={{height: '60px', color: 'white'}}/>
            </IconButton>
            
            <Box>
              <IconButton
                size="large"
                edge="end"
                aria-label="account of current user"
                aria-controls={menuId}
                aria-haspopup="true"
                onClick={(e) => handleProfileMenuOpen(e)}
                color="inherit"
              >
                <Avatar alt="User" src="https://media.istockphoto.com/id/1255734833/vi/anh/c%E1%BA%ADn-c%E1%BA%A3nh-ch%C3%A0ng-trai-tr%E1%BA%BB-m%E1%BB%89m-c%C6%B0%E1%BB%9Di-%C4%91%E1%BA%B9p-trai-m%E1%BA%B7c-%C3%A1o-s%C6%A1-mi-x%C3%A1m-c%E1%BA%A3m-th%E1%BA%A5y-l%E1%BA%A1c-quan-v%C3%A0-t%E1%BB%B1-tin-b%E1%BB%8B-c%C3%B4.jpg?s=612x612&w=0&k=20&c=U1h6IrwLUdHkvT9u7GXn6xSRYJq-WFCGzmWOtNL4Tn4=" style={{marginRight: "20px"}}/>
              </IconButton>

              <IconButton color="inherit">
                <Badge badgeContent={4} color="secondary">
                  <NotificationsIcon />
                </Badge>
              </IconButton>
            </Box>
          </Box>
        </Toolbar>
      </AppBar>
      {renderMenu}
    </>
  )
}