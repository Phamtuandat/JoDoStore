export interface Category {
    id: number | string
    name: string
    childCategories: []
    description: string
    parentCategory: null | Category[]
    parentCategoryId: Category
    slug: string
}
