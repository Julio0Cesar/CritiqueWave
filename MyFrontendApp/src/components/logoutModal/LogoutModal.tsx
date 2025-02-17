import { useNavigate } from 'react-router-dom'
import styles from './LogoutModal.module.scss'

interface LogoutModalProps {
  onCancel: () => void
}

const LogoutModal: React.FC<LogoutModalProps> = ({ onCancel }) => {
  const navigate = useNavigate()

  const navigateReturn = () =>{
    navigate("/Entrar")
  }
  const handleLogout = () => {
    localStorage.removeItem('token')
    navigateReturn()
    window.location.reload()
  }

  return (
    <div className={styles.modalOverlay}>
      <div className={styles.modalContent}>
        <h3>Deseja mesmo sair da conta?</h3>
        <div className={styles.buttons}>
          <button className={styles.sair} onClick={handleLogout}>Sair</button>
          <button className={styles.cancelar} onClick={onCancel}>Cancelar</button>
        </div>
      </div>
    </div>
  )
}

export default LogoutModal
