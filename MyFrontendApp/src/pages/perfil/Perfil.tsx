import { useEffect, useState } from 'react';
import Card from '../../components/card/Card'
import { getUserData } from '../../services/getUserDataService';
import { getUserProfile } from '../../services/getUserProfileService';
import styles from './Perfil.module.scss'
import { useNavigate } from 'react-router-dom';

const Perfil = () => {
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
    
    const navigateEditUser = () =>{
        navigate("/EditarUsuario")
    }


return(
    <div className={styles.container}>
        <div className={styles.profileHeader}>
            <div className={styles.profileBackground}>
            </div>
            <div className={styles.profileInfo}>
                <div className={styles.profileImage}>
                    <img 
                        src={perfilData?.fotoPerfil}
                        alt="" />
                </div>
                <div className={styles.profileName}>
                    <h2>{userData?.username || 'Nome não disponível'}</h2>
                    <p>{perfilData?.status || 'Status não disponível'}</p>
                </div>
            </div>
            <div className={styles.profileEditButton}>
                <button onClick={navigateEditUser}>Editar perfil</button>
            </div>
        </div>
        <div className={styles.profileDescription}>
            <div className={styles.profileDesc}>
                <div className={styles.Photos}>
                    <strong>%</strong>
                    <p>Posts</p>
                </div>
                <div className={styles.Followers}>
                    <strong>%</strong>
                    <p>Seguidores</p>

                </div>
                <div className={styles.Following}>
                    <strong>%</strong>
                    <p>Seguindo</p>

                </div>
            </div>
        </div>
        <div className={styles.profileSobre}>
            <h3>Sobre</h3>
            <div className={styles.sobre}>
                <p>{perfilData?.sobre || 'Informações sobre o perfil não disponíveis'}</p>
            </div>
        </div>
        <div className={styles.profilePosts}>
            <h3>Postagens Recentes</h3>
            <div className={styles.profileCards}>
                <Card/> <Card/> <Card/> <Card/> <Card/> <Card/>
            </div>

        </div>

    </div>
)

}

export default Perfil
