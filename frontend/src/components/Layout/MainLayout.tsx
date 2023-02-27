import Box from "@mui/material/Box"
import { useTheme } from "@mui/material/styles"
import Footer from "components/Footer"
import Header from "components/common/Header"
import BasicSpeedDial from "components/common/SpeedDial"
type IProps = {
    children?: React.ReactNode
}
export const MainLayout = (props: IProps) => {
    const theme = useTheme()

    return (
        <Box display="flex" flexDirection="column" bgcolor={theme.palette.background.default}>
            <Box flexDirection="column" display="flex" color="text.primary">
                <Box>
                    <Header />
                </Box>
                <Box>{props.children}</Box>
                <Box sx={{ position: "fixed", bottom: 0, left: 0, right: 15, zIndex: 100 }}>
                    <BasicSpeedDial label="Actions" />
                </Box>
                <Box mt="auto" justifyContent="center" display="flex">
                    <Footer />
                </Box>
            </Box>
        </Box>
    )
}
