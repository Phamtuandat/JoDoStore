import { Address, ListResponse } from "models"
import axiosClient from "./AxiosClient"

export const addressApi = {
    getAll(): Promise<ListResponse<Address>> {
        const url = "/Address"
        return axiosClient.get(url)
    },
    create(param: Address): Promise<ListResponse<Address>> {
        const url = "/Address"
        return axiosClient.post(url, param)
    },
    update(param: Address): Promise<ListResponse<Address>> {
        const url = "/Address"
        return axiosClient.patch(url, param)
    },
}
