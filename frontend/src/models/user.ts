export interface AuthenticateInfo {
    id: number | string
    firstName: string
    lastName: string
    email: string
    address: Address | null
    phoneNumber?: string
    birthday?: string
    gender?: "Male" | "Female" | "Order"
    emailConfirmed: boolean
}
export type editForm = {
    firstName: string
    lastName: string
    birthday: string
    gender: string
    phoneNumber: string
}
export interface Address {
    id: number | string
    district: string
    province: string
    ward: string
    address: string
    isDefault?: boolean
    name: string
    phoneNumber: string
}
export interface SaveAddress {
    id: number | string
    district: string
    province: string
    ward: string
    address: string
    isDefault?: boolean
    name: string
    phoneNumber: string
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
    phoneNumber: string | null
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
    addressBook?: Address
    orderDate?: string
    userName?: string
    totalPrice?: string | number
    status?: string
}
