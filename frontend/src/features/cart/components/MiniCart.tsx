import LocalMallOutlinedIcon from "@mui/icons-material/LocalMallOutlined"
import { Badge, ClickAwayListener, Drawer, IconButton } from "@mui/material"
import { useAppDispatch, useAppSelector } from "app/hooks"
import { useRef } from "react"
import { cartAction, cartSelector, showMiniCartSelector } from "../cartSlice"
import CartReview from "./CartReview"
import { useLocation } from "react-router-dom"

function MiniCart() {
    const anchorEl = useRef<HTMLButtonElement>(null)
    const showMiniCart = useAppSelector(showMiniCartSelector)
    const carts = useAppSelector(cartSelector)
    const dispatch = useAppDispatch()
    const location = useLocation()
    const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
        dispatch(cartAction.showMiniCart())
    }
    const handleClose = () => {
        dispatch(cartAction.hideMiniCart())
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
                        }}
                    >
                        <Badge badgeContent={carts.length}>
                            <LocalMallOutlinedIcon />
                        </Badge>
                    </IconButton>
                )}
                <Drawer
                    id="basic-menu"
                    open={showMiniCart && location.pathname !== "/orders"}
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
