import { useState } from 'react'
import styles from './EsqueceuSenha.component.scss'
import { Link, useNavigate } from 'react-router-dom'
import { criaUser } from '../../services/criaUserService'

const EsqueceuSenha = () => {
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

    return (
        <div className="container-center">
            <form onSubmit={handleSubmit}>
                <div className='card'>
                    <div className="titulo">
                        <h3>Esqueceu a senha?</h3>
                    <p>Insira seu e-mail e nós enviaremos as instruções para alterar a sua senha </p>
                    </div>
                    <div className='labels'>  
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
                    </div>
                <div className="submitOrOther">
                        <div className="botao">
                            <button type="submit">Enviar Solicitação</button>
                        </div>
                        <div className="otherOptions">
                            <p><Link to='/Entrar'><a> Retorne para a tela de login </a></Link></p>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    )
}

export default EsqueceuSenha