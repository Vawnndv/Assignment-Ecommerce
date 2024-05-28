import { useState } from 'react';
import './App.css'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import Login from './pages/auth/Login/Login';
import Home from './pages/home/Home';
import Layout from './components/layout/Layout';
import CategoryManagement from './pages/category/CategoryManagement';
import ProductManagement from './pages/product/ProductManagement';
import UserManagement from './pages/user/UserManagement';
import ProductDetails from './pages/product/ProductDetails';

export function App() {
  const [rememberMe, setRememberMe] = useState(false);

  return (
    <Routes> 
      <Route path="/Login" element={<Login rememberMe={rememberMe} setRememberMe={setRememberMe} />} />
      <Route path='/' element={<Layout/>}>
        <Route path="/" element={<Home/>} />
        <Route path="/categories/:id" element={<CategoryManagement />} />
        <Route path="/categories" element={<CategoryManagement />} />
        <Route path="/products" element={<ProductManagement/>} />
        <Route path="/products/:id" element={<ProductDetails/>} />
        <Route path="/users" element={<UserManagement/>} />
      </Route>
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
