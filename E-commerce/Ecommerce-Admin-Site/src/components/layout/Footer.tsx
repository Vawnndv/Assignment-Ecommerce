import Box from '@mui/material/Box'
import Typography from '@mui/material/Typography'
import Grid from '@mui/material/Grid'
import IconButton from '@mui/material/IconButton'
import FacebookIcon from '@mui/icons-material/Facebook'
import LinkedInIcon from '@mui/icons-material/LinkedIn'
import InstagramIcon from '@mui/icons-material/Instagram'
import { Stack } from '@mui/material'
import AppBar from '@mui/material/AppBar'

function Footer() {

  return (
    <Stack
      component="footer"
      id='footer'
    >
      <AppBar position='static'>
        {/* Part 1: Name */}
        <Typography component="h1" sx={{ fontSize: '50px', textAlign: 'center' }}
          className='text'>
          E-commerce
        </Typography>

        <Grid container spacing={2} sx={{ px: 4, py: 2, mx: 9 }}>
          <Grid item xs={12} sm={6}>
            <Box display="flex" flexDirection="column" alignItems="flex-start">
              <Stack>
                <Typography variant='body1' sx={{ fontSize: '20px', fontWeight: 'bold' }}>
              Contact us
                </Typography>
                <Typography variant='body1' sx={{ fontSize: '20px',
                  wordWrap: 'break-word'
                }}>
              Email: 
                </Typography>
              </Stack>

              
            </Box>

          </Grid>
          <Grid item xs={12} sm={6} >
            <Stack>
              <Typography variant='body1' sx={{ fontSize: '20px', fontWeight: 'bold' }}>
              About us
              </Typography>
            </Stack>
          </Grid>
        </Grid>

        <Box sx={{ display: 'flex', justifyContent: 'center', mt: 2 }}>
          <IconButton color="inherit" aria-label="facebook">
            <FacebookIcon />
          </IconButton>
          <IconButton color="inherit" aria-label="linkedin">
            <LinkedInIcon />
          </IconButton>
          <IconButton color="inherit" aria-label="instagram">
            <InstagramIcon />
          </IconButton>
        </Box>
      </AppBar>
    </Stack>
  )
}

export default Footer
