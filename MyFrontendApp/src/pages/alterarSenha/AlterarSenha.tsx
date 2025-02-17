import { Link, useNavigate } from 'react-router-dom'
import styles from './AlterarSenha.component.scss'
import { useState } from 'react'
import { criaUser } from '../../services/criaUserService'

const AlterarSenha = () => {
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
                    <h3>Redefinir senha</h3>
                    <p>Sua nova senha deve ser diferente da anteriormente utilizada </p>
                </div>
                <div className='labels'>  
                <label className="label">
                    <h4>Nova Senha</h4>
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
                <label className="label">
                    <h4>Confirmar Senha</h4>
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
                        <button type="submit">Alterar Senha</button>
                    </div>
                    <div className="otherOptions">
                        <p><Link to='/Perfil'><a> Retorne para o perfil </a></Link></p>
                    </div>
                </div>
            </div>
        </form>
    </div>
    )
}

export default AlterarSenha