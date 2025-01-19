import { useState } from 'react'
import { Link, NavLink, useLocation } from 'react-router-dom'
import styles from './Nav.module.scss'

const Nav = () => {
  const location = useLocation()

  if (location.pathname === '/Login'){
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
            <li><NavLink to='/Login' 
            className={({ isActive }) => (isActive ? styles.active : "")}
            >Login</NavLink></li>
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
