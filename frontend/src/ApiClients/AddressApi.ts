import { Address, ListResponse } from "models"
import axiosClient from "./AxiosClient"

export const addressApi = {
    getAll(): Promise<ListResponse<Address>> {
        const url = "/Address"
        return axiosClient.get(url)
    },
}
