import { Box, Container, Grid, Hidden } from "@mui/material"
import { useAppSelector } from "app/hooks"
import { MainLayout } from "components/Layout/MainLayout"
import { selectLogin } from "features/authenticate/authSlice"
import { useEffect } from "react"
import { Outlet, useLocation, useNavigate } from "react-router-dom"
import UserNav from "./Components/UserNav"

const UserPage = () => {
    const isloggIn = useAppSelector(selectLogin)
    const navigate = useNavigate()
    const location = useLocation()
    useEffect(() => {
        if (!isloggIn) {
            navigate("/auth")
        }
        if (location.pathname === "/user") {
            navigate("/user/account")
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])

    return (
        <MainLayout>
            <Container maxWidth="lg">
                <Box mt={{ md: 20, xs: 10 }}>
                    <Grid container spacing={2}>
                        <Hidden mdDown>
                            <Grid item md={3} lg={3}>
                                <UserNav />
                            </Grid>
                        </Hidden>
                        <Grid item md={9} lg={9} sm={12} xs={12}>
                            <Box>
                                <Outlet />
                            </Box>
                        </Grid>
                    </Grid>
                </Box>
            </Container>
        </MainLayout>
    )
}

export default UserPage
