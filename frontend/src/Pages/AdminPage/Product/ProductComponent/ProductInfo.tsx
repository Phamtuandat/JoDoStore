import { Box } from "@mui/material"
import { InputField } from "components/inputField"
import SelectTextFields from "components/inputField/SelectField"
import { convertToRaw, EditorState } from "draft-js"
import "draft-js/dist/Draft.css"
import { Brand, Category } from "models"
import { useEffect, useState } from "react"
import { Control } from "react-hook-form"
import TextEditTor from "../../components/TextEditTor"
type Props = {
    setValue: (value: string) => void
    control: Control<any>
    brands: Brand[]
    categories: Category[]
}

const ProductInfo = ({ setValue, control, categories, brands }: Props) => {
    const [editorState, setEditorState] = useState<EditorState>()

    useEffect(() => {
        if (editorState) {
            setValue(JSON.stringify(convertToRaw(editorState.getCurrentContent())))
        }
    }, [editorState, setValue])

    return (
        <Box
            sx={{
                mt: 5,
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
                    fullWidth={true}
                />
                <TextEditTor handleChange={(value) => setEditorState(value)} />
            </Box>
            <Box
                sx={{
                    width: "50%",
                    p: 2,
                }}
            >
                <SelectTextFields
                    control={control}
                    name="categories"
                    label="Categories"
                    disabled={false}
                    options={categories}
                    isMutiple={true}
                />
                <SelectTextFields
                    control={control}
                    name="brand"
                    label="Brand"
                    disabled={false}
                    options={brands}
                    isMutiple={false}
                />
            </Box>
        </Box>
    )
}

export default ProductInfo
