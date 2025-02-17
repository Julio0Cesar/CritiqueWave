import { useState } from "react";
import styles from "./Login.module.scss";
import { Link, useNavigate } from "react-router-dom";
import { autenticaUser } from "../../services/autenticaUserService";
import { useAuth } from "../../context/AuthContext";

const Login = () => {

  const {storeToken} = useAuth()
  const [formData, setFormData] = useState ({
    email:'',
    senha:''
  })
  const navigate = useNavigate();

  const handleLogin = async (e: { preventDefault: () => void; }) => {
    e.preventDefault()
  
    try {
      const response = await autenticaUser(formData.email, formData.senha)
      storeToken(response.token)

      navigate("/");
    } catch (error) {
      console.error("Erro ao autenticar", error)
    }
  }

  const handleChange = (e: {target: {name:any; value:any;}}) =>{
    const {name,value} = e.target
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }))
  }

  return (
    <div className="container-center">
      <form onSubmit={handleLogin}>
        <div className="card">
          <div className="retorne">
            <h3><Link to="/"><a>Retorne</a></Link></h3>
          </div>
          <div className="titulo">
            <h3>Bem vindo!</h3>
            <p>Acesse sua conta preenchendo as informações abaixo</p>
          </div>
          <div className="labels">
            <label className="label">
              <h4>E-mail ou Username</h4>
              <input 
                type='email'
                name='email'
                autoComplete='on'
                required
                placeholder='Digite seu e-mail ou username'
                value={formData.email}
                onChange={handleChange}
              />
            </label>
            <label className="label">
              <h4>Senha</h4>
              <input 
                type='password'
                name='senha'
                autoComplete='off'
                required
                placeholder='············'
                value={formData.senha}
                onChange={handleChange}
              />
            </label>
          </div>   
          <div className={styles.subArea}>
            <div className={styles.forgotPassword}>
              <p><Link to="/EsqueceuSenha"><a>Esqueceu a senha?</a></Link></p>
            </div>
          </div>
          <div className="submitOrOther">
            <div className="botao">
            <button type="submit">Entrar</button>
            </div>
            <div className="otherOptions">
              <p>Novo na plataforma?<Link to='/CriarConta'><a> Crie sua conta </a></Link></p>
            </div>
          </div>
          <div className="divider">
            <span className="dividerLine"></span><h4>ou</h4><span className="dividerLine"></span>
          </div>
          <div className="methods">
            <h4>G</h4>
          </div>
        </div>
      </form>
    </div>
  )
}

export default Login