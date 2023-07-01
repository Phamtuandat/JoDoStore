import axiosClient from "./AxiosClient"

export const bannerApi = {
    getAll(): Promise<string[]> {
        const url = "/banner"
        return axiosClient.get(url).then((response) => response.data)
    },
}
