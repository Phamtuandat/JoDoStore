import { AxiosResponse } from "axios"
import { CartItemReq } from "models"
import axiosClient from "./AxiosClient"

export const cartApi = {
    getCart: (token: string) => {
        const url = "/cart"
        return axiosClient.get(url, {
            headers: {
                Authorization: "Bearer " + token,
            },
        })
    },
    addItem: (param: CartItemReq, token: string): Promise<AxiosResponse<void>> => {
        const url = "/cart"
        return axiosClient.post(url, param, {
            headers: {
                Authorization: "Bearer " + token,
            },
        })
    },
    removeItem: (id: number, token: string) => {
        const url = `/cart/${id}`
        return axiosClient.delete(url, {
            headers: {
                Authorization: "Bearer " + token,
            },
        })
    },
}
