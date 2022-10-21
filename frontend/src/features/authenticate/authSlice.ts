import { createSlice, PayloadAction } from "@reduxjs/toolkit"
import { authResponse, loginRequest, registerRequest } from "models"
import handleNotify from "utils/Toast-notify"

export interface AuthState {
    isLoggedIn: boolean
    processing: boolean
    // current user logged
    currentUser?: authResponse
    error: string
}

const initialState: AuthState = {
    isLoggedIn: Boolean(localStorage.getItem("token")),
    processing: false,
    currentUser: JSON.parse(localStorage.getItem("currentUser") as string),
    error: "",
}

const AuthSlice = createSlice({
    name: "authSlice",
    initialState,
    reducers: {
        login(state, action: PayloadAction<loginRequest>) {
            state.processing = true
        },
        register(state, action: PayloadAction<registerRequest>) {
            state.processing = true
        },
        success(state, action: PayloadAction<authResponse>) {
            state.processing = false
            state.currentUser = action.payload
            state.isLoggedIn = true
        },
        failed(state, action: PayloadAction<string>) {
            state.processing = false
            state.error = action.payload
            console.log(state.error)
            handleNotify.error(state.error)
        },
        logout(state) {
            state.isLoggedIn = false
            state.currentUser = undefined
        },
        refreshToken(state) {
            state.processing = true
        },
    },
})

//export actions
export const AuthSliceAction = AuthSlice.actions

//export selector
export const selectIsLogin = (state: any) => state.isLoggedIn
export const selectIsLogging = (state: any) => state.logging

//export reducer
const AuthReducer = AuthSlice.reducer

export default AuthReducer
