import { useState } from 'react';
import styles from './EditPerfilModal.module.scss';  
import { atualizarUser } from '../../services/atualizarUserService';

const EditPerfilModal = () => {

  const [formData, setFormData] = useState({
    username: '',
    status: '',
    sobre: '',
    email: '',
    senha: '',
    foto: null as File | null,
  });

  const handleChange = (e: any ) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
        ...prevData,
        [name]: value,
    }))
}

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files) {
        setFormData({
          ...formData,
          foto: e.target.files[0],
        })
      }
  }

  const handleSubmit = async (e:{preventDefault: () => void; }) => {
    e.preventDefault()
    try {
        const response = await atualizarUser(
            formData.username,
            formData.status,
            formData.sobre,
            formData.email,
            formData.senha,
            formData.foto,
        )
    } catch (error) {
            console.error('Erro:', error)
    }
  }

  return (
    <div className={styles.modal}>
      <div className={styles.modalContent}>
        <button className={styles.closeButton}>X</button>
        <h2>Editar Perfil</h2>
        <form onSubmit={handleSubmit}>
          <div className={styles.formGroup}>
            <label htmlFor="username">Username</label>
            <input
              type="text"
              id="username"
              name="username"
              value={formData.username}
              onChange={handleChange}
            />
          </div>
          <div className={styles.formGroup}>
            <label htmlFor="status">Status</label>
            <input
              type="text"
              id="status"
              name="status"
              value={formData.status}
              onChange={handleChange}
            />
          </div>
          <div className={styles.formGroup}>
            <label htmlFor="sobre">Sobre</label>
            <textarea
              id="sobre"
              name="sobre"
              value={formData.sobre}
              onChange={handleChange}
            />
          </div>
          <div className={styles.formGroup}>
            <label htmlFor="email">E-mail</label>
            <input
              type="email"
              id="email"
              name="email"
              value={formData.email}
              onChange={handleChange}
            />
          </div>
          <div className={styles.formGroup}>
            <label htmlFor="senha">Senha</label>
            <input
              type="password"
              id="senha"
              name="senha"
              value={formData.senha}
              onChange={handleChange}
            />
          </div>
          <div className={styles.formGroup}>
            <label htmlFor="foto">Foto de Perfil</label>
            <input
              type="file"
              id="foto"
              name="foto"
              onChange={handleFileChange}
            />
          </div>
          <button type="submit" className={styles.submitButton}>Salvar</button>
        </form>
      </div>
    </div>
    )

}

export default EditPerfilModal
