import * as React from "react"
import Button from "@mui/material/Button"
import Menu from "@mui/material/Menu"
import MenuItem from "@mui/material/MenuItem"
import { Avatar, Box } from "@mui/material"
import { NavLink } from "react-router-dom"

type Props = {
    isLoggedIn: boolean | null
    handleLogout: () => void
}

export default function BasicMenu({ isLoggedIn, handleLogout }: Props) {
    const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null)
    const open = Boolean(anchorEl)
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
                <Avatar alt="Remy Sharp" src="/static/images/avatar/1.jpg" />
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
                <MenuItem onClick={handleClose}>My account</MenuItem>
                <MenuItem onClick={handleClose}>My Orders</MenuItem>
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
