import { Brand } from "./brand"
import { Category } from "./category"
import { Tags } from "./tags"

export interface Product {
    id: string | number | null
    name: string
    categories: Category[] | []
    brand: Brand | null
    descriptions: string
    price: number | null
    priceSale?: number | null
    smallImageLink?: string
    thumbnail?: string
    tags?: Tags[] | []
}

export interface SaveProductReq {
    name: string | ""
    categories: number[]
    brand: number | null
    descriptions: string
    price: number | 0
    priceSale?: number | 0
    smallImageLink?: string
    thumbnail?: File[]
    tags?: Tags[] | []
}
