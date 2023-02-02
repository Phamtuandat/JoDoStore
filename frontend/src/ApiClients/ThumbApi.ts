import { ListResponse, Thumbnail } from "models"
import axiosClient from "./AxiosClient"

const thumbApi = {
    getAll(param?: string): Promise<ListResponse<Thumbnail>> {
        const url = `/Thumbnail/${param || ""}`
        return axiosClient.get(url)
    },

    delete(param: number): Promise<ListResponse<Thumbnail>> {
        const url = `/Thumbnail/${param}`
        return axiosClient.delete(url)
    },
    create(param: FormData): Promise<ListResponse<Thumbnail>> {
        const url = "/Thumbnail"
        return axiosClient.post(url, param, {
            headers: {
                "Content-Type": "multipart/form-data; boundary=something",
            },
        })
    },
}

export default thumbApi
