import { AuthenticateInfo, AuthResponse, LoginRequest, RegisterRequest } from "models"
import axiosClient from "./AxiosClient"

const authApi = {
    login(params: LoginRequest): Promise<AuthResponse<AuthenticateInfo>> {
        const url = "User/login"
        return axiosClient.post(url, params, { withCredentials: true })
    },
    register(params: RegisterRequest): Promise<AuthResponse<AuthenticateInfo>> {
        const url = "User/register"
        return axiosClient.post(url, params)
    },
    logout() {
        const url = "User/logout"
        return axiosClient.post(url)
    },
    refreshCookie(): Promise<AuthResponse<AuthenticateInfo>> {
        const url = "User/validate"
        return axiosClient.post(url)
    },
}
export default authApi
