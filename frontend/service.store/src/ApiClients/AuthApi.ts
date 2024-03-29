import { AuthenticateInfo, AuthResponse, editForm, LoginRequest, RegisterRequest } from "models"
import axiosClient from "./AxiosClient"

const authApi = {
    login(params: LoginRequest): Promise<AuthResponse<AuthenticateInfo>> {
        const url = "User/login"
        return axiosClient.post(url, params, { withCredentials: true })
    },
    updateInfo(params: editForm): Promise<AuthResponse<AuthenticateInfo>> {
        const url = "User"
        return axiosClient.patch(url, params, { withCredentials: true })
    },
    register(params: RegisterRequest): Promise<AuthResponse<AuthenticateInfo>> {
        const url = "User/register"
        return axiosClient.post(url, params)
    },
    logout() {
        const url = "User/logout"
        return axiosClient.post(url)
    },
    refreshCookie(token?: string): Promise<AuthResponse<AuthenticateInfo>> {
        const url = process.env.IDENTITY_URL + "/connect/userinfo"
        return axiosClient.get(url, {
            headers: { Authorization: "Bearer " + token },
        })
    },
    adminValidate(): Promise<AuthResponse<AuthenticateInfo>> {
        const url = "User/Admin"
        return axiosClient.post(url)
    },
    confirmEmail() {
        const url = "User/confirmReq"
        return axiosClient.post(url)
    },
}
export default authApi
