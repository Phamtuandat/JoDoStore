import { call, fork, put, race, take } from "@redux-saga/core/effects"
import { PayloadAction } from "@reduxjs/toolkit"
import authApi from "ApiClients/AuthApi"
import { AuthResponse, LoginRequest, RegisterRequest } from "models"
import handleNotify from "utils/Toast-notify"
import { AuthSliceAction } from "./authSlice"

interface IRace {
    response: AuthResponse
    cancel: any
}

function* handleLogin(action: PayloadAction<LoginRequest>): any {
    try {
        const { response }: IRace = yield race({
            response: call(authApi.login, action.payload),
            cancel: take(AuthSliceAction.failed.type),
        })
        yield fork(handleStorage, response)
        yield put(AuthSliceAction.success(response))
        yield call(handleNotify.success, "Login is successfully!")
    } catch (error) {
        yield put(AuthSliceAction.failed(error as string))
    }
}
function* handleRefreshToken() {
    const refreshToken = localStorage.getItem("refreshToken") as string
    try {
        const result: AuthResponse = yield call(authApi.refreshToken, refreshToken)
        yield call(handleStorage, result)
        yield put(AuthSliceAction.success(result))
    } catch (error) {
        yield put(AuthSliceAction.failed)
        yield call(handleLogout)
    }
}
function* handleRegister(action: PayloadAction<RegisterRequest>) {
    try {
        const result: AuthResponse = yield call(authApi.register, action.payload)
        yield call(handleStorage, result)
        yield put(AuthSliceAction.success(result))
    } catch (error) {
        yield put(AuthSliceAction.failed)
    }
}
function* handleStorage(result: AuthResponse) {
    const tokenExpirationDate = new Date(new Date().getTime() + 1000 * 60 * 60)
    yield localStorage.setItem(
        "currentUser",
        JSON.stringify({
            firstName: result.firstName,
            lastName: result.lastName,
            email: result.email,
            id: result.id,
        })
    )
    yield localStorage.setItem("token", JSON.stringify(result.token))
    yield localStorage.setItem("refreshToken", JSON.stringify(result.refreshToken))
    yield localStorage.setItem("tokenExpirationDate", JSON.stringify(tokenExpirationDate))
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
        const isLoggedIn = Boolean(localStorage.getItem("refreshToken"))
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
        const isLoggedIn = Boolean(localStorage.getItem("refreshToken"))
        if (isLoggedIn) {
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
