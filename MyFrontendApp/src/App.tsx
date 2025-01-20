import styles from './styles/Global.scss';

import { useState, useEffect } from 'react';
import { BrowserRouter, Navigate, HashRouter, Router, Route, Routes, Link} from 'react-router-dom';

import axiosInstance from './config/axiosConfig';

import Login from './pages/login/Login';
import Home from './pages/home/Home';
import CriarConta from './pages/criarConta/CriarConta';
import Nav from './components/navbar/Nav';

function App() {

  /*const handleClick = async () => {
    const token = localStorage.getItem('token'); // Recuperando o token
    console.log('Token Recuperado:', token); // Verificando se o token está presente
    
    try {
      const response = await axiosInstance.get('/api/usuarios');
      console.log('Dados recebidos:', response.data);
    } catch (error) {
      console.error('Error:', error);
    }
  }*/
  
  return (
    <div className={styles.App}>
      <BrowserRouter>
      <Nav/>
        <Routes>
          <Route path='/' element={<Home/>} />
          <Route path='/CriarConta' element={<CriarConta/>} />
          <Route path='/Entrar' element={<Login />} />

          <Route path='*' />
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
