import React from 'react';
import { Link } from 'react-router-dom';

const Navbar = () => {
  return (
    <nav>
      <Link to="/">Home</Link> | 
      <Link to="/criar-conta">Criar Conta</Link>
    </nav>
  );
};

export default Navbar;
