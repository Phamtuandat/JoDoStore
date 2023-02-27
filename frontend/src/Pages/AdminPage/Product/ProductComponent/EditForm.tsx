import { Box, Button, Grid, Paper, Typography } from "@mui/material"
import { brandApi } from "ApiClients/BrandApi"
import categoryApi from "ApiClients/CategoryApi"
import { productApi } from "ApiClients/ProductApi"
import { Brand, Category, Product } from "models"
import { useEffect, useRef, useState } from "react"
import { useForm } from "react-hook-form"
import { useLoaderData, useParams, useRevalidator } from "react-router-dom"
import handleNotify from "utils/Toast-notify"
import { SaveProductForm } from "../AddProductPage"
import ImageEditForm from "./ImageEditForm"
import Pricing from "./Pricing"
import ProductInfo from "./ProductInfo"

const EditForm = () => {
    const ignore = useRef(false)
    const product = useLoaderData() as Product
    const revalidator = useRevalidator()
    const [state, setState] = useState<{
        categories: Category[]
        brands: Brand[]
        isLoading: boolean
    }>({
        categories: [],
        brands: [],
        isLoading: true,
    })
    const { control, handleSubmit, setValue, reset } = useForm<SaveProductForm>({
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
    })
    useEffect(() => {
        if (!ignore.current) {
            ignore.current = true
            ;(async () => {
                const result = await categoryApi.getAll()
                const categoryList = result.data
                const BrandList = (await brandApi.getAll()).data
                setState({
                    categories: categoryList,
                    brands: BrandList,
                    isLoading: false,
                })
            })()
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])
    const { productId } = useParams()
    useEffect(() => {
        revalidator.revalidate()
        console.log(!!product.description)
        reset({
            name: product.name,
            brand: product.brand?.name,
            category: product.category?.name,
            description: product.description,
            price: product.price,
            salePrice: product.salePrice,
            smallImageLink: product.smallImageLink,
            thumbnail: undefined,
            tags: product.tags?.map((tag) => tag.name),
        })
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [productId, reset])

    const onSubmit = async (value: SaveProductForm) => {
        const formData = new FormData()

        formData.append("name", value.name)
        formData.append("descriptions", `${value.description}`)
        formData.append("category.name", value.category || "No Category")
        formData.append("salePrice", `${value.salePrice}`)
        formData.append("price", `${value.price}`)
        formData.append("brand.name", `${value.brand}`)
        if (value.tags) {
            value.tags.forEach((tag, i) => {
                formData.append(`tags[${i}]`, `${tag}`)
            })
        }
        if (value.thumbnail) {
            value.thumbnail.forEach((th) => {
                formData.append("media", th)
            })
        }
        try {
            if (productId) {
                await productApi.update(formData, +productId)
                handleNotify.success("Edit Product Successfully")
            }
        } catch (error) {
            handleNotify.error(error as string)
        }
    }

    return (
        <Box
            display="flex"
            mt={2}
            flexDirection="column"
            p={1}
            onSubmit={handleSubmit(onSubmit)}
            component="form"
        >
            <Box
                display="flex"
                justifyContent="space-between"
                alignItems="center"
                flexDirection={{ md: "column", lg: "row" }}
            >
                <Box maxWidth={"480px"}>
                    <Typography variant="h6" fontWeight={600} my={2}>
                        Make the changes below
                    </Typography>
                    <Typography
                        variant="body2"
                        sx={{
                            opacity: 0.6,
                        }}
                    >
                        We{"’"} re constantly trying to express ourselves and actualize our dreams.
                        If you have the opportunity to play.
                    </Typography>
                </Box>
                <Box my={2}>
                    <Button type="submit" variant="contained" size="small">
                        Save
                    </Button>
                </Box>
            </Box>
            <Box width="100%" mt={5} justifyContent="center">
                <Grid container spacing={1}>
                    <Grid item sm={12} md={6} lg={4} sx={{ width: "100%", height: "100%" }}>
                        <ImageEditForm control={control} product={product} productId={productId} />
                    </Grid>
                    <Grid item md={6} lg={8} sm={12} mx="auto" width="100%">
                        <Paper
                            sx={{
                                px: 2,
                                pt: 3,
                                pb: 4,
                                display: "flex",
                                flexDirection: "column",
                                height: "100%",
                            }}
                        >
                            <Typography component="span" variant="h6" mx="auto" mb={-2}>
                                Product Information
                            </Typography>
                            {!!product.description && !state.isLoading && (
                                <ProductInfo
                                    control={control}
                                    setValue={setValue}
                                    brands={state.brands}
                                    categories={state.categories}
                                />
                            )}
                        </Paper>
                    </Grid>
                    <Grid item md={12} sm={12} xs={12}>
                        <Paper sx={{ mt: "10px", py: 1, width: "100%" }} elevation={1}>
                            <Pricing setValue={setValue} control={control} />
                        </Paper>
                    </Grid>
                </Grid>
            </Box>
        </Box>
    )
}
export default EditForm
