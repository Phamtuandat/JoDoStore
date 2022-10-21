import { authResponse, loginRequest, registerRequest } from "models"
import axiosClient from "./AxiosClient"

const authApi = {
    login(params: loginRequest): Promise<authResponse> {
        const url = "Authenticate/Login"
        return axiosClient.post(url, params)
    },
    register(params: registerRequest): Promise<authResponse> {
        const url = "Authenticate/Register"
        return axiosClient.post(url, params)
    },
    refreshToken(refreshToken: string): Promise<authResponse> {
        const url = "Authenticate/refreshToken"

        return axiosClient.post(url, undefined, {
            headers: { Authorization: "Bearer " + JSON.parse(refreshToken) },
        })
    },
}
export default authApi
