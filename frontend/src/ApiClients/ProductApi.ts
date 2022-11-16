import { ListParams, Product } from "models"
import axiosClient from "./AxiosClient"

export const productApi = {
    create(data: FormData, token: string) {
        const url = "/Product"
        return axiosClient.post(url, data, {
            headers: {
                "Content-Type": "multipart/form-data",
                Authorization: "Bearer " + token,
            },
        })
    },
    getList(params?: ListParams) {
        const url = "/Product"
        return axiosClient.get(url, {
            params: params,
        })
    },
    getById(id: number) {
        const url = `/Product/${id}`
        return axiosClient.get(url)
    },
    update(id: number, product: Product) {
        const url = `/Product/${id}`
        return axiosClient.patch(url, product, {
            headers: {
                Authorization: "Bearer " + JSON.parse(localStorage.getItem("token") as string),
            },
        })
    },
    delete(id: number) {
        const url = `/Product/${id}`
        return axiosClient.delete(url, {
            headers: {
                Authorization: "Bearer " + JSON.parse(localStorage.getItem("token") as string),
            },
        })
    },
}
