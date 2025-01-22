import axiosInstance from '../config/axiosConfig'

interface LoginData {
    email: string;
    senha: string;
}

export const login = async (loginData:LoginData): Promise<any> => {
    try {
        const response =await axiosInstance.post('/api/auth/login', loginData)
        return response.data
    } catch (error) {
        console.error('Erro ao fazer login: ', error)
        throw error
    }
}