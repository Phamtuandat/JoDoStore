import { Category } from "models"
import { ListResponse } from "models/common"
import axiosClient from "./AxiosClient"

const categoryApi = {
    getAll(): Promise<ListResponse<Category>> {
        const url = "/Categories"
        return axiosClient.get(url)
    },
}
export default categoryApi
