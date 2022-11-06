import Box from "@mui/material/Box/Box"
import categoryApi from "ApiClients/CategoryApi"
import { MainLayout } from "components/Layout/MainLayout"
import { Category } from "models"
import { useEffect, useState } from "react"
import CategorySlide from "./components/CategorySlide"
import FlashDeal from "./components/FlashDeal"
import ListProductSale from "./components/ListProduct"
import { BanerSwiper } from "./components/BanerSwiper"

export default function HomePage() {
    const [state, setState] = useState<Category[]>([])
    useEffect(() => {
        ;(async () => {
            try {
                const result = await categoryApi.getAll()
                setState(result.data)
            } catch (error) {}
        })()
    }, [])
    return (
        <MainLayout>
            <BanerSwiper categories={state} />
            <CategorySlide categories={state} />
            <FlashDeal />
            <Box>
                <ListProductSale />
            </Box>
        </MainLayout>
    )
}
