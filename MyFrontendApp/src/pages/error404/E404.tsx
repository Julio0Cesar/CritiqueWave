import { Navigate, useNavigate } from 'react-router-dom'
import styles from './E404.module.scss'

const E404 = () => {
    const navigate = useNavigate()

    const navigateReturn = () =>{
        navigate("/")
        
}

    return (
        <div className={styles.container}>
            <div className={styles.text}>
                <h1>404</h1>
            </div>
            <div className={styles.text}>
                <h3>Pagina não encontrada</h3>
            </div>
            <div className={styles.text}>
                <button onClick={navigateReturn}>Retorne para home</button>
            </div>
        </div>
    )
}

export default E404