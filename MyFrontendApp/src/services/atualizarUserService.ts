import axiosInstance from "../config/axiosConfig";

export const atualizarUser = async (
    username?: string,
    status?: string,
    sobre?: string,
    email?: string,
    senha?: string,
    foto?: File | null
) => {
    const formData = new FormData();

    // Adicionando os dados do formulário
    if (username) formData.append("Username", username)
    if (status) formData.append("Status", status)
    if (sobre) formData.append("Sobre", sobre)
    if (email) formData.append("Email", email)
    if (senha) formData.append("SenhaHash", senha)
    if (foto) formData.append("FotoPerfil", foto)

    try {
        const responsePerfil = await axiosInstance.put("perfil/me", formData, {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        });
    
        const responseUser = await axiosInstance.put("usuarios/me", formData, {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        });
    
        return responseUser && responsePerfil.data    
    } catch (error: any) {
        console.log("Erro:", error.responsePerfil?.data?.message || error.message)
        throw error
    }
}