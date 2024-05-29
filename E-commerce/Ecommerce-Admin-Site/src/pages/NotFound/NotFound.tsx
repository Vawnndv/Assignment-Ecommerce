import { Link } from 'react-router-dom';
import { Typography, Button, Container, Box } from '@mui/material';

function NotFound() {
  return (
    <Container maxWidth="sm">
      <Box sx={{ textAlign: 'center', mt: 8 }}>
        <Typography variant="h1" component="h1" gutterBottom>
          Not Found
        </Typography>
        <Typography variant="body1" paragraph>
          Oops! The page you are looking for does not exist.
        </Typography>
        <Button component={Link} to="/" variant="contained" color="primary">
          Go Home
        </Button>
      </Box>
    </Container>
  );
}

export default NotFound;
