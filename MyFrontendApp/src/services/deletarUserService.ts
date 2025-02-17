import axiosInstance from "../config/axiosConfig";

export const deletarUser = async () => {
    try {
        const responseDelete = await axiosInstance.delete("usuarios/excluir-usuario", {
            headers: {
                Authorization: `Bearer ${localStorage.getItem("token")}`,
            },
        })

        return { responseDelete }
    } catch (error: any) {
        console.log("Erro:", error.response?.data?.message || error.message)
        throw error
    }
};
