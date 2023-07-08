import { PayloadAction } from "@reduxjs/toolkit"
import { cartApi } from "ApiClients/CartApi"
import { AxiosResponse } from "axios"
import { CartItemReq, CartRes } from "models"
import { call, fork, put, take } from "redux-saga/effects"
import { CartItems, cartSliceAction } from "./cartSlice"

function* getCart() {
    try {
        const action: PayloadAction<string> = yield take(cartSliceAction.getCart.type)
        const response: AxiosResponse<CartRes> = yield call(cartApi.getCart, action.payload)
        yield put(cartSliceAction.getCartSuccess(response.data.items))
    } catch (error) {
        yield put(cartSliceAction.handleReqFailure)
    }
}

function* addFlow() {
    while (true) {
        const action: PayloadAction<CartItems> = yield take(cartSliceAction.addToCart.type)
        try {
            const param: CartItemReq = {
                productId: +action.payload.productId,
                quantity: action.payload.quantity,
            }
            const token = JSON.parse(
                JSON.parse(localStorage.getItem("persist:root") as string).auth
            ).token
            yield call(cartApi.addItem, param, token)
            yield put(cartSliceAction.addToCartSuccess(action.payload))
        } catch (error) {
            yield put(cartSliceAction.handleReqFailure(error as string))
        }
    }
}
function* removeItem() {
    while (true) {
        const action: PayloadAction<string | number> = yield take(
            cartSliceAction.removeFromCart.type
        )
        const token = JSON.parse(
            JSON.parse(localStorage.getItem("persist:root") as string).auth
        ).token
        try {
            yield call(cartApi.removeItem, +action.payload, token)
            yield put(cartSliceAction.removeSuccess(action.payload))
        } catch (error) {
            yield put(cartSliceAction.handleReqFailure(error as string))
        }
    }
}

export function* cartSaga() {
    yield fork(addFlow)
    yield fork(removeItem)
    yield call(getCart)
}
