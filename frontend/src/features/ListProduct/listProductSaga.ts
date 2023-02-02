import { PayloadAction } from "@reduxjs/toolkit"
import { productApi } from "ApiClients/ProductApi"
import { ListParams, ListResponse, Product } from "models"
import buildQuery from "odata-query"
import { call, debounce, put, takeLatest } from "redux-saga/effects"
import { ProductSliceActions } from "./listProductSlice"

function* getListProduct(action: PayloadAction<ListParams>) {
    try {
        const params = buildQuery(action.payload)
        const productList: ListResponse<Product> = yield call(productApi.getList, params)
        yield put(ProductSliceActions.getListSuccess(productList.data))
    } catch (error) {
        yield put(ProductSliceActions.getListFailed(error as string))
    }
}

function* handleFilterWithDebounce(action: PayloadAction<ListParams>) {
    yield put(ProductSliceActions.setFilter(action.payload))
}

export function* productListSaga() {
    yield takeLatest(ProductSliceActions.getList.type, getListProduct)
    yield debounce(1000, ProductSliceActions.setFilterWithDebounce.type, handleFilterWithDebounce)
}
