import styles from './Login.module.scss'
import { Link } from 'react-router-dom'
import React, { useState } from 'react';

const Login = () => {
    const [formData, setFormData] = useState({
        nome: '',
        email: '',
        senha: '',
        cpf: '',
        dataNascimento: '',
    })

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    };

    const handleSubmit = async (e) =>{
        e.preventDefault(); // Previne o reload da pagina
        console.log(formData);
        try {
            const response = await fetch('http://localhost:5218/api/usuarios', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(formData),
            })
            if (response.ok){
                const data = await response.json()
                alert('Usuário criado com sucesso!');
                console.log(data);
            } else {
                alert('Erro ao criar o usuário')
            }
        } catch (error) {
            console.error('Erro:', error)
        }
    }
    return(
        <div>
            <form onSubmit={handleSubmit} className={styles.form}>
          <h1>Login</h1>
          <label>
              Name:
              <input 
                  type='text'
                  name='nome'
                  autoComplete='off'
                  required
                  placeholder='Name'
                  value={formData.nome}
                  onChange={handleChange}
              />
          </label>  
          <label>
              E-mail: 
              <input 
                type='email'
                name='email'
                required
                placeholder='E-mail'
                  value={formData.email}
                  onChange={handleChange}
              />
          </label>
          <label>
              Password:
              <input 
                  type='password'
                  name='senha'
                  autoComplete='off'
                  required
                  placeholder='Password'
                  value={formData.senha}
                  onChange={handleChange}
              />
          </label>     
          <label>
              CPF:
              <input 
                  type='text'
                  name='cpf'
                  autoComplete='off'
                  required
                  placeholder='CPF'
                  value={formData.cpf}
                  onChange={handleChange}
              />
          </label>    
          <label>
              Data de nascimento:
              <input 
                  type='date'
                  name='dataNascimento'
                  autoComplete='off'
                  required
                  placeholder='Data de nascimento'
                  value={formData.dataNascimento}
                  onChange={handleChange}
              />
          </label>  
          <button type="submit">Submit</button>
          <p>Create your Account <Link to='/'>Here</Link></p>
        </form>
        </div>
    );
}

export default Login;