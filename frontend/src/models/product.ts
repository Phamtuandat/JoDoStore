import { Brand } from "./brand"
import { Category } from "./category"
import { Tag } from "./tags"

export interface Photo {
    id: number | string
    title: string
    imageUrl: string
    productId: number
}
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
    smallImageLink?: string
    thumbnails: Photo[] | []
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
