import { AxiosHeaders } from "axios"

export interface ListResponse<T> {
    data: T[]
    headers: headers
}

interface headers extends AxiosHeaders {
    "x-pagination": string
}
export interface PaginationMetadata {
    currentPage: number
    pageSize: number
    totalItems: number
    totalPages: number
}
export interface ListParams {
    pageSize?: number
    orderBy?: string
    name?: string
    currentPage?: number
    categoryIds?: number[]
    iconId?: number[]
    maxPrice?: number
    minPrice?: number
    [key: string]: any
}
/**
 public string? Name { get; set; }
            public decimal? MinPrice { get; set; }
            public decimal? MaxPrice { get; set; }
            public int[]? CategoryIds { get; set; }
            public string? OrderBy { get; set; }
            public int? IConId { get; set; }
            public int PageSize { get; set; } = 50;
            public int CurrentPage { get; set; }
 */
export interface Option {
    id: number
    name: string
}

export interface AuthResponse<T> {
    data: T
}
