import { useEffect, useState } from 'react';
import Card from '../../components/card/Card'
import { getUserData } from '../../services/getUserDataService';
import styles from './Perfil.module.scss'

const Perfil = () => {
    const [userData, setUserData] = useState<any>(null);

    useEffect(() => {
        const token = localStorage.getItem('token')
        if (token) {
            getUserData(token)
                .then((data) => {
                    setUserData(data)
                })
                .catch((err) => {
                    console.error("Erro ao buscar usuário:", err)
                });
        } else {
        }
    }, []);


return(
    <div className={styles.container}>
        <div className={styles.profileHeader}>
            <div className={styles.profileBackground}>
                
            </div>
            <div className={styles.profileInfo}>
                <div className={styles.profileImage}>
                    <img 
                        src="https://images.unsplash.com/photo-1482877346909-048fb6477632?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=958&q=80" 
                        alt="" />
                </div>
                <div className={styles.profileName}>
                    <h2>{userData?.nome}</h2>
                    <p>Local</p>
                </div>
            </div>
            <div className={styles.profileEditButton}>
                <button>Editar perfil</button>

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
        <div className={styles.profileAbout}>
            <h3>Sobre</h3>
            <div className={styles.sobre}>
                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Quae corporis pariatur doloremque ipsam placeat facilis, aspernatur excepturi sapiente, temporibus eum culpa cum commodi ipsum animi illo optio incidunt iure hic.</p>
            </div>
        </div>
        <div className={styles.profilePosts}>
            <h3>Postagens Recentes</h3>
            <div className={styles.profileCards}>
                <Card/>
            </div>

        </div>

    </div>
)

}

export default Perfil
