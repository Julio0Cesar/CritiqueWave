import axiosInstance from "../config/axiosConfig";

export const getUserProfile = async (token: string) => {
    try {
        const response = await axiosInstance.get(`perfil/me`,{
            headers: {
                Authorization: `Bearer ${token}`,
            }
        })
        return response.data
    } catch (error: any) {
        console.error("Erro:", error.response?.data?.message || error.message)
        throw error
    }
}