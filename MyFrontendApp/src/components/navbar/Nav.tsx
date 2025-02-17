import { useEffect, useState } from 'react'
import { Link, NavLink, useLocation, useNavigate } from 'react-router-dom'
import styles from './Nav.module.scss'
import { useAuth } from '../../context/AuthContext';
import LogoutModal from '../logoutModal/LogoutModal';

const Nav = () => {
  const location = useLocation()
  const {user} = useAuth()
  const [showModal, setShowModal] = useState(false);

  if (location.pathname === '/CriarConta' 
    || location.pathname === '/Entrar' 
    || location.pathname === '/EsqueceuSenha'
    || location.pathname === '/AlterarSenha'){
    return null
  }
  const handleLogoutClick = () => {
    setShowModal(true)
  }

  const handleCancel = () => {
    setShowModal(false)
  }


  return (
      <nav>
        <div className={styles.navContent}>
          <div className={styles.navListItems}>
            <ul>
              <li>
                <NavLink to='/'>
                  <h3>Home</h3>
                </NavLink>
              </li>
              {user ? (
                <>
                  <li>
                    <NavLink to='/Perfil' >
                      <h3>Perfil</h3>
                    </NavLink>
                  </li>
                  <li>
                    <NavLink to='/EditarUsuario'>
                      <h3>Configurações</h3>
                    </NavLink>
                  </li>
                  <li>
                    <h3 onClick={handleLogoutClick}>Logout</h3>
                  </li>
                </>
              ) : (
                <>
                  <li>
                    <NavLink to='/CriarConta'>
                      <h3>Criar Conta</h3>
                    </NavLink>
                  </li>
                  <li>
                    <NavLink to='/Entrar'>
                    <h3>Entrar</h3>
                    </NavLink>
                  </li>
                </>
              )}
              <li><NavLink to='/About'>
              <h3>About</h3></NavLink></li>
            </ul>
          </div>
          <div className={styles.navListItemsSearch}>
              <ul>
                <li>
                  <input 
                    type="text" />
                </li>
                <li>
                  <button>Pesquisar</button>
                </li>
              </ul>
          </div>
        </div>
        {showModal && (
        <LogoutModal
          onCancel={handleCancel}
        />
      )}
      </nav>
  )
}

export default Nav
