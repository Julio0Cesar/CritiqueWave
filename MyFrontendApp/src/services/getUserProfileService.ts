import axiosInstance from "../config/axiosConfig";

export const getUserProfile = async (userId: string) => {
    try {
        const response = await axiosInstance.get(`usuarios/${userId}`)
        return response.data
    } catch (error: any) {
        console.error("Erro:", error.response?.data?.message || error.message)
        error
    }
}