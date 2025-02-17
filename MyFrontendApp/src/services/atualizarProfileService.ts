import axiosInstance from "../config/axiosConfig";

export const atualizarProfile = async (
    status?: string,
    sobre?: string,
    foto?: File | null
) => {
    try {
        const formData = new FormData();
        if (status) formData.append("Status", status)
        if (sobre) formData.append("Sobre", sobre)
        if (foto) formData.append("fotoPerfil", foto)

        const responsePerfil = await axiosInstance.put("perfil/me", formData, {
            headers: {
                "Content-Type": "multipart/form-data",
                Authorization: `Bearer ${localStorage.getItem("token")}`,
            },
        })

        return { perfil: responsePerfil.data }
    } catch (error: any) {
        console.log("Erro:", error.response?.data?.message || error.message)
        throw error
    }
};
