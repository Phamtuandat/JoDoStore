/* eslint-disable no-throw-literal */
import { call, fork, put, take } from "@redux-saga/core/effects"
import { PayloadAction } from "@reduxjs/toolkit"
import authApi from "ApiClients/AuthApi"
import { AuthResponse, AuthenticateInfo, LoginRequest, RegisterRequest } from "models"
import { Task } from "redux-saga"
import { cancel, cancelled } from "redux-saga/effects"
import handleNotify from "utils/Toast-notify"
import { AuthSliceAction } from "./authSlice"

function* handleLogin(action: PayloadAction<LoginRequest>): any {
    try {
        const response = yield call(authApi.login, action.payload)
        if (response) {
            yield put(AuthSliceAction.success(response.data))
            yield call(handleNotify.success, "Login is successfully!")
        } else {
            throw new Error("Somthing went wrong!")
        }
    } catch (error) {
        yield put(AuthSliceAction.failed(error as string))
    } finally {
        if (yield cancelled()) {
            console.log("login cancelled")
        }
    }
}

function* handleRegister(action: PayloadAction<RegisterRequest>): any {
    try {
        const result: AuthResponse<AuthenticateInfo> = yield call(authApi.register, action.payload)
        yield put(AuthSliceAction.success(result.data))
        yield call(handleNotify.success, "Register is successfully!")
    } catch (error) {
        yield put(AuthSliceAction.failed)
    } finally {
        if (yield cancelled()) {
            console.log("register cancelled")
        }
    }
}

function* handleLogout() {
    yield call(authApi.logout)
    yield put(AuthSliceAction.logout)
}
function* watchLogingFlow() {
    while (true) {
        const loginAction: PayloadAction<LoginRequest> = yield take(AuthSliceAction.login.type)
        const task: Task = yield fork(handleLogin, loginAction)
        const action: string = yield take([
            AuthSliceAction.logout.type,
            AuthSliceAction.failed.type,
        ])
        if (action === AuthSliceAction.logout.type) {
            yield cancel(task)
        }
        yield call(handleLogout)
    }
}
function* watchRegisterFlow() {
    while (true) {
        const registerAction: PayloadAction<RegisterRequest> = yield take(
            AuthSliceAction.register.type
        )
        const task: Task = yield fork(handleRegister, registerAction)
        const action: string = yield take([
            AuthSliceAction.failed.type,
            AuthSliceAction.logout.type,
        ])
        if (action === AuthSliceAction.logout.type) {
            yield cancel(task)
        }
        yield call(handleLogout)
    }
}
function* watchLogoutFlow() {
    while (true) {
        yield take(AuthSliceAction.logout.type)
        yield call(handleLogout)
    }
}
export default function* authSaga() {
    yield fork(watchLogingFlow)
    yield fork(watchRegisterFlow)
    yield fork(watchLogoutFlow)
}
