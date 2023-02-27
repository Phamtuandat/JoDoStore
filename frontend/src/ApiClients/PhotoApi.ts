import { ListResponse, Photo } from "models"
import axiosClient from "./AxiosClient"

const PhotoApi = {
    getAll(param?: string): Promise<ListResponse<Photo>> {
        const url = `/Photo/${param || ""}`
        return axiosClient.get(url)
    },

    delete(param: number): Promise<ListResponse<Photo>> {
        const url = `/Photo/${param}`
        return axiosClient.delete(url)
    },
    create(param: FormData): Promise<ListResponse<Photo>> {
        const url = "/Photo"
        return axiosClient.post(url, param, {
            headers: {
                "Content-Type": "multipart/form-data; boundary=something",
            },
        })
    },
}

export default PhotoApi
