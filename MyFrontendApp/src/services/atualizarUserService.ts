import axiosInstance from "../config/axiosConfig";

export const atualizarUser = async (
  nome?: string,
  username?: string,
  email?: string,
  senha?: string
) => {
  try {
    const userData = {
      Nome: nome,
      Username: username,
      Email: email,
      SenhaHash: senha
    }
    
    const responseUser = await axiosInstance.put("usuarios/atualizar-usuario", userData, {
      
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
    })

    return { user: responseUser.data}
  } catch (error: any) {
      console.log("Erro:", error.response?.data?.message || error.message)
      throw error
  }
}
