import React from 'react';
import ReactDOM from 'react-dom/client';
import { WrappedApp } from './App';
import { store } from './redux/store'
import { Experimental_CssVarsProvider as CssVarsProvider } from '@mui/material/styles';
import theme from './theme';
import { CssBaseline } from '@mui/material';
import { Provider } from 'react-redux';

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <Provider store={store}>
      <CssVarsProvider theme={theme}>
        <CssBaseline />
          <WrappedApp />
      </CssVarsProvider>
    </Provider>
  </React.StrictMode>,
)
