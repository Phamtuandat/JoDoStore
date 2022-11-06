import { ListParams, Product } from "models"
import axiosClient from "./AxiosClient"

const productApi = {
    getList(params: ListParams) {
        const url = "/Product"
        return axiosClient.get(url, {
            params: {
                _limit: 50,
                _page: 1,
            },
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
