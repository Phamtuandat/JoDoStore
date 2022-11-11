import { Box } from "@mui/material"
import { brandApi } from "ApiClients/BrandApi"
import categoryApi from "ApiClients/CategoryApi"
import AdminStepper from "components/AdminStepper"
import { Brand, Category, SaveProductReq } from "models"
import { ReactNode, useEffect, useState } from "react"
import { useForm } from "react-hook-form"
import Pricing from "./Pricing"
import ProductInfo from "./ProductInfo"
import ProductMedia from "./ProductMedia"

type Props = {
    handleSubmitForm: (value: SaveProductReq) => void
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

function ProductForm({ handleSubmitForm }: Props) {
    const [categories, setCategories] = useState<Category[]>([])
    const [brands, setBrands] = useState<Brand[]>([])
    const [step, setActiveStep] = useState(0)

    const { control, handleSubmit, setValue, getValues } = useForm<SaveProductReq>({
        defaultValues: {
            name: "",
            brand: null,
            categories: [],
            descriptions: "",
            price: 0,
            priceSale: 0,
            smallImageLink: "",
            thumbnail: undefined,
            tags: [],
        },
    })
    useEffect(() => {
        ;(async () => {
            const result = await categoryApi.getAll()
            const categoryList = result.data
            const BrandList = await brandApi.getAll()
            setBrands(BrandList.data)
            setCategories(categoryList)
        })()
    }, [])
    const handleFormSubmit = (value: SaveProductReq) => {
        handleSubmitForm(value)
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
    return (
        <>
            <AdminStepper
                handleNextStep={(step) => setActiveStep(step)}
                handleFormSubmit={handleSubmit(handleFormSubmit)}
            >
                <Box sx={{ width: "100%" }}>
                    <Box component={TabPanel} value={step} index={0}>
                        <ProductInfo
                            control={control}
                            setValue={(value) => setValue("descriptions", value)}
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
                        <ProductMedia
                            control={control}
                            setValue={(value) => setValue("thumbnail", value)}
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
                        <Pricing control={control} />
                    </Box>
                </Box>
            </AdminStepper>
        </>
    )
}

export default ProductForm
