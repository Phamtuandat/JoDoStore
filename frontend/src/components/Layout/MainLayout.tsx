import MenuIcon from "@mui/icons-material/Menu"
import NotificationsNoneOutlinedIcon from "@mui/icons-material/NotificationsNoneOutlined"
import { Badge } from "@mui/material"
import Box from "@mui/material/Box"
import IconButton from "@mui/material/IconButton"
import Typography from "@mui/material/Typography"
import { useTheme } from "@mui/material/styles"
import { useAppDispatch, useAppSelector } from "app/hooks"
import Footer from "components/Footer"
import ProfileMenu from "components/common/ProfileMenu"
import BasicSpeedDial from "components/common/SpeedDial"
import { AuthSliceAction } from "features/authenticate/authSlice"
import MiniCart from "features/cart/components/MiniCart"
import { motion } from "framer-motion"
import { useState } from "react"
import { Link } from "react-router-dom"
type IProps = {
    children?: React.ReactNode
}
export const MainLayout = (props: IProps) => {
    const theme = useTheme()
    const isLoggedIn = useAppSelector((state) => state.auth.isLoggedIn)
    const dispatch = useAppDispatch()
    const [scrollY, setScrollY] = useState<number>(0)
    window.addEventListener("scroll", function () {
        setScrollY(this.scrollY)
    })
    const handleLogout = () => {
        dispatch(AuthSliceAction.logout())
    }
    return (
        <Box display="flex" flexDirection="column" bgcolor={theme.palette.background.default}>
            <Box
                component={motion.div}
                position={"sticky"}
                sx={{
                    mx: "auto",
                    justifyContent: "space-between",
                    display: "flex",
                    height: "60px",
                    alignItems: "center",
                    p: 2,
                    bgcolor: theme.palette.background.paper,
                    color: theme.palette.text.primary,
                    zIndex: 100,
                    boxShadow: theme.shadows[2],
                    width: "100%",
                }}
                top={0}
                animate={
                    scrollY > 30
                        ? { borderRadius: 10, width: "98%", top: 6, opacity: 0.95 }
                        : { borderRadius: 0, width: "100%", top: 0 }
                }
                transition={{ duration: 0.2 }}
            >
                <IconButton size="large" edge="start" aria-label="menu" sx={{ mr: 2 }}>
                    <MenuIcon color="primary" />
                </IconButton>
                <Box sx={{ flexGrow: 1 }}>
                    <Typography
                        sx={{ textDecoration: "none" }}
                        variant="h6"
                        color="primary"
                        component={Link}
                        to="/"
                    >
                        Gear
                    </Typography>
                </Box>
                <Box zIndex={10000}>
                    <MiniCart />
                    <IconButton
                        sx={{
                            color: theme.palette.text.secondary,
                        }}
                    >
                        <Badge badgeContent={4} color="primary">
                            <NotificationsNoneOutlinedIcon />
                        </Badge>
                    </IconButton>
                </Box>
                <Box>
                    <ProfileMenu isLoggedIn={isLoggedIn} handleLogout={handleLogout} />
                </Box>
            </Box>
            <Box flexDirection="column" display="flex" color="text.primary">
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
