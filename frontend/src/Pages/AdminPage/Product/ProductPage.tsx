import { Box } from "@mui/material"
import { Outlet } from "react-router-dom"

type Props = {}

const ProductPage = (props: Props) => {
    return (
        <Box>
            <Outlet />
        </Box>
    )
}

export default ProductPage
