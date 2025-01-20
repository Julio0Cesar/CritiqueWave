import { useState } from "react";
import styles from "./Login.module.scss";
import { Link } from "react-router-dom";

const Login = () => {

  const [formData, setformData] = useState ({
    email:'',
    senha:'',
  })

  const handleChange = (e: {target: {name:any; value:any;}}) =>{
    const {name,value} = e.target
    setformData((prevData) => ({
      ...prevData,
      [name]: value,
    }))
  }

  return (
    <div className={styles.container}>
      <form action="" className={styles.form}>
        <h2><Link to="/"><a>Retorne</a></Link></h2>
        <div className={styles.labels}>
          <label htmlFor="" className={styles.label}>
            <h3>E-mail:</h3>
            <input 
              type='email'
              name='e-mail'
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
              name='Senha'
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