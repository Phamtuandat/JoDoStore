import { AuthResponse, LoginRequest, RegisterRequest } from "models"
import axiosClient from "./AxiosClient"

const authApi = {
    login(params: LoginRequest): Promise<AuthResponse> {
        const url = "Authenticate/Login"
        return axiosClient.post(url, params)
    },
    register(params: RegisterRequest): Promise<AuthResponse> {
        const url = "Authenticate/Register"
        return axiosClient.post(url, params)
    },
    refreshToken(refreshToken: string): Promise<AuthResponse> {
        const url = "Authenticate/refreshToken"

        return axiosClient.post(url, undefined, {
            headers: { Authorization: "Bearer " + JSON.parse(refreshToken) },
        })
    },
    checkAdminRole() {
        const token: string = JSON.parse(localStorage.getItem("token") as string)
        const url = "Authenticate/AdminCheck"
        return axiosClient.post(url, undefined, {
            headers: { Authorization: "Bearer " + token },
        })
    },
}
export default authApi
