import { Box, Divider, Drawer, Hidden, Paper } from "@mui/material"
import { useTheme } from "@mui/material/styles"
import BasicSpeedDial from "components/common/SpeedDial"
import Footer from "components/Footer"
import { useEffect, useState } from "react"
import { Outlet } from "react-router-dom"
import DashHeader from "./components/DashHeader"
import NavBarMenu from "./components/NavBarMenu"
import { ToastContainer } from "react-toastify"

function AdminPage() {
    const [state, setState] = useState(false)
    const toggleDrawer = (open: boolean) => (event: KeyboardEvent | MouseEvent) => {
        if (
            event.type === "keydown" &&
            ((event as KeyboardEvent).key === "Tab" || (event as KeyboardEvent).key === "Shift")
        ) {
            return
        }
        setState(open)
    }
    const [scrollY, setScrollY] = useState<number>(0)
    window.addEventListener("scroll", function () {
        setScrollY(this.scrollY)
    })
    const theme = useTheme()

    useEffect(() => {
        return () => {}
    }, [])

    return (
        <>
            <Box display="flex" bgcolor="background.paper" color="text.primary">
                <ToastContainer
                    position="bottom-right"
                    autoClose={2500}
                    hideProgressBar={true}
                    newestOnTop={false}
                    closeOnClick
                    rtl={false}
                    pauseOnFocusLoss
                    draggable
                    pauseOnHover
                    theme={theme.palette.mode}
                />
                <Hidden lgDown>
                    <Box
                        sx={{
                            width: "250px",
                            maxHeight: "100%",
                        }}
                    >
                        <NavBarMenu />
                    </Box>
                </Hidden>
                <Drawer anchor="left" open={state} onClose={toggleDrawer(false)}>
                    <Box
                        sx={{
                            width: "250px",
                            maxHeight: "100%",
                        }}
                    >
                        <NavBarMenu />
                    </Box>
                </Drawer>
                <Divider flexItem orientation="vertical" />
                <Box onScroll={(e) => setScrollY(e.currentTarget.scrollTop)} width="100%">
                    <Paper
                        sx={{
                            zIndex: 100,
                            borderRadius: "5px",
                            mx: 1,
                            width: "100%",
                        }}
                    >
                        <DashHeader toggleDrawer={toggleDrawer(true)} scrollY={scrollY} />
                    </Paper>
                    <Outlet />
                    <Box sx={{ position: "fixed", bottom: 0, left: 0, right: 15 }}>
                        <BasicSpeedDial label="Actions" />
                    </Box>
                </Box>
            </Box>
            <Footer />
        </>
    )
}

export default AdminPage
