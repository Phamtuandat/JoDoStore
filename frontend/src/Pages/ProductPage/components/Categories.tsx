import { List } from "@mui/material"
import Box from "@mui/material/Box"
import Checkbox from "@mui/material/Checkbox"
import FormControlLabel from "@mui/material/FormControlLabel"
import categoryApi from "ApiClients/CategoryApi"
import { Category } from "models"
import { useEffect, useState } from "react"

export default function Categories({
    handleCategoryChange,
}: {
    handleCategoryChange: (id: string | number, value: boolean) => void
}) {
    const [categories, setCategories] = useState<Category[]>()

    useEffect(() => {
        ;(async () => {
            const categoryList = (await categoryApi.getAll()).data
            setCategories(categoryList.filter((c) => c.parentCategory === null))
        })()
    }, [])

    const handleFormControlChange = (id: string | number, isChecked: boolean) => {
        handleCategoryChange(id, isChecked)
    }
    return <>{RenderCategory(categories, 0, handleFormControlChange)}</>
}

function RenderCategory(
    categories?: Category[],
    level?: number,
    onFormControlChange?: (id: string | number, isChecked: boolean) => void
) {
    if (categories) {
        return (
            <>
                {categories.map((category) => (
                    <CategoryItem
                        key={category.id}
                        category={category}
                        level={level || 0}
                        onFormControlChange={onFormControlChange}
                    />
                ))}
            </>
        )
    }
}

function CategoryItem({
    category,
    level,
    onFormControlChange,
}: {
    category: Category
    level: number
    onFormControlChange?: (id: string | number, isChecked: boolean) => void
}) {
    const [checked, setChecked] = useState<{ [key: string]: boolean }>()

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>, id: string | number) => {
        setChecked({
            ...checked,
            [id]: !checked ? true : !checked[id],
        })
        if (onFormControlChange) {
            onFormControlChange(id, !checked ? true : !checked[id])
        }
    }

    return (
        <Box ml={level * 3}>
            {level === 0 && (
                <FormControlLabel
                    label={category.name}
                    control={<Checkbox onChange={(e) => handleChange(e, category.id)} />}
                />
            )}
            <List component="div" disablePadding>
                {level !== 0 && (
                    <FormControlLabel
                        label={category.name}
                        control={<Checkbox onChange={(e) => handleChange(e, category.id)} />}
                    />
                )}
                {category.childCategories &&
                    RenderCategory(category.childCategories, level || 0 + 1, onFormControlChange)}
            </List>
        </Box>
    )
}
