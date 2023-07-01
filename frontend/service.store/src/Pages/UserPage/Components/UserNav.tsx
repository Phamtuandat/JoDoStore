import { useTheme } from "@mui/material/styles"
import Box from "@mui/material/Box"
import Divider from "@mui/material/Divider"
import ListItemButton from "@mui/material/ListItemButton"
import ListItemText from "@mui/material/ListItemText"
import { useAppSelector } from "app/hooks"
import { selectCurrentUser } from "features/authenticate/authSlice"
import { Link, useLocation } from "react-router-dom"

const data = [
    { label: "Manager My Profile", path: "/user/account" },
    { label: "My Orders", path: "/user/customer" },
]

type Props = {}

const UserNav = (props: Props) => {
    const theme = useTheme()
    const currentUser = useAppSelector(selectCurrentUser)
    const location = useLocation()
    return (
        <Box px={3}>
            <ListItemText
                sx={{ my: 0 }}
                primary={`Hello, ${currentUser?.firstName} ${currentUser?.lastName}`}
                primaryTypographyProps={{
                    letterSpacing: 0,
                }}
            />
            <Divider />
            <Box>
                {data.map((item) => (
                    <Box
                        key={item.label}
                        component={Link}
                        to={item.path}
                        sx={{
                            textDecoration: "none",
                            color: "text.primary",
                            width: "100%",
                        }}
                    >
                        <ListItemButton
                            divider
                            sx={{
                                py: 2,
                                minHeight: 50,
                                px: 1,
                                "&:hover": {
                                    bgcolor: theme.palette.primary.light,
                                },
                                "&.Mui-selected": {
                                    bgcolor: theme.palette.primary.light,
                                },
                                "&.Mui-selected:hover": {
                                    bgcolor: theme.palette.primary.light,
                                },
                                my: 1,
                                borderRadius: "10px",
                            }}
                            selected={item.path === location.pathname}
                        >
                            <ListItemText
                                primary={item.label}
                                primaryTypographyProps={{
                                    fontWeight: "medium",
                                }}
                            />
                        </ListItemButton>
                    </Box>
                ))}
            </Box>
        </Box>
    )
}

export default UserNav
