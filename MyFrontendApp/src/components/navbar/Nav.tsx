import { useEffect, useState } from 'react'
import { Link, NavLink, useLocation } from 'react-router-dom'
import styles from './Nav.module.scss'
import { useAuth } from '../../context/AuthContext';

const Nav = () => {
  const location = useLocation()
  const {user} = useAuth()

  if (location.pathname === '/CriarConta' || location.pathname === '/Entrar'){
    return null
  }
  

  return (
    <header>
      <nav className={styles.navContent}>
        <div className={styles.logo}>
          <NavLink to='/'>
            Home
          </NavLink>
        </div>
        <div className={styles.navListItems}>
          <ul>
            {user ? (
              <>
                <li>
                  <NavLink to='/Perfil' 
                  className={({ isActive }) => (isActive ? styles.active : '')}>
                    Perfil
                  </NavLink>
                </li>
              </>
            ) : (
              <>
                <li>
                  <NavLink to='/CriarConta' 
                  className={({ isActive }) => (isActive ? styles.active : "")}> 
                    Criar Conta
                  </NavLink>
                </li>
                <li>
                  <NavLink to='/Entrar' 
                  className={({ isActive }) => (isActive ? styles.active : "")}>
                  Entrar
                  </NavLink>
                </li>
              </>
            )}
            <li><NavLink to='/About' 
            className={({ isActive }) => (isActive ? styles.active : "")}
            >About</NavLink></li>
          </ul>
        </div>
      </nav>
    </header>
  );
};

export default Nav;
