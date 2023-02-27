import MenuIcon from "@mui/icons-material/Menu"
import SearchIcon from "@mui/icons-material/Search"
import { Button, Drawer, Hidden } from "@mui/material"
import Box from "@mui/material/Box"
import IconButton from "@mui/material/IconButton"
import Typography from "@mui/material/Typography"
import { useTheme } from "@mui/material/styles"
import { useAppDispatch, useAppSelector } from "app/hooks"
import SubHeader from "components/SubHeader"
import ProfileMenu from "components/common/ProfileMenu"
import { AuthSliceAction } from "features/authenticate/authSlice"
import MiniCart from "features/cart/components/MiniCart"
import { useEffect, useState } from "react"
import { Link } from "react-router-dom"
type Props = {}

const Header = (props: Props) => {
    const theme = useTheme()
    const [isOpen, setOpen] = useState(false)
    const isLoggedIn = useAppSelector((state) => state.auth.isLoggedIn)
    const dispatch = useAppDispatch()
    const handleLogout = () => {
        dispatch(AuthSliceAction.logout())
    }
    const [prevScrollPos, setPrevScrollPos] = useState(0)
    const [visible, setVisible] = useState(true)

    useEffect(() => {
        const handleScroll = () => {
            const currentScrollPos = window.pageYOffset
            setVisible(prevScrollPos > currentScrollPos)
            setPrevScrollPos(currentScrollPos)
        }

        window.addEventListener("scroll", handleScroll)

        return () => window.removeEventListener("scroll", handleScroll)
    }, [prevScrollPos, visible])
    return (
        <Box
            position="fixed"
            bgcolor="background.default"
            top={0}
            zIndex={100}
            px={{ xs: 0, md: 2 }}
            sx={{
                width: "100%",
                transform: !visible ? "translateY(-60px)" : "none",
                transition: "transform 0.1s",
            }}
        >
            <Box
                sx={{
                    height: "60px",
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
                        color="text.primary"
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
                        color: "text.primary",
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
