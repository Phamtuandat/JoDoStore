import { Icon } from "./icon"
import { Category } from "./category"

export interface Product {
    id: string | number
    name: string
    category: Category | null
    brand: Icon | null
    description: string | null
    price: number | null
    salePrice?: number | null
    imagePaths: string[]
    thumbnail: string
    tags?: string[]
    detail?: string
}

export type CartItemReq = {
    productId: number
    quantity: number
}

export type CartItem = {
    quantity: number
    productId: number
}

export type CartRes = {
    id: number | string
    items: CartItem[]
}
