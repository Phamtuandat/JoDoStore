import { Brand, ListResponse } from "models"
import axiosClient from "./AxiosClient"

export const brandApi = {
    getAll(): Promise<ListResponse<Brand>> {
        const url = "/Brand"
        return axiosClient.get(url)
    },
}
