import MenuIcon from "@mui/icons-material/Menu"
import SearchIcon from "@mui/icons-material/Search"
import { Button, Drawer, Hidden, useMediaQuery } from "@mui/material"
import Box from "@mui/material/Box"
import IconButton from "@mui/material/IconButton"
import Typography from "@mui/material/Typography"
import { useTheme } from "@mui/material/styles"
import { useAppDispatch, useAppSelector } from "app/hooks"
import SubHeader from "components/SubHeader"
import ProfileMenu from "components/common/ProfileMenu"
import { AuthSliceAction } from "features/authenticate/authSlice"
import MiniCart from "features/cart/components/MiniCart"
import { useState } from "react"
import { Link, useLocation } from "react-router-dom"
type Props = {}

const Header = (props: Props) => {
    const theme = useTheme()
    const mathes = useMediaQuery(theme.breakpoints.up("md"))
    const [isOpen, setOpen] = useState(false)
    const isLoggedIn = useAppSelector((state) => state.auth.isLoggedIn)
    const dispatch = useAppDispatch()
    const location = useLocation()
    const handleLogout = () => {
        dispatch(AuthSliceAction.logout())
    }
    return (
        <Box
            position={{ xs: "static", md: "absolute" }}
            top={0}
            zIndex={100}
            px={{ xs: 0, md: 2 }}
            sx={{
                width: "100%",
                overflow: "hidden",
            }}
        >
            <Box
                sx={{
                    height: "60px",
                    bgcolor: { xs: "background.default", md: "transparent" },
                    color: theme.palette.text.secondary,
                    zIndex: 100,
                    display: "grid",
                    gridTemplateColumns: "1fr auto 1fr",
                    alignSelf: "center",
                }}
            >
                <Hidden mdUp>
                    <Box display="flex">
                        <Button
                            sx={{
                                textShadow: "2px 2px 2px gray",
                                color: "text.primary",
                            }}
                            onClick={() => setOpen(true)}
                        >
                            <MenuIcon />
                        </Button>
                    </Box>
                    <Drawer open={isOpen} anchor="left" onClose={() => setOpen(false)}>
                        <Box role="presentation" sx={{ width: 300 }}>
                            <SubHeader />
                        </Box>
                    </Drawer>
                </Hidden>
                <Box
                    component={Link}
                    to="/"
                    sx={{ textDecoration: "none", textShadow: "2px 2px 2px gray" }}
                    display="flex"
                >
                    <Typography
                        variant="h4"
                        component="span"
                        alignSelf="center"
                        fontStyle="normal"
                        fontWeight={700}
                        color={location.pathname === "/" ? "text.secondary" : "text.primary"}
                    >
                        JODO
                    </Typography>
                </Box>

                <Hidden mdDown>
                    <Box>
                        <SubHeader />
                    </Box>
                </Hidden>
                <Box
                    display="flex"
                    justifyContent="flex-end"
                    sx={{
                        color:
                            mathes && location.pathname === "/" ? "text.secondary" : "text.primary",
                    }}
                >
                    <IconButton
                        sx={{
                            color: "inherit",
                        }}
                    >
                        <SearchIcon />
                    </IconButton>
                    <MiniCart />
                    <ProfileMenu isLoggedIn={isLoggedIn} handleLogout={handleLogout} />
                </Box>
            </Box>
        </Box>
    )
}

export default Header
