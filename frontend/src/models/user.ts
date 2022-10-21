export interface authResponse {
    id: number | string
    firstName: string
    lastName: string
    email: string
    token: string
    refreshToken: string
}

export interface userInfor {
    id: number | string
    firstName: string
    lastName: string
    email: string
}
export interface loginRequest {
    email: string
    password: string
}

export interface registerRequest {
    email: string
    password: string
    confirmPassword: string
    firstName: string
    lastName: string
    phoneNumber: number | null
}
