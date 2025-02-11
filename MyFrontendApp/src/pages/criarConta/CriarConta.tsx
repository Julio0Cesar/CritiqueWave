import styles from './CriarConta.module.scss'
import { Link, useLocation, useNavigate } from 'react-router-dom'
import { useState } from 'react'
import { criaUser } from '../../services/criaUserService'

const CriarConta = () => {
    const [formData, setFormData] = useState({
        nome: '',
        username: '',
        email: '',
        senha: ''
    })
    const navigate = useNavigate()

    const handleChange = (e:any) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }))
    }

    const handleSubmit = async (e: { preventDefault: () => void; }) =>{
        e.preventDefault();
        
        try {
            const response = await criaUser(
                formData.nome, 
                formData.username, 
                formData.email, 
                formData.senha
            )

            navigate("/Entrar")
        } catch (error) {
            console.error('Erro:', error)
        }

    }
    return(
        <div className={styles.container}>
            <form onSubmit={handleSubmit} className={styles.form}>
            <h2><Link to='/'><a>Retorne</a></Link></h2>
          <label className={styles.label}>
              <h3>Nome:</h3> 
              <input 
                  type='text'
                  name='nome'
                  autoComplete='off'
                  required
                  placeholder='Nome'
                  value={formData.nome}
                  onChange={handleChange}
              />
          </label>  
          <label className={styles.label}>
              <h3>Username:</h3> 
              <input 
                  type='text'
                  name='username'
                  autoComplete='off'
                  required
                  placeholder='Username'
                  value={formData.username}
                  onChange={handleChange}
              />
          </label>  
          <label className={styles.label}>
            <h3>E-mail: </h3>
              
              <input 
                type='email'
                name='email'
                required
                placeholder='E-mail'
                  value={formData.email}
                  onChange={handleChange}
              />
          </label>
          <label className={styles.label}>
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
          <div className={styles.footer}>
            <button type="submit">Criar Conta</button>
            <div className={styles.br}>
            </div>
            <p>ou faça o login <Link to='/Entrar'><a>aqui</a></Link></p>
          </div>
        </form>
        </div>
    )
}

export default CriarConta;