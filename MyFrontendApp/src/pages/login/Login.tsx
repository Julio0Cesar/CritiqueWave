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
    <div className={styles.container}>
      <form onSubmit={handleLogin} className={styles.form}>
        <h2><Link to="/"><a>Retorne</a></Link></h2>
        <div className={styles.labels}>
          <label htmlFor="" className={styles.label}>
            <h3>E-mail:</h3>
            <input 
              type='email'
              name='email'
              autoComplete='on'
              required
              placeholder='E-mail'
              value={formData.email}
              onChange={handleChange}
            />
          </label>
          <label htmlFor="" className={styles.label}>
            <h3>Senha:</h3>
            <input 
              type='password'
              name='senha'
              autoComplete='off'
              required
              placeholder='Senha'
              value={formData.senha}
              onChange={handleChange}
            />
          </label>
        </div>   
        <div className={styles.footer}>
          <button type="submit">Entrar</button>
          <div className={styles.br}>
          </div>
          <p>ou crie sua conta <Link to='/CriarConta'><a>aqui</a></Link></p>
        </div>
      </form>
    </div>
  )
}

export default Login;