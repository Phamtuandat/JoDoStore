import { yupResolver } from "@hookform/resolvers/yup"
import { Box } from "@mui/material"
import { brandApi } from "ApiClients/BrandApi"
import categoryApi from "ApiClients/CategoryApi"
import AdminStepper from "components/AdminStepper"
import { Brand, Category } from "models"
import { ReactNode, useEffect, useState } from "react"
import { useForm } from "react-hook-form"
import * as yup from "yup"
import { SaveProductForm } from "../AddProductPage"
import Pricing from "./Pricing"
import ProductInfo from "./ProductInfo"
import MediaForm from "./MediaForm"
type Props = {
    handleSubmitForm: (value: SaveProductForm) => void
}
interface TabPanelProps {
    children?: ReactNode
    index: number
    value: number
}

function TabPanel(props: TabPanelProps) {
    const { children, value, index, ...other } = props

    return (
        <div
            role="tabpanel"
            hidden={value !== index}
            id={`simple-tabpanel-${index}`}
            aria-labelledby={`simple-tab-${index}`}
            {...other}
        >
            {value === index && <Box sx={{ p: 3 }}>{children}</Box>}
        </div>
    )
}

const schema = yup
    .object({
        name: yup.string().required(),
        brand: yup.string().required(),
        category: yup.string().required(),
        price: yup.number().positive().integer().required(),
        salePrice: yup.number().positive().integer().required(),
        thumbnail: yup
            .mixed()
            .required("You need to provide a file")
            .test(
                "type",
                "Only the following formats are accepted: .jpeg, .jpg, .bmp, .pdf and .doc",
                (value) => {
                    return (
                        value &&
                        (value[0].type === "image/jpeg" ||
                            value[0].type === "image/bmp" ||
                            value[0].type === "image/png" ||
                            value[0].type === "application/pdf" ||
                            value[0].type === "image/webp" ||
                            value[0].type === "application/msword")
                    )
                }
            ),
        tags: yup.array().required(),
    })
    .required()

function ProductForm({ handleSubmitForm }: Props) {
    const [categories, setCategories] = useState<Category[]>([])
    const [brands, setBrands] = useState<Brand[]>([])
    const [step, setActiveStep] = useState(0)

    const {
        control,
        handleSubmit,
        setValue,
        getValues,
        formState: { errors, isSubmitted },
        clearErrors,
        reset,
    } = useForm<SaveProductForm>({
        defaultValues: {
            name: "",
            brand: null,
            category: null,
            description: null,
            price: 0,
            salePrice: 0,
            smallImageLink: "",
            thumbnail: undefined,
            tags: [],
        },
        resolver: yupResolver(schema),
    })
    useEffect(() => {
        ;(async () => {
            const result = await categoryApi.getAll()
            const categoryList = result.data
            const BrandList = await brandApi.getAll()
            setBrands(BrandList.data)
            setCategories(categoryList)
        })()
    }, [isSubmitted])

    const handleFormSubmit = (value: SaveProductForm) => {
        handleSubmitForm(value)
    }

    // eslint-disable-next-line react-hooks/exhaustive-deps
    return (
        <>
            <AdminStepper
                handleNextStep={(step) => setActiveStep(step)}
                handleFormSubmit={handleSubmit(handleFormSubmit)}
                errors={errors}
                isSubmitted={isSubmitted}
                handleResetForm={() => reset()}
            >
                <Box
                    sx={{
                        width: "100%",
                    }}
                >
                    <Box component={TabPanel} value={step} index={0}>
                        <ProductInfo
                            control={control}
                            setValue={setValue}
                            brands={brands}
                            categories={categories}
                        />
                    </Box>
                    <Box
                        component={TabPanel}
                        value={step}
                        index={1}
                        display="flex"
                        flexDirection="column"
                        justifyContent="center"
                    >
                        <MediaForm
                            control={control}
                            setValue={(value) => {
                                setValue("thumbnail", value)
                                clearErrors("thumbnail")
                            }}
                            value={getValues("thumbnail")}
                        />
                    </Box>
                    <Box
                        component={TabPanel}
                        value={step}
                        index={2}
                        display="flex"
                        flexDirection="column"
                        justifyContent="center"
                    >
                        <Pricing setValue={setValue} control={control} />
                    </Box>
                </Box>
            </AdminStepper>
        </>
    )
}

export default ProductForm
