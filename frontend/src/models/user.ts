export interface AuthResponse {
    id: number | string
    firstName: string
    lastName: string
    email: string
    token: string
    refreshToken: string
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
}

export interface RegisterRequest {
    email: string
    password: string
    confirmPassword: string
    firstName: string
    lastName: string
    phoneNumber: number | null
}
