import { createSlice, PayloadAction } from "@reduxjs/toolkit"
import { RootState } from "app/store"
import { Product } from "models"
import handleNotify from "utils/Toast-notify"

export type CartItems = {
    product: Product
    quantity: number
}

export interface ICart {
    cartItems: CartItems[]
    processing: boolean
    error: string | null
}

const initialState: ICart = {
    cartItems: [],
    processing: false,
    error: null,
}

const cartSlice = createSlice({
    name: "Cart",
    initialState: initialState,
    reducers: {
        addToCart(state, action: PayloadAction<CartItems>) {
            state.processing = true
        },
        addToCartSuccess(state, action: PayloadAction<CartItems>) {
            state.processing = false
            const newItem = action.payload
            const index = state.cartItems.findIndex((x) => x.product.id === newItem.product.id)
            if (index >= 0) {
                state.cartItems[index].quantity += newItem.quantity
            } else {
                state.cartItems.push(newItem)
            }
            handleNotify.success(`successfully added to cart!`)
        },
        getCart(state) {
            state.processing = true
        },
        getCartSuccess(state, action: PayloadAction<CartItems[]>) {
            state.processing = false
            state.cartItems = action.payload
        },

        handleReqFailure(state, action: PayloadAction<string>) {
            state.processing = false
            state.error = action.payload
            handleNotify.error("Something went wrong, please try again later!")
        },
        setQuantity(state, action: PayloadAction<CartItems>) {
            const index = state.cartItems.findIndex(
                (x) => x.product.id === action.payload.product.id
            )
            if (index >= 0) {
                state.cartItems[index].quantity = action.payload.quantity
            }
        },
        removeFromCart(state, action: PayloadAction<string | number>) {
            state.processing = true
        },
        removeSuccess(state, action: PayloadAction<string | number>) {
            const idNeedRemove = action.payload
            state.cartItems = state.cartItems.filter((x) => x.product.id !== idNeedRemove)
        },
        removeAllCartItem(state) {
            state.cartItems = []
        },
    },
})

export const cartSelector = (state: RootState) => state.cart.cartItems
export const cartProcessing = (state: RootState) => state.cart.processing
export const cartSliceAction = cartSlice.actions
const cartReducer = cartSlice.reducer
export default cartReducer
