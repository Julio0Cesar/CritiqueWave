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
        <div className="container-center">
            
            <form onSubmit={handleSubmit}>
                <div className='card'>
                <div className="retorne">
                    <h3><Link to="/"><a>Retorne</a></Link></h3>
                </div>
                <div className="titulo">
                    <h3>Bem vindo!</h3>
                    <p>Crie sua conta preenchendo as informações abaixo</p>
                </div>
                <div className='labels'>
                    <label className="label">
                        <h4>Nome</h4> 
                        <input 
                            type='text'
                            name='nome'
                            autoComplete='off'
                            required
                            placeholder='Digite seu nome'
                            value={formData.nome}
                            onChange={handleChange}
                        />
                    </label>  
                    <label className="label">
                        <h4>Username</h4> 
                        <input 
                            type='text'
                            name='username'
                            autoComplete='off'
                            required
                            placeholder='Digite seu username'
                            value={formData.username}
                            onChange={handleChange}
                        />
                    </label>  
                    <label className="label">
                        <h4>E-mail</h4>
                        <input 
                            type='email'
                            name='email'
                            required
                            placeholder='Digite seu e-mail'
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
                <div className="submitOrOther">
                    <div className="botao">
                        <button type="submit">Criar Conta</button>
                    </div>
                    <div className="otherOptions">
                        <p>Já possui uma conta?<Link to='/Entrar'><a> Entre aqui </a></Link></p>
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

export default CriarConta