import { Box, Button } from '@mui/material';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

function Home() {
  const showToast = () => {
    toast.success('Hello from toast!');
  };

  return (
    <Box>
      <Button onClick={showToast}>Show Toast</Button>
      <ToastContainer />
    </Box>
  );
}

export default Home;
