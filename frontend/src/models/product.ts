import { Brand } from "./brand"
import { Category } from "./category"

export interface SavePhotoReq {
    title: string
    productId?: number
    FormFile: File | null
    collections: string
}

export interface Product {
    id: string | number
    name: string
    category: Category | null
    brand: Brand | null
    description: string | null
    price: number | null
    salePrice?: number | null
    imagePaths: string[]
    thumbnail: string
    tags?: string[]
}

export type CartItemReq = {
    id: number
    quantity: number
}

export type CartItem = {
    id: number
    product: Product
    quantity: number
    productId: number
}

export type CartRes = {
    id: number | string
    items: CartItem[]
}
