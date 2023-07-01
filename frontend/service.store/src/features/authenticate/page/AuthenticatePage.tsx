/* eslint-disable react-hooks/exhaustive-deps */
import { Box, Paper } from "@mui/material"
import CssBaseline from "@mui/material/CssBaseline"
import Grid from "@mui/material/Grid"
import { useAppDispatch, useAppSelector } from "app/hooks"
import { LoginRequest, RegisterRequest } from "models"
import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { ToastContainer } from "react-toastify"
import { AuthSliceAction } from "../authSlice"
import { LoginForm } from "../components/LoginForm"
import { RegisterForm } from "../components/RegisterForm"

export default function AuthenticatePage() {
    const navigate = useNavigate()

    const [mode, setMode] = useState<"Signin" | "Signup">("Signin")
    const dispatch = useAppDispatch()
    const isLogging = !!useAppSelector((state) => state.auth.processing)
    const isLoggedIn = !!useAppSelector((state) => state.auth.isLoggedIn)

    useEffect(() => {
        return () => {
            if (!isLoggedIn) {
                dispatch(AuthSliceAction.cancel())
            }
        }
    }, [])
    useEffect(() => {
        if (isLoggedIn) {
            navigate(-1)
        }
    }, [isLoggedIn, navigate])

    useEffect(() => {
        if (isLoggedIn) {
            navigate("/")
        }
    }, [])

    const onLoginSubmit = (formValue: LoginRequest) => {
        dispatch(AuthSliceAction.login(formValue))
    }
    const onRegisterSubmit = (formValue: RegisterRequest) => {
        dispatch(AuthSliceAction.register(formValue))
    }
    const handleLogout = () => {
        dispatch(AuthSliceAction.logout())
    }

    const toggleMode = () => {
        setMode((prev) => (prev === "Signin" ? "Signup" : "Signin"))
    }
    return (
        <Box>
            <ToastContainer />
            <Grid container component="main" sx={{ height: "calc(100vh - 0px)" }}>
                <CssBaseline />
                <Grid
                    item
                    xs={false}
                    sm={4}
                    md={7}
                    sx={{
                        backgroundImage:
                            "url(https://bookriot.com/wp-content/uploads/2020/03/library-libraries-feature-700x375-1-1280x720.jpg)",
                        backgroundRepeat: "no-repeat",
                        backgroundColor: (t) =>
                            t.palette.mode === "light" ? t.palette.grey[50] : t.palette.grey[900],
                        backgroundSize: "cover",
                        backgroundPosition: "center",
                    }}
                />
                <Grid item xs={12} sm={8} md={5} component={Paper} elevation={6} square>
                    {mode === "Signin" ? (
                        <LoginForm
                            onSubmit={onLoginSubmit}
                            isLoggedIn={isLoggedIn}
                            isLogging={isLogging}
                            handleLogout={handleLogout}
                            toggleMode={toggleMode}
                        />
                    ) : (
                        <RegisterForm
                            onSubmit={onRegisterSubmit}
                            handleLogout={handleLogout}
                            toggleMode={toggleMode}
                        />
                    )}
                </Grid>
            </Grid>
        </Box>
    )
}
