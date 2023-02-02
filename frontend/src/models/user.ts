export interface AuthenticateInfo {
    id: number | string
    firstName: string
    lastName: string
    email: string
    adress: any
}

export interface UserInfor {
    id: number | string
    firstName: string
    lastName: string
    email: string
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
