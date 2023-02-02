import { ListResponse, Product } from "models"
import axiosClient from "./AxiosClient"

export const productApi = {
    create(data: FormData) {
        const url = "/Product"
        return axiosClient.post(url, data, {
            headers: {
                "Content-Type": "multipart/form-data; boundary=something",
            },
        })
    },
    getList(params?: string): Promise<ListResponse<Product>> {
        const url = `/Product${params || ""}`
        return axiosClient.get(url)
    },
    getById(id: number) {
        const url = `/Product/${id}`
        return axiosClient.get(url)
    },
    update(data: FormData, id: number) {
        const url = `/Product/${id}`
        return axiosClient.patch(url, data)
    },
    delete(id: number) {
        const url = `/Product/${id}`
        return axiosClient.delete(url)
    },
}
