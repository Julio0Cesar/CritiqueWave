import axiosInstance from "../config/axiosConfig";

export const criaUser = async (
    nome: string, 
    username: string,
    email: string, 
    senha: string
) => {
    try {
        const response = await axiosInstance.post("usuarios/criar", {
            Nome: nome,
            UserName: username,
            Email: email,
            SenhaHash: senha
        })
        
        return response.data
    } catch (error: any) {
        console.log("Erro:", error.response?.data?.message || error.message)
        throw error
    }
}