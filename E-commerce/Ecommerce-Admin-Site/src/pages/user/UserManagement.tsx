import { useEffect, useState } from 'react';
import { Button, CircularProgress, Container, Grid, Paper, Tooltip, IconButton, Typography, Dialog, DialogTitle, DialogContent, DialogActions, TextField, Box } from '@mui/material';
import Title from '../../components/Title';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import { getAllUsersService, getAllAdminsService, createAdminAccountService } from '../../redux/services/authServices';
import ViewIcon from '@mui/icons-material/Visibility';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import { UserModel } from '../../Models/AuthModels';
import AddIcon from '@mui/icons-material/Add';
import { toast } from 'react-toastify';
import { yupResolver } from '@hookform/resolvers/yup';
import { RegisterValidation } from '../../validation/userValidation';
import { useForm, SubmitHandler } from 'react-hook-form';

type AdminRegisterSubmitForm = {
  username: string;
  password: string;
  email: string;
};

function AdminManagement() {
  const [isLoading, setIsLoading] = useState(false);
  const [users, setUsers] = useState<UserModel[]>([]);
  const [admins, setAdmins] = useState<UserModel[]>([]);
  const [selectedUser, setSelectedUser] = useState<UserModel | null>(null);
  const [open, setOpen] = useState(false);
  const [openCreateAdmin, setOpenCreateAdmin] = useState(false);

  useEffect(() => {
    const fetchUsers = async () => {
      setIsLoading(true);
      try {
        const usersData = await getAllUsersService();
        const adminsData = await getAllAdminsService();
        setUsers(usersData);
        setAdmins(adminsData);
      } catch (error) {
        console.error('Error fetching users:', error);
      } finally {
        setIsLoading(false);
      }
    };

    fetchUsers();
  }, []);

  const handleOpenModal = (user: UserModel) => {
    setSelectedUser(user);
    setOpen(true);
  };

  const handleCloseModal = () => {
    setOpen(false);
    setSelectedUser(null);
  };

  const handleEditUser = (id: number) => {
    console.log('Edit user with id:', id);
  };

  const handleDeleteUser = (id: number) => {
    console.log('Delete user with id:', id);
  };

  const handleCreateAdmin = () => {
    setOpenCreateAdmin(true);
  };

  const handleCloseCreateAdminModal = () => {
    setOpenCreateAdmin(false);
    reset();
  };

  // Validate user
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors }
  } = useForm<AdminRegisterSubmitForm>({ resolver: yupResolver(RegisterValidation) });

  const handleCreateAdminSubmit: SubmitHandler<AdminRegisterSubmitForm> = async (data) => {
    try {
      await createAdminAccountService(data);
      toast.success('Admin created successfully');

      setAdmins(prevAdmins => [...prevAdmins, {
        userName: data.username,
        email: data.email,
      }]);
      handleCloseCreateAdminModal();
    } catch (error) {
      console.error('Error creating admin:', error);
      toast.error('Error creating admin');
    }
  };

  const columns: GridColDef[] = [
    {
      field: 'userName',
      headerName: 'User Name',
      flex: 1,
    },
    {
      field: 'email',
      headerName: 'Email',
      flex: 2,
    },
    {
      field: 'options',
      headerName: 'Options',
      flex: 1,
      renderCell: (params) => (
        <div>
          <IconButton onClick={() => handleOpenModal(params.row)}>
            <Tooltip title="View user details">
              <ViewIcon />
            </Tooltip>
          </IconButton>
          <IconButton onClick={() => handleEditUser(params.row.id)} disabled>
            <Tooltip title="Edit user">
              <EditIcon />
            </Tooltip>
          </IconButton>
          <IconButton onClick={() => handleDeleteUser(params.row.id)} color="secondary" disabled>
            <Tooltip title="Delete user">
              <DeleteIcon />
            </Tooltip>
          </IconButton>
        </div>
      ),
    },
  ];

  const adminColumns: GridColDef[] = [
    {
      field: 'userName',
      headerName: 'Admin Name',
      flex: 1,
    },
    {
      field: 'email',
      headerName: 'Email',
      flex: 1,
    },
  ];

  return (
    <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
      <Grid container spacing={1}>
        <Grid item xs={12}>
          <Paper sx={{ p: 2, display: 'flex', flexDirection: 'column' }}>
            <Title>USER MANAGEMENT</Title>
            <Box sx={{ display: 'flex', justifyContent: 'flex-end', alignItems: 'center', my: 2 }}>
              <Button variant="contained" startIcon={<AddIcon />} onClick={handleCreateAdmin}>
                Create new admin
              </Button>
            </Box>
            {isLoading ? (
              <CircularProgress />
            ) : (
              <div style={{ height: 'auto', width: '100%' }}>
                <Typography variant="h6" gutterBottom>
                  Users
                </Typography>
                <DataGrid
                  rows={users}
                  columns={columns}
                  pageSizeOptions={[5, 10, 20, 50, 100]}
                  autoHeight
                  getRowId={(row) => row.userName}
                  initialState={{
                    pagination: {
                      paginationModel: { page: 0, pageSize: 20 },
                    },
                  }}
                />
                <Typography mt={10} variant="h6" gutterBottom>
                  Admins
                </Typography>
                <DataGrid
                  rows={admins}
                  columns={adminColumns}
                  pageSizeOptions={[5, 10, 20, 50, 100]}
                  autoHeight
                  getRowId={(row) => row.userName}
                  initialState={{
                    pagination: {
                      paginationModel: {
                        page: 0, pageSize: 20 },
                    },
                  }}
                />
              </div>
            )}
          </Paper>
        </Grid>
      </Grid>

      <Dialog
        open={open}
        onClose={handleCloseModal}
        aria-labelledby="user-modal-title"
        aria-describedby="user-modal-description"
      >
        <DialogTitle>User Details</DialogTitle>
        <DialogContent>
          <Typography id="user-modal-description" sx={{ mt: 2 }}>
            {selectedUser && (
              <>
                <p><strong>Username:</strong> {selectedUser.userName}</p>
                <p><strong>Email:</strong> {selectedUser.email}</p>
              </>
            )}
          </Typography>
        </DialogContent>
        <DialogActions>
          <Button color="primary" variant="contained" onClick={handleCloseModal}>Close</Button>
        </DialogActions>
      </Dialog>

      <Dialog
        open={openCreateAdmin}
        onClose={handleCloseCreateAdminModal}
        aria-labelledby="create-admin-modal-title"
        aria-describedby="create-admin-modal-description"
      >
        <DialogTitle>Create New Admin</DialogTitle>
        <DialogContent>
          <Box component="form" onSubmit={handleSubmit(handleCreateAdminSubmit)}>
            <TextField
              autoFocus
              margin="dense"
              label="Username"
              type="text"
              fullWidth
              {...register('username')}
              error={!!errors.username}
              helperText={errors.username ? errors.username.message : ''}
            />
            <TextField
              margin="dense"
              label="Email"
              type="email"
              fullWidth
              {...register('email')}
              error={!!errors.email}
              helperText={errors.email ? errors.email.message : ''}
            />
            <TextField
              margin="dense"
              label="Password"
              type="password"
              fullWidth
              {...register('password')}
              error={!!errors.password}
              helperText={errors.password ? errors.password.message : ''}
            />
            <DialogActions>
              <Button onClick={handleCloseCreateAdminModal} color="primary">
                Cancel
              </Button>
              <Button type="submit" color="primary" variant="contained">
                Create
              </Button>
            </DialogActions>
          </Box>
        </DialogContent>
      </Dialog>
    </Container>
  );
}

export default AdminManagement;
  