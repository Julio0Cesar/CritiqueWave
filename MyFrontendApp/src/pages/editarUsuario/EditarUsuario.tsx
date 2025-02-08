import { useEffect, useState } from 'react'
import styles from './EditarUsuario.module.scss'
import { Link, useNavigate } from 'react-router-dom'
import { getUserData } from '../../services/getUserDataService'
import { getUserProfile } from '../../services/getUserProfileService'
import { atualizarUser } from '../../services/atualizarUserService'

const EditarUsuario = () => {

    const [userData, setUserData] = useState<any>(null)
    const [perfilData, setPerfilData] = useState<any>(null)
    const navigate = useNavigate()

    useEffect(() => {
        const fetchData = async () => {
            const token = localStorage.getItem('token')
            if (!token) return
    
            try {
                const [user, profile] = await Promise.all([
                    getUserData(token),
                    getUserProfile(token)
                ])
                setUserData(user)
                setPerfilData(profile)
            } catch (err) {
                console.error("Erro ao buscar dados do usuário:", err)
            }
        }
    
        fetchData()
    }, [])

    const [formData, setFormData] = useState({
        nome: "",
        username: "",
        email: "",
        senha: ""
    });
    
    useEffect(() => {
        if (userData) {
            setFormData({
                nome: userData.nome || "",
                username: userData.username || "",
                email: userData.email || "",
                senha: userData.senhahash || ""
            });
        }
    }, [userData]);
    

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
            const response = await atualizarUser(
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
export default EditarUsuario