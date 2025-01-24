import { useEffect, useState } from 'react'
import { Link, NavLink, useLocation, useNavigate } from 'react-router-dom'
import styles from './Nav.module.scss'

const Nav = () => {
  const location = useLocation()
  const [user, setUser] = useState(null)
  const navigate = useNavigate()

  useEffect(() => {
    const loggedInUser = JSON.parse(localStorage.getItem('user') || 'null')
    setUser(loggedInUser)
  }, [])

  if (location.pathname === '/CriarConta' || location.pathname === '/Entrar'){
    return null
  }

  const handleLogout = () => {
    setUser(null);
    localStorage.removeItem('user');
    navigate("/Login")
  };
  

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
                  <span>ID: 10</span>
                </li>
                <li>
                  <NavLink to='/Perfil' 
                  className={({ isActive }) => (isActive ? styles.active : '')}>
                    Perfil
                  </NavLink>
                </li>
                <li>
                  <NavLink to='/Logout' onClick={handleLogout}
                  className={({ isActive }) => (isActive ? styles.active : '')}>
                    Logout
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
