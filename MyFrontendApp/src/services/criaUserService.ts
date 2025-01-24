import axiosInstance from "../config/axiosConfig";

export const criaUser = async (
    nome: string, 
    cpf: string, 
    email: string, 
    senha: string, 
    dataNascimento: string
) => {
    try {
        const response = await axiosInstance.post("usuarios/criar", {
            Nome: nome,
            Cpf: cpf,
            Email: email,
            SenhaHash: senha,
            DataNascimento: dataNascimento,
        })
        
        return response.data
    } catch (error: any) {
        console.log("Erro:", error.response?.data?.message || error.message)
        throw error;
    }
}