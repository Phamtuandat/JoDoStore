import Box from "@mui/material/Box"
import { useTheme } from "@mui/material/styles"
import Footer from "components/Footer"
import Header from "components/common/Header"
import Loading from "components/common/Loading"
import BasicSpeedDial from "components/common/SpeedDial"
import { useEffect, useState } from "react"
import { useLocation } from "react-router-dom"
import { ToastContainer } from "react-toastify"
type IProps = {
    children?: React.ReactNode
}
export const MainLayout = (props: IProps) => {
    const [isLoading, setLoading] = useState(true)
    const location = useLocation()
    const theme = useTheme()
    useEffect(() => {
        const timeout = setTimeout(() => {
            setLoading(false)
        }, 2000)

        return () => {
            clearTimeout(timeout)
        }
    }, [location.pathname])
    return (
        <Box display="flex" flexDirection="column" bgcolor={theme.palette.background.default}>
            <ToastContainer
                position="bottom-right"
                autoClose={1000}
                hideProgressBar={true}
                newestOnTop={false}
                closeOnClick
                rtl={false}
                pauseOnFocusLoss
                draggable
                pauseOnHover
                theme={theme.palette.mode}
                limit={1}
            />
            <Box flexDirection="column" display="flex" color="text.primary">
                <Header />
                <Box>{!isLoading ? props.children : <Loading />}</Box>
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
