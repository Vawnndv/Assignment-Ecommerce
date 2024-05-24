import { blueGrey, cyan, deepOrange, orange } from '@mui/material/colors'
import { experimental_extendTheme as extendTheme } from '@mui/material/styles'


declare module '@mui/material/styles' {
  interface PaletteOptions {
    customColor?: {
      main: string;
      muted: string;
      status: string;
    };
  }
}

const theme = extendTheme({
  colorSchemes: {
    light: {
      palette: {
        primary: {
          main: '#212121', // Main color
        },
        secondary: deepOrange,
        customColor: {
          main: '#321357', // Custom color
          muted: 'rgba(50, 19, 87, 0.1)',
          status: '#41B06E',
        },
      }
    },
    dark: {
      palette: {
        primary: cyan,
        secondary: orange,
        background: {
          paper: blueGrey[800],
          default: blueGrey[800]
        }
      }
    }
  }
  // ...other properties
})

export default theme