import LocalMallOutlinedIcon from "@mui/icons-material/LocalMallOutlined"
import { Badge, ClickAwayListener, Drawer, IconButton } from "@mui/material"
import { useAppDispatch, useAppSelector } from "app/hooks"
import { useRef } from "react"
import { cartAction, cartSelector, showMiniCartSelector } from "../cartSlice"
import { useTheme } from "@mui/material/styles"
import CartReview from "./CartReview"

function MiniCart() {
    const theme = useTheme()
    const anchorEl = useRef<HTMLButtonElement>(null)
    const showMiniCart = useAppSelector(showMiniCartSelector)
    const carts = useAppSelector(cartSelector)
    const dispatch = useAppDispatch()

    const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
        dispatch(cartAction.showMiniCart())
    }
    const handleClose = () => {
        dispatch(cartAction.hideMiniCart())
    }

    return (
        <ClickAwayListener onClickAway={handleClose}>
            <>
                <IconButton
                    ref={anchorEl}
                    onClick={handleClick}
                    sx={{
                        color: theme.palette.text.secondary,
                    }}
                >
                    <Badge badgeContent={carts.length} color="primary">
                        <LocalMallOutlinedIcon />
                    </Badge>
                </IconButton>
                <Drawer id="basic-menu" open={showMiniCart} anchor={"right"} onClose={handleClose}>
                    <CartReview carts={carts} />
                </Drawer>
            </>
        </ClickAwayListener>
    )
}

export default MiniCart
