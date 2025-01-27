import styles from './styles/Global.scss';

import { BrowserRouter, Navigate, HashRouter, Router, Route, Routes, Link} from 'react-router-dom';

import Login from './pages/login/Login';
import Home from './pages/home/Home';
import CriarConta from './pages/criarConta/CriarConta';
import Nav from './components/navbar/Nav';

function App() {
  
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
