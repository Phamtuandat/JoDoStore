import { Box } from "@mui/material"
import BannerImageList from "./components/BannerImageList"

const UIPage = () => {
    return (
        <Box
            sx={{
                py: 5,
                px: 2,
            }}
        >
            <Box>
                <BannerImageList title="Banner" />
            </Box>
        </Box>
    )
}

export default UIPage
