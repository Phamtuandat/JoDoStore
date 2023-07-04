import { createSelector } from "@reduxjs/toolkit"
import { RootState } from "app/store"

const cartItemSelector = (state: RootState) => state.cart.cartItems

export const countItems = createSelector(cartItemSelector, (cartItems) =>
    cartItems.reduce((count, item) => count + 1, 0)
)

export const cartTotalSelector = createSelector(cartItemSelector, (cartItems) =>
    cartItems.reduce((total, item) => total + item.quantity * (item.salePrice || 0), 0)
)
