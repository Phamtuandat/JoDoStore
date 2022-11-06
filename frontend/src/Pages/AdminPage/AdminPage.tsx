import { Box, Drawer, Hidden, Paper } from "@mui/material"
import { useState } from "react"
import { Outlet } from "react-router-dom"
import DashHeader from "./components/DashHeader"
import NavBarMenu from "./components/NavBarMenu"
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
    return (
        <Box p={1} display="flex" bgcolor="background.default" color="text.primary">
            <Hidden lgDown>
                <NavBarMenu />
            </Hidden>
            <Drawer anchor="left" open={state} onClose={toggleDrawer(false)}>
                <NavBarMenu />
            </Drawer>
            <Box
                sx={{
                    width: "100%",
                    pl: { lg: 1, xs: 0, md: 0, lx: 1 },
                }}
            >
                <Paper sx={{ minHeight: "100vh", p: 1.5 }}>
                    <DashHeader toggleDrawer={toggleDrawer(true)} />
                    <Outlet />
                </Paper>
            </Box>
        </Box>
    )
}

export default AdminPage
