import { AxiosResponse } from "axios"
import { CartItemReq } from "models"
import axiosClient from "./AxiosClient"

export const cartApi = {
    getCart: () => {
        const url = "/cart"
        return axiosClient.get(url)
    },
    addItem: (param: CartItemReq): Promise<AxiosResponse<void>> => {
        const url = "/cart"
        return axiosClient.post(url, param)
    },
    removeItem: (id: number) => {
        const url = `/cart/${id}`
        return axiosClient.delete(url)
    },
}
