import styles from './CreatePostModal.scss'

interface CreatePostModalProps {
  onCancel: () => void
}

const CreatePostModal: React.FC<CreatePostModalProps> = ({ onCancel }) => {
  return (
    <div>
        Crie seu post aqui
    </div>
  )
}

export default CreatePostModal
