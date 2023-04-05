import { PayloadAction } from "@reduxjs/toolkit"
import { cartApi } from "ApiClients/CartApi"
import { AxiosResponse } from "axios"
import { CartItemReq, CartRes } from "models"
import { call, fork, put, take } from "redux-saga/effects"
import { CartItems, cartSliceAction } from "./cartSlice"

function* getCart() {
    try {
        yield take(cartSliceAction.getCart.type)
        const response: AxiosResponse<CartRes> = yield call(cartApi.getCart)
        const data = response.data
        const action: CartItems[] = data.items.map((x) => ({
            quantity: x.quantity,
            product: x.product,
        }))
        yield put(cartSliceAction.getCartSuccess(action))
    } catch (error) {
        yield put(cartSliceAction.handleReqFailure)
    }
}

function* addFlow() {
    while (true) {
        const action: PayloadAction<CartItems> = yield take(cartSliceAction.addToCart.type)
        try {
            const param: CartItemReq = {
                id: +action.payload.product.id,
                quantity: action.payload.quantity,
            }
            yield call(cartApi.addItem, param)
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
        try {
            yield call(cartApi.removeItem, +action.payload)
            yield put(cartSliceAction.removeSuccess(action.payload))
        } catch (error) {
            yield put(cartSliceAction.handleReqFailure(error as string))
        }
    }
}

export function* cartSaga() {
    yield fork(addFlow)
    yield fork(removeItem)
    yield fork(getCart)
}
