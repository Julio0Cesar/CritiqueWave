import { useEffect, useState } from 'react'
import styles from './EditarUsuario.module.scss'
import { Link, useNavigate } from 'react-router-dom'
import { getUserData } from '../../services/getUserDataService'
import { getUserProfile } from '../../services/getUserProfileService'
import { atualizarUser } from '../../services/atualizarUserService'
import { atualizarProfile } from '../../services/atualizarProfileService'
import { deletarUser } from '../../services/deletarUserService'

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
        sobre: "",
        status: "",
        senha: ""
    })
    useEffect(() => {
        if (userData && perfilData) {
            setFormData({
                nome: userData.nome || "",
                username: userData.username || "",
                email: userData.email || "",
                status: perfilData.status || "",
                sobre: perfilData.sobre || "",
                senha: userData.senhaHash || ""
            })
        }
    }, [userData, perfilData])
    const handleChange = (e:any) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }))
    }

    const handleSubmit = async (e: { preventDefault: () => void }) =>{
        e.preventDefault()
        
        try {
            await atualizarUser(
                formData.nome, 
                formData.username, 
                formData.email,
                formData.senha
            )
            await atualizarProfile(
                formData.status,
                formData.sobre
            )
            navigate("/Perfil")
        } catch (error) {
            console.error('Erro:', error)
        }
    }
    const navigateHome = () =>{
        navigate("/")
    }

    const deletarusuario = async (e: { preventDefault: () => void }) =>{
        e.preventDefault()

        try {
            deletarUser()
            localStorage.removeItem('token')
            window.location.reload()
            navigate("/CriarConta")
        } catch (error) {
            console.error('Erro:', error)
        }
    }

return(
<div className="container">
    <form onSubmit={handleSubmit}>
        <div className='card'>
            <div className={styles.editImage}>
                <div className="profileImage">
                    <img 
                        src={perfilData?.fotoPerfil}
                        alt="" />
                </div>
                <div className={styles.atualizarFotoDePerfil}>
                    <div className='botao'>
                        <button className={styles.button}>Atualizar foto de perfil</button>
                    </div>
                    <p>Permitido JPG, JPEG ou PNG. Tamanho Max de 800KB</p>
                </div>
            </div>
            <div className="divider">
                <span className="dividerLine"></span>
            </div>
            <div className={styles.formEdit}>
                <div className={styles.labels}>
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
                </div>
                <div className={styles.labels}>
                    <label className="label">
                        <h4>Status</h4> 
                        <input 
                            type='text'
                            name='status'
                            autoComplete='off'
                            required
                            placeholder='Atualize seu status'
                            value={formData.status}
                            onChange={handleChange}
                        />
                    </label>  
                    <label className="label">
                        <h4>Sobre</h4> 
                        <input 
                            type='text'
                            name='sobre'
                            autoComplete='off'
                            required
                            placeholder='Digite algo sobre você'
                            value={formData.sobre}
                            onChange={handleChange}
                        />
                    </label>   
                </div>
            </div>
            <div className={styles.changeButtons}>
                <div className={styles.atualizarButton}>
                    <button type="submit">Salvar Alterações</button>
                </div>
                <div className={styles.atualizarButton}>
                    <button className={styles.cancelButton} onClick={navigateHome}>Cancelar</button>
                </div>
            </div>
        </div>
        <div className={styles.secondCard}>
            <div className="card">
                <div className="titulo">
                    <h3>Deletar Conta</h3>
                    <div className={styles.confirmMessage}>
                        <h3>Você tem certeza que deseja deletar sua conta?</h3>
                        <p>Ao deletar a conta você vai perder todas as informações</p>
                    </div>
                    <div className={styles.confirmDelete}>
                        <input type="checkbox" />
                        <h4>Eu confirmo que desejo deletar minha conta</h4>
                    </div>
                    <div className={styles.confirmDeleteButton}>
                        <button className={styles.deleteButton} onClick={deletarusuario}>Deletar Conta</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
</div>
)

}
export default EditarUsuario