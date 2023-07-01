import { createSlice, PayloadAction } from "@reduxjs/toolkit"
import { RootState } from "app/store"
import { ListParams, Product } from "models"

type ProductState = {
    processing: boolean
    productList: Product[] | []
    error?: string | null
    query: ListParams
    searchProductList: Product[] | []
}

const initialState: ProductState = {
    processing: false,
    productList: [],
    searchProductList: [],
    error: null,
    query: {
        top: 0,
    },
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
            state.productList = action.payload
        },
        getListFailed(state, action: PayloadAction<string>) {
            state.error = action.payload
            state.processing = false
        },
        setFilter(state, action: PayloadAction<ListParams>) {
            state.query = action.payload
        },
        setFilterWithDebounce(state, action: PayloadAction<ListParams>) {
            state.processing = true
        },
        resetFilter(state) {
            state.productList = []
            state.query = {
                top: 0,
            }
        },
    },
})

export const ProductSliceActions = productSlice.actions

export const productListSelector = (state: RootState) => state.product.productList
export const processingSeclector = (state: RootState) => state.product.processing
export const errorGetProductSelector = (state: RootState) => state.product.error
export const filterSelector = (state: RootState) => state.product.query
const productReducer = productSlice.reducer
export default productReducer
