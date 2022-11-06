import { PayloadAction } from "@reduxjs/toolkit"
import { ListParams } from "models"
import { call, take } from "redux-saga/effects"
import { ProductSliceActions } from "./listProductSlice"

function* getListProduct(action: PayloadAction<ListParams>) {
    yield
}

export function* productListSaga() {
    const action: PayloadAction<ListParams> = yield take(ProductSliceActions.getList.type)
    yield call(getListProduct, action)
}
