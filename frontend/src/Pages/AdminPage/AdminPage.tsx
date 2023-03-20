import { Box, Drawer, Hidden, Paper } from "@mui/material"
import { useTheme } from "@mui/material/styles"
import BasicSpeedDial from "components/common/SpeedDial"
import Footer from "components/Footer"
import { useState } from "react"
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
    return (
        <Box
            display="flex"
            bgcolor="background.default"
            color="text.primary"
            maxHeight="100vh"
            sx={{
                overflowY: "clip",
            }}
        >
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
            <Paper elevation={2}>
                <Hidden lgDown>
                    <NavBarMenu />
                </Hidden>
                <Drawer anchor="left" open={state} onClose={toggleDrawer(false)}>
                    <Box bgcolor="background.default">
                        <NavBarMenu />
                    </Box>
                </Drawer>
            </Paper>
            <Box
                sx={{
                    width: "100%",
                    pl: { lg: 1, xs: 0, md: 0, lx: 1 },
                    overflowY: "scroll",
                    "&:hover::-webkit-scrollbar": {
                        display: "block",
                        height: "4px",
                    },
                    "&::-webkit-scrollbar": {
                        width: "0.512rem",
                        height: "4px",
                    },
                    "&::-webkit-scrollbar-track": {
                        boxShadow: "inset 0 0 6px rgba(0,0,0,0.00)",
                        webkitBoxShadow: "inset 0 0 6px rgba(0,0,0,0.00)",
                    },
                    "&::-webkit-scrollbar-thumb": {
                        backgroundColor: "#8d8e90",
                        height: "4px",
                        borderRadius: "8px",
                    },
                }}
                onScroll={(e) => setScrollY(e.currentTarget.scrollTop)}
            >
                <Box sx={{ minHeight: "100vh" }}>
                    <Paper
                        sx={{
                            position: "sticky",
                            top: "5px",
                            zIndex: 100,
                            borderRadius: "5px",
                            overflow: "hidden",
                            opacity: 0.92,
                            mx: 1,
                        }}
                        elevation={scrollY > 0.9 ? 2 : 0}
                    >
                        <DashHeader toggleDrawer={toggleDrawer(true)} scrollY={scrollY} />
                    </Paper>
                    <Outlet />
                </Box>
                <Box sx={{ position: "fixed", bottom: 0, left: 0, right: 15 }}>
                    <BasicSpeedDial label="Actions" />
                </Box>
                <Footer />
            </Box>
        </Box>
    )
}

export default AdminPage
