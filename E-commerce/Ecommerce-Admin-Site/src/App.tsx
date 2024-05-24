import { useState } from 'react';
import './App.css'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import Login from './pages/auth/Login/Login';
import Home from './pages/home/Home';

export function App() {
  const [rememberMe, setRememberMe] = useState(false);

  return (
    <Routes> 
      <Route path="/" element={<Home/>} />
      <Route path="/Login" element={<Login rememberMe={rememberMe} setRememberMe={setRememberMe} />} />
    </Routes>
  )
}

export function WrappedApp() {
  return (
    <BrowserRouter>
      <App />
    </BrowserRouter>
  );
}
