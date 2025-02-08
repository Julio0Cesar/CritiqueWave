import axiosInstance from "../config/axiosConfig"

export const autenticaUser = async (email: string, senha: string) => {
    try {
        const response = await axiosInstance.post("auth/login", {
            EmailDTO: email,
            SenhaHashDTO: senha
        })
        return response.data
    } catch (error: any) {
        console.error("Erro de login:", error.response?.data?.message || error.message)
        throw error;
    }
}