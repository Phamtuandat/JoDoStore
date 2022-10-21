import Brightness4Icon from "@mui/icons-material/Brightness4"
import Brightness7Icon from "@mui/icons-material/Brightness7"
import MenuIcon from "@mui/icons-material/Menu"
import AppBar from "@mui/material/AppBar"
import Box from "@mui/material/Box"
import Button from "@mui/material/Button"
import IconButton from "@mui/material/IconButton"
import Paper from "@mui/material/Paper"
import Toolbar from "@mui/material/Toolbar"
import Typography from "@mui/material/Typography"
import { useTheme } from "@mui/system"
import { useAppDispatch, useAppSelector } from "app/hooks"
import { ColorMode } from "Context/ColorModeContext"
import { AuthSliceAction } from "features/authenticate/authSlice"
import { useContext, useEffect } from "react"
import { NavLink } from "react-router-dom"

type IProps = {
    children?: React.ReactNode
}
export const MainLayout = (props: IProps) => {
    const isLoggedIn = useAppSelector((state) => state.auth.isLoggedIn)
    const dispatch = useAppDispatch()
    const theme = useTheme()
    const colorMode = useContext(ColorMode)
    useEffect(() => {
        if (!!localStorage.getItem("tokenExpirationDate")) {
            const tokenExpirationDate = localStorage.getItem("tokenExpirationDate")
            const date = new Date(Date.parse(JSON.parse(tokenExpirationDate as string)))
            if (date.getTime() - new Date().getTime() <= 0) {
                dispatch(AuthSliceAction.refreshToken())
            }
        }
    }, [dispatch, isLoggedIn])

    const handleLogout = () => {
        dispatch(AuthSliceAction.logout())
        console.log(AuthSliceAction.logout())
    }
    return (
        <>
            <Box sx={{ flexGrow: 1 }}>
                <AppBar position="static">
                    <Toolbar>
                        <IconButton
                            size="large"
                            edge="start"
                            color="inherit"
                            aria-label="menu"
                            sx={{ mr: 2 }}
                        >
                            <MenuIcon />
                        </IconButton>
                        <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                            News
                        </Typography>

                        <IconButton
                            sx={{ ml: 1 }}
                            onClick={colorMode.toggleColorMode}
                            color="inherit"
                        >
                            {theme.palette.mode === "dark" ? (
                                <Brightness7Icon />
                            ) : (
                                <Brightness4Icon />
                            )}
                        </IconButton>

                        {!isLoggedIn ? (
                            <Box
                                component={NavLink}
                                to="/auth"
                                sx={{
                                    color: "inherit",
                                    textDecoration: "none",
                                }}
                            >
                                <Button color="inherit">Login</Button>
                            </Box>
                        ) : (
                            <Button variant="contained" color="warning" onClick={handleLogout}>
                                Logout
                            </Button>
                        )}
                    </Toolbar>
                </AppBar>
            </Box>
            <Paper sx={{ height: "100vh" }} square>
                {props.children}
            </Paper>
        </>
    )
}
