import { useState } from "react";
import styles from "./Login.module.scss";
import { Link } from "react-router-dom";
import { login } from "../../services/authService";

const Login = () => {

  const [formData, setFormData] = useState ({
    email:'',
    senha:'',
    message:'',
  })

  const handleLogin = async (e: { preventDefault: () => void; }) => {
    e.preventDefault()
  
    console.log("Token armazenado no localStorage:", localStorage.getItem('token'));
  
    // Verificando se formData é um objeto válido
    if (typeof formData === 'object' && formData !== null) {
      // Converte para JSON e loga a string JSON
      const jsonString = JSON.stringify(formData);
      console.log("formData convertido para JSON:", jsonString);
    } else {
      console.error("formData não é um objeto válido.");
    }
  
    try {
      const response = await login({ email: formData.email, senha: formData.senha });
      localStorage.setItem("token", response.token);
      setFormData((prev) => ({ ...prev, message: 'login realizado com sucesso' }));
    } catch (error) {
      console.error("Erro ao tentar fazer login:", error);
      setFormData((prev) => ({ ...prev, message: 'Email ou senha inválidos' }));
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