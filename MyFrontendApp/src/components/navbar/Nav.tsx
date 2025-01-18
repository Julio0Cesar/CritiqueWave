import { useState } from 'react'
import { Link, NavLink, useLocation } from 'react-router-dom'
import styles from './Nav.module.scss'
import React from 'react';

const Nav = () => {

  return (
    <header>
      <nav classname={styles.navContent}>
        <div classname={styles.logo}>
          <NavLink to='/'>
            Home
          </NavLink>
        </div>
        <div className={styles.navListItems}>
          <ul>
            <li><NavLink to='/Login' 
            classname={({ isActive }) => (isActive ? styles.active : "")}
            >Login</NavLink></li>
          </ul>
        </div>
      </nav>
    </header>
  );
};

export default Nav;
