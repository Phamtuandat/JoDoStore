import { ListResponse, Order } from "models"
import axiosClient from "./AxiosClient"

export const orderApi = {
    create(param: Order): Promise<ListResponse<Order>> {
        const url = "/Orders"
        return axiosClient.post(url, param)
    },
    getUserOrders(): Promise<ListResponse<Order>> {
        const url = "/orders/user"
        return axiosClient.get(url)
    },
}
