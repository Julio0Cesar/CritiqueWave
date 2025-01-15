import React from 'react';
import './App.scss';
import axiosInstance from './config/axiosConfig.ts';
import { BrowserRouter as Router, Route, Routes, Link} from 'react-router-dom';
import CriarConta from './pages/CriarConta.tsx';
import Navbar from './components/Navbar.tsx';
import Home from './pages/Home.tsx';

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
      <Router>
        <Navbar/>
        <div classname="App">
          <Routes>
            <Route path="/" element={<Home />}></Route>
            <Route path="/criar-conta" element={<CriarConta />} />
          </Routes>
        </div>
      </Router>
  );
}

export default App;
