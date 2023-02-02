import { Filter } from "odata-query"

export interface PaginationParams {
    _limit: number
    _page: number
    _totalRows: number
}

export interface ListResponse<T> {
    data: T[]
    pagination: PaginationParams
}
export interface ListParams {
    top?: number
    orderBy?: string
    filter?: IFilter
    levels?: number | "max"
    skip?: number
    count?: boolean | Filter
    [key: string]: any
}

export interface AuthResponse<T> {
    data: T
}

export interface IFilter {
    category?:
        | { name?: string | undefined | { in?: string[] }; id?: string | number | undefined }
        | { [key: string]: any }
    brand?:
        | { name?: string | undefined | { in?: string[] }; id?: string | number | undefined }
        | { [key: string]: any }
    salePrice?: {
        gt?: number | undefined
        lt?: number | undefined
    }
    [key: string]: any
}
