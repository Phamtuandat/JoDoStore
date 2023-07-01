import LocalMallOutlinedIcon from "@mui/icons-material/LocalMallOutlined"
import { Badge, ClickAwayListener, Drawer, IconButton } from "@mui/material"
import { useAppSelector } from "app/hooks"
import { useRef, useState } from "react"
import { useLocation } from "react-router-dom"
import { cartSelector } from "../cartSlice"
import CartReview from "./CartReview"

function MiniCart() {
    const anchorEl = useRef<HTMLButtonElement>(null)
    const [open, setOpen] = useState(false)
    const carts = useAppSelector(cartSelector)
    const location = useLocation()
    const handleClick = () => {
        setOpen(true)
    }
    const handleClose = () => {
        setOpen(false)
    }

    return (
        <ClickAwayListener onClickAway={handleClose}>
            <>
                {location.pathname !== "/orders" && (
                    <IconButton
                        ref={anchorEl}
                        onClick={handleClick}
                        sx={{
                            color: "inherit",
                            margin: "auto",
                        }}
                    >
                        <Badge badgeContent={carts.length} color="primary">
                            <LocalMallOutlinedIcon />
                        </Badge>
                    </IconButton>
                )}
                <Drawer
                    id="basic-menu"
                    open={open && location.pathname !== "/orders"}
                    anchor={"right"}
                    onClose={handleClose}
                >
                    <CartReview carts={carts} />
                </Drawer>
            </>
        </ClickAwayListener>
    )
}

export default MiniCart
