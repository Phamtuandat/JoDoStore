export interface AuthenticateInfo {
    id: number | string
    firstName: string
    lastName: string
    email: string
    address: Address | null
    phoneNumber?: string
    birthday?: string
    gender?: "Male" | "Female" | "Order"
}
export type editForm = {
    firstName: string
    lastName: string
    birthday: string
    gender: string
    phoneNumber: string
}
export interface Address {
    id: number
    state: string
    city: string
    streetAddress: string
}

export interface UserInfor {
    id: number | string
    firstName: string
    lastName: string
    email: string
    address: Address | null
}
export interface LoginRequest {
    email: string
    password: string
    rememberMe?: boolean
}

export interface RegisterRequest {
    email: string
    password: string
    confirmPassword: string
    firstName: string
    lastName: string
    phoneNumber: number | null
    userName: string
}
export interface OrderItem {
    productId: number
    quantity: number
    id?: number
    totalPrice?: number
}
export interface Order {
    id?: number
    orderItems: OrderItem[]
    shippingCash: number
    addressId: number
    address?: Address
    orderDate?: string
}
