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
    showMiniCart: boolean
}

const initialState: ICart = {
    cartItems: [],
    showMiniCart: false,
}

const cartSlice = createSlice({
    name: "Cart",
    initialState: initialState,
    reducers: {
        showMiniCart(state) {
            state.showMiniCart = true
        },
        hideMiniCart(state) {
            state.showMiniCart = false
        },
        addToCart(state, action: PayloadAction<CartItems>) {
            const newItem = action.payload
            const index = state.cartItems.findIndex((x) => x.product.id === newItem.product.id)
            if (index >= 0) {
                state.cartItems[index].quantity += newItem.quantity
            } else {
                state.cartItems.push(newItem)
            }
            handleNotify.success(`successfully added to cart!`)
        },
        setQuantity(state, action: PayloadAction<CartItems>) {
            const index = state.cartItems.findIndex(
                (x) => x.product.id === action.payload.product.id
            )
            if (index >= 0) {
                state.cartItems[index].quantity = action.payload.quantity
            }
        },
        removeFromCart(state, action) {
            const idNeedRemove = action.payload
            state.cartItems = state.cartItems.filter((x) => x.product.id !== idNeedRemove)
        },
    },
})

export const showMiniCartSelector = (state: RootState) => state.cart.showMiniCart
export const cartSelector = (state: RootState) => state.cart.cartItems
export const cartAction = cartSlice.actions
const cartReducer = cartSlice.reducer
export default cartReducer
