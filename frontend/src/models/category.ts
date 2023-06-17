export interface Category {
    id: number | string
    name: string
    childCategories: Category[] | []
    description: string
    parentCategory: undefined | Category
    parentCategoryId: Category
    slug: string
}
