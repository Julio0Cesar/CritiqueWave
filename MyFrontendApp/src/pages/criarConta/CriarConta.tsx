import styles from './CriarConta.module.scss'
import { Link, useLocation } from 'react-router-dom'
import { useState } from 'react';

const CriarConta = () => {
    const [formData, setFormData] = useState({
        nome: '',
        email: '',
        senha: '',
        cpf: '',
        dataNascimento: '',
    })

    const handleChange = (e: { target: { name: any; value: any; }; }) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    };

    const handleSubmit = async (e: { preventDefault: () => void; }) =>{
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
          <label className={styles.label}>
              <h3>CPF:</h3> 
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
          <label className={styles.label}>
              <h3>Data de nascimento:</h3> 
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
          <div className={styles.footer}>
            <button type="submit">Criar Conta</button>
            <div className={styles.br}>
            </div>
            <p>ou faça o login <Link to='/Login'><a>aqui</a></Link></p>
          </div>
        </form>
        </div>
    );
}

export default CriarConta;