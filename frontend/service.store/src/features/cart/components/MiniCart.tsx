import LocalMallOutlinedIcon from "@mui/icons-material/LocalMallOutlined"
import { Badge, ClickAwayListener, Drawer, IconButton } from "@mui/material"
import { productApi } from "ApiClients/ProductApi"
import { useAppSelector } from "app/hooks"
import { Product } from "models"
import qs from "qs"
import { useEffect, useRef, useState } from "react"
import { useLocation } from "react-router-dom"
import { cartSelector } from "../cartSlice"
import CartReview from "./CartReview"

export type CartItemReview = {
    product: Product
    quantity: number
}

function MiniCart() {
    const anchorEl = useRef<HTMLButtonElement>(null)
    const [open, setOpen] = useState(false)
    const cartItemSelector = useAppSelector(cartSelector)
    const [carts, setCarts] = useState<CartItemReview[]>([])
    const location = useLocation()
    const handleClick = () => {
        setOpen(true)
    }
    const handleClose = () => {
        setOpen(false)
    }
    useEffect(() => {
        ;(async () => {
            const productIds = cartItemSelector.map((item) => item.productId)
            const param = qs.stringify({
                ids: productIds,
            })
            if (productIds.length > 0) {
                const cartReview: CartItemReview[] = []
                const productList = (await productApi.getList(param)).data
                productList.forEach((product) => {
                    cartReview.push({
                        product: product,
                        quantity:
                            cartItemSelector.find((cart) => cart.productId === product.id)
                                ?.quantity || 0,
                    })
                })
                setCarts(cartReview)
            } else {
                setCarts([])
            }
        })()
    }, [cartItemSelector, cartItemSelector.length])
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
                        <Badge badgeContent={cartItemSelector.length} color="primary">
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
