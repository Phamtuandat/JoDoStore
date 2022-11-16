import { call, fork, put, race, take } from "@redux-saga/core/effects"
import { PayloadAction } from "@reduxjs/toolkit"
import authApi from "ApiClients/AuthApi"
import { AuthenticateInfo, AuthResponse, LoginRequest, RegisterRequest } from "models"
import handleNotify from "utils/Toast-notify"
import { AuthSliceAction } from "./authSlice"

interface IRace {
    response: AuthResponse<AuthenticateInfo>
    cancel: any
}

function* handleLogin(action: PayloadAction<LoginRequest>): any {
    try {
        const { response }: IRace = yield race({
            response: call(authApi.login, action.payload),
            cancel: take(AuthSliceAction.failed.type),
        })
        yield put(AuthSliceAction.success(response.data))
        yield call(handleNotify.success, "Login is successfully!")
    } catch (error) {
        yield put(AuthSliceAction.failed(error as string))
    }
}
function* handleRefreshToken() {
    const refreshToken = localStorage.getItem("refreshToken") as string
    try {
        const result: AuthResponse<AuthenticateInfo> = yield call(
            authApi.refreshToken,
            refreshToken
        )
        yield put(AuthSliceAction.success(result.data))
    } catch (error) {
        yield put(AuthSliceAction.failed)
        yield call(handleLogout)
    }
}
function* handleRegister(action: PayloadAction<RegisterRequest>) {
    try {
        const result: AuthResponse<AuthenticateInfo> = yield call(authApi.register, action.payload)
        yield put(AuthSliceAction.success(result.data))
    } catch (error) {
        yield put(AuthSliceAction.failed)
    }
}

function* handleLogout() {
    const itemList: Array<string> = ["currentUser", "token", "refreshToken", "tokenExpirationDate"]
    yield itemList.forEach((e) => {
        localStorage.removeItem(e)
    })
    console.log("comming")
}
function* watchLogingFlow() {
    while (true) {
        const isLoggedIn = Boolean(localStorage.getItem("token"))
        if (!isLoggedIn) {
            const loginAction: PayloadAction<LoginRequest> = yield take(AuthSliceAction.login.type)
            yield call(handleLogin, loginAction)
        } else {
            yield take(AuthSliceAction.logout.type)
            yield call(handleLogout)
        }
    }
}
function* watchRefeshTokenFlow() {
    while (true) {
        const isLoggedIn = JSON.parse(localStorage.getItem("persist:root") as string).auth
            .refreshToken
        if (isLoggedIn) {
            yield take(AuthSliceAction.refreshToken.type)
            yield fork(handleRefreshToken)
        } else {
            yield take(AuthSliceAction.logout.type)
            yield call(handleLogout)
        }
    }
}
function* watchRegisterFlow() {
    while (true) {
        const isLoggedIn = JSON.parse(localStorage.getItem("persist:root") as string).auth
            .isLoggedIn
        if (!!!isLoggedIn) {
            const registerAction: PayloadAction<RegisterRequest> = yield take(
                AuthSliceAction.register.type
            )
            yield fork(handleRegister, registerAction)
        } else {
            yield take(AuthSliceAction.logout.type)
            yield call(handleLogout)
        }
    }
}
export default function* authSaga() {
    yield fork(watchLogingFlow)
    yield fork(watchRefeshTokenFlow)
    yield fork(watchRegisterFlow)
}
