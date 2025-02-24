import styles from './CreatePostModal.module.scss'

interface CreatePostModalProps {
  onCancel: () => void
}

const CreatePostModal: React.FC<CreatePostModalProps> = ({ onCancel }) => {
  return (
      <div className={styles.modalOverlay}>
        <div className={styles.modalContent}>
          <div className={styles.title}>
            <h3 className={styles.titleCriar}>Criar novo Post </h3>
            <h3 onClick={onCancel} className={styles.titleReturn}>X </h3>
          </div>
          <div className={styles.form}>

            <div className={styles.labels}>
              <label className={styles.labelForm}><h4>Titulo</h4>
              </label>
              <input 
                type="text"
                placeholder='Escreva aqui o titulo' 
                required
              />
            </div>
            <div className={styles.labels}>
              <label className={styles.labelForm}><h4>Descrição</h4>
              </label>
              <input 
                type="text"
                placeholder='Adicionar descrição'
                required
              />
            </div>
            <div className={styles.labels}>
              <label className={styles.labelForm}><h4>Adicionar foto</h4>
              </label>
              <input 
                type="file"
                name=''
                placeholder='Adicionar Texto' 
                className={styles.fileInput}
                required
              />
            </div>
            
          </div>

        </div>
      </div>
  )
}

export default CreatePostModal
