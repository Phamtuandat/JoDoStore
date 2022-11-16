import { Brand } from "./brand"
import { Tags } from "./tags"

export interface Media {
    Id: number | string
    Title: string
    thumbnailPath: string
}

export interface Product {
    id: string | number | null
    name: string
    categories: number
    brand: Brand | null
    descriptions: string
    price: number | null
    priceSale?: number | null
    smallImageLink?: string
    mediaResource: Media[]
    tags?: Tags[] | []
}

export interface SaveProductReq {
    name: string | ""
    categories: number | null
    brand: number | null
    descriptions: string
    price: number | 0
    priceSale?: number | 0
    smallImageLink?: string
    thumbnail?: File[]
    tags?: Tags[] | []
}
