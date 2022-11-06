import { createSlice, PayloadAction } from "@reduxjs/toolkit"
import { ListParams, Product } from "models"

type ProductState = {
    processing: boolean
    ListProduct?: Product[] | []
    error?: string
}

const initialState: ProductState = {
    processing: false,
    ListProduct: [],
    error: undefined,
}

const productSlice = createSlice({
    name: "productSlice",
    initialState,
    reducers: {
        getList(state, action: PayloadAction<ListParams>) {
            state.processing = true
        },
        getListSuccess(state, action: PayloadAction<Product[]>) {
            state.processing = false
            state.ListProduct = action.payload
        },
        getListFailed(state, action: PayloadAction<string>) {
            state.error = action.payload
            state.processing = false
        },
        getListDebounce(state, action: PayloadAction<ListParams>) {
            state.processing = true
        },
    },
})

export const ProductSliceActions = productSlice.actions

export const productListSelector = (state: any) => state.listProduct
export const processingSeclector = (state: any) => state.processing
export const errorGetProductSelector = (state: any) => state.error

const productReducer = productSlice.reducer
export default productReducer
