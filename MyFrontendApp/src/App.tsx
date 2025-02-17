import styles from './styles/Global.scss';

import { BrowserRouter, Navigate, HashRouter, Router, Route, Routes, Link} from 'react-router-dom';

import Login from './pages/login/Login';
import Home from './pages/home/Home';
import CriarConta from './pages/criarConta/CriarConta';
import Nav from './components/navbar/Nav';
import { AuthProvider, useAuth } from './context/AuthContext';
import Perfil from './pages/perfil/Perfil';
import E404 from './pages/error404/E404';
import EditarUsuario from './pages/editarUsuario/EditarUsuario';
import EsqueceuSenha from './pages/esqueceuSenha/EsqueceuSenha';
import AlterarSenha from './pages/alterarSenha/AlterarSenha';

function App() {

  const {user} = useAuth()
  
  return (
    <div className={styles.App}>
        <BrowserRouter>
        <Nav/>
          <Routes>
            <Route path='/' element={<Home/>} />
            <Route path='/CriarConta' element={user ? <Navigate to='/Perfil'/> :<CriarConta/>} />
            <Route path='/Entrar' element={user ? <Navigate to='/Perfil'/> :<Login />} />
            <Route path='/Perfil' element={user ? <Perfil />: <Navigate to='/Entrar' />} />
            <Route path='/EditarUsuario' element={user ? <EditarUsuario/> : <Navigate to='/Entrar'/>}/>
            <Route path="/EsqueceuSenha" element={user ? <Navigate to="/AlterarSenha" /> : <EsqueceuSenha />} />
            <Route path="/AlterarSenha" element={user ? <AlterarSenha /> : <Navigate to="/EsqueceuSenha" />} />

            <Route path='*' element={<E404 />}/>
          </Routes>
        </BrowserRouter>
      
    </div>
  );
}

export default App;
