import { Box, Drawer, Hidden, Paper } from "@mui/material"
import BasicSpeedDial from "components/common/SpeedDial"
import { useState } from "react"
import { Outlet } from "react-router-dom"
import DashHeader from "./components/DashHeader"
import NavBarMenu from "./components/NavBarMenu"
function AdminPage() {
    const [state, setState] = useState(false)
    const [scrollY, setScrollY] = useState<number>(0)
    const toggleDrawer = (open: boolean) => (event: KeyboardEvent | MouseEvent) => {
        if (
            event.type === "keydown" &&
            ((event as KeyboardEvent).key === "Tab" || (event as KeyboardEvent).key === "Shift")
        ) {
            return
        }
        setState(open)
    }
    window.addEventListener("scroll", function () {
        setScrollY(this.scrollY)
    })

    return (
        <Box
            p={1}
            display="flex"
            bgcolor="background.default"
            color="text.primary"
            maxHeight="100vh"
            sx={{
                overflowY: "clip",
            }}
        >
            <Box maxHeight="100vh">
                <Hidden lgDown>
                    <NavBarMenu />
                </Hidden>
                <Drawer anchor="left" open={state} onClose={toggleDrawer(false)}>
                    <NavBarMenu />
                </Drawer>
            </Box>
            <Box
                sx={{
                    width: "100%",
                    pl: { lg: 1, xs: 0, md: 0, lx: 1 },
                    overflowY: "scroll",
                }}
                onScroll={(e) => setScrollY(e.currentTarget.scrollTop)}
            >
                <Paper sx={{ p: 1.5, minHeight: "100vh" }}>
                    <Paper
                        sx={{
                            position: "sticky",
                            top: "0",
                            zIndex: 100,
                            bgcolor: "background.default",
                            borderRadius: "10px",
                            overflow: "hidden",
                            opacity: 0.97,
                        }}
                        elevation={scrollY > 0.9 ? 2 : 1}
                    >
                        <DashHeader toggleDrawer={toggleDrawer(true)} />
                    </Paper>
                    <Outlet />
                </Paper>
                <Box sx={{ position: "fixed", bottom: 0, left: 0, right: 15 }}>
                    <BasicSpeedDial label="Actions" />
                </Box>
            </Box>
        </Box>
    )
}

export default AdminPage
