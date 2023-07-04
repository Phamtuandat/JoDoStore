import { createSlice, PayloadAction } from "@reduxjs/toolkit"
import { RootState } from "app/store"
import { AuthenticateInfo, LoginRequest, RegisterRequest } from "models"
import handleNotify from "utils/Toast-notify"

export interface AuthState {
    isLoggedIn: boolean | null
    processing: boolean
    // current user logged
    currentUser?: AuthenticateInfo | null
    error: string
    token: string | null
}

const initialState: AuthState = {
    isLoggedIn: null,
    processing: false,
    currentUser: null,
    error: "",
    token: null,
}

const AuthSlice = createSlice({
    name: "authSlice",
    initialState,
    reducers: {
        login(state, action: PayloadAction<LoginRequest>) {
            state.processing = true
        },
        register(state, action: PayloadAction<RegisterRequest>) {
            state.processing = true
        },
        success(state, action: PayloadAction<AuthenticateInfo>) {
            state.processing = false
            state.currentUser = action.payload
            state.isLoggedIn = true
            state.error = ""
            state.token = action.payload.access_Token
        },
        failed(state, action: PayloadAction<string>) {
            state.processing = false
            state.error = action.payload
            console.log(state.error)
            handleNotify.error(state.error)
        },
        logout(state) {
            state.isLoggedIn = false
            state.currentUser = null
            state.processing = false
        },
        cancel(state) {
            state.processing = false
        },
    },
})

//export actions
export const AuthSliceAction = AuthSlice.actions

//export selector
export const selectLogin = (state: RootState) => state.auth.isLoggedIn
export const selectIsLogging = (state: RootState) => state.auth.processing
export const selectCurrentUser = (state: RootState) => state.auth.currentUser
export const selectToken = (state: RootState) => state.auth.token

//export reducer
const AuthReducer = AuthSlice.reducer

export default AuthReducer
