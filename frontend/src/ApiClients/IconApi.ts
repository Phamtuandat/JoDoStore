import { Icon, ListResponse } from "models"
import axiosClient from "./AxiosClient"

export const brandApi = {
    getAll(): Promise<ListResponse<Icon>> {
        const url = "/Icon"
        return axiosClient.get(url)
    },
}
