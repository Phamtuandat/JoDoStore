import { category } from "models"
import { ListResponse } from "models/common"
import axiosClient from "./AxiosClient"

const categoryApi = {
    getAll(): Promise<ListResponse<category>> {
        const url = "/category"
        return axiosClient.get(url, {
            params: {
                _limit: 50,
                _page: 1,
            },
        })
    },
}
export default categoryApi
