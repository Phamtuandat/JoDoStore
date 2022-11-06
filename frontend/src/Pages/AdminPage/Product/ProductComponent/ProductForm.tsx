import { Box, Button } from "@mui/material"
import { InputField } from "components/inputField"
import SelectTextFields from "components/inputField/SelectField"
import { EditorState, convertToRaw } from "draft-js"
import "draft-js/dist/Draft.css"
import { Category, Product } from "models"
import { useEffect, useState } from "react"
import { useForm } from "react-hook-form"
import TextEditTor from "../../components/TextEditTor"
type Props = {
    handleSubmitForm: (value: Product) => void
}

const ProductForm = ({ handleSubmitForm }: Props) => {
    const [editorState, setEditorState] = useState<EditorState>()
    const { control, handleSubmit, setValue } = useForm<Product>({
        defaultValues: {
            name: "",
            brand: null,
            categories: [],
            descriptions: "",
            id: "",
            price: null,
            priceSale: null,
            smallImageLink: "",
            thumbnail: "",
        },
    })
    const handleFormSubmit = (value: Product) => {
        handleSubmitForm(value)
    }
    useEffect(() => {
        if (editorState) {
            setValue("descriptions", JSON.stringify(convertToRaw(editorState.getCurrentContent())))
        }
    }, [editorState, setValue])

    return (
        <Box component="form" onSubmit={handleSubmit(handleFormSubmit)}>
            <Box
                sx={{
                    mt: 5,
                    minHeight: "350px",
                }}
                display="flex"
            >
                <Box
                    sx={{
                        width: "50%",
                        p: 2,
                    }}
                >
                    <InputField
                        control={control}
                        name="name"
                        label="Name"
                        disabled={false}
                        variant="standard"
                        autoComplete="new-password"
                    />
                    <TextEditTor handleChange={(value) => setEditorState(value)} />
                </Box>
                <Box
                    sx={{
                        width: "50%",
                        p: 2,
                    }}
                >
                    <InputField
                        control={control}
                        name="brand"
                        label="Brand"
                        disabled={false}
                        variant="standard"
                        autoComplete="new-password"
                    />
                    <SelectTextFields
                        control={control}
                        name="categories"
                        label="Categories"
                        disabled={false}
                        options={categories}
                        isMutiple={true}
                    />
                </Box>
            </Box>
            <Box textAlign="right">
                <Button type="submit" variant="contained">
                    NEXT
                </Button>
            </Box>
        </Box>
    )
}

export default ProductForm
const categories: Category[] = [
    {
        name: "Clothing",
    },
    {
        name: "Electronics",
    },
    {
        name: "Furniture",
    },
    {
        name: "Orders",
    },
]
