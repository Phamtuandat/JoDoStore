import { Brand } from "./brand"
import { Category } from "./category"

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
}
