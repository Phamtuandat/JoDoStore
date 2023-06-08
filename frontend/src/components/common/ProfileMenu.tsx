import * as React from "react"
import Button from "@mui/material/Button"
import Menu from "@mui/material/Menu"
import MenuItem from "@mui/material/MenuItem"
import { Avatar, Box, Typography, Link as MuiLink } from "@mui/material"
import { NavLink } from "react-router-dom"
import { Link } from "react-router-dom"
import { useAppSelector } from "app/hooks"
import { selectCurrentUser } from "features/authenticate/authSlice"

type Props = {
    isLoggedIn: boolean | null
    handleLogout: () => void
}

export default function BasicMenu({ isLoggedIn, handleLogout }: Props) {
    const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null)
    const open = Boolean(anchorEl)
    const currentUser = useAppSelector(selectCurrentUser)
    const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
        setAnchorEl(event.currentTarget)
    }
    const handleClose = () => {
        setAnchorEl(null)
    }

    return (
        <div>
            <Button
                id="basic-button"
                aria-controls={open ? "basic-menu" : undefined}
                aria-haspopup="true"
                aria-expanded={open ? "true" : undefined}
                onClick={handleClick}
            >
                <Avatar alt={currentUser?.firstName || "A"} src="/static/images/avatar/1.jpg" />
            </Button>
            <Menu
                id="basic-menu"
                anchorEl={anchorEl}
                open={open}
                onClose={handleClose}
                MenuListProps={{
                    "aria-labelledby": "basic-button",
                }}
            >
                <MenuItem onClick={handleClose}>
                    <MuiLink
                       href="https://account.diydevblog.com/index"
                    >
                        My account
                    </MuiLink>
                </MenuItem>
                <MenuItem onClick={handleClose}>
                    <Typography
                        component={Link}
                        to="/user/customer"
                        sx={{
                            textDecoration: "none",
                            color: "text.primary",
                        }}
                    >
                        My orders
                    </Typography>
                </MenuItem>
                <MenuItem onClick={handleClose}>
                    {!isLoggedIn ? (
                        <Box
                            component={NavLink}
                            to="/auth"
                            sx={{
                                textDecoration: "none",
                            }}
                        >
                            <Button color="secondary">Login</Button>
                        </Box>
                    ) : (
                        <Button color="secondary" onClick={handleLogout}>
                            Logout
                        </Button>
                    )}
                </MenuItem>
            </Menu>
        </div>
    )
}
