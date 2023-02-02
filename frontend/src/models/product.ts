import { Brand } from "./brand"
import { Category } from "./category"
import { Tag } from "./tags"

export interface Thumbnail {
    id: number | string
    title: string
    imageUrl: string
    productId: number
}
export interface SaveThumbReq {
    title: string
    productId?: number
    thumbnail: File
}

export interface Product {
    id: string | number | null
    name: string
    category: Category | null
    brand: Brand | null
    description: string | null
    price: number | null
    salePrice?: number | null
    smallImageLink?: string
    thumbnails: Thumbnail[] | []
    tags?: Tag[] | []
}

export interface SaveProductReq {
    id?: number | string | null
    name: string | ""
    category: Category | null
    brand: Brand | null
    description: string | null
    price: number | null
    salePrice: number | null
    smallImageLink?: string
    thumbnail?: File[] | []
    tags?: Tag[] | []
}
