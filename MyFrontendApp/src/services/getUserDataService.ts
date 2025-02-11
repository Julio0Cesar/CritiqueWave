import axiosInstance from "../config/axiosConfig";

export const getUserData = async (token: string) => {
    try {
        const response = await axiosInstance.get(`usuarios/me`,{
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