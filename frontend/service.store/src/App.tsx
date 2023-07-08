/* eslint-disable react-hooks/exhaustive-deps */
import ColorModeContext from "Context/ColorModeContext"
import router from "Routes/routers"
import { useAppDispatch, useAppSelector } from "app/hooks"
import { AuthSliceAction, selectToken } from "features/authenticate/authSlice"
import { cartSliceAction } from "features/cart/cartSlice"
import { UserManager } from "oidc-client"
import { useEffect, useRef } from "react"
import { RouterProvider } from "react-router-dom"
import "react-toastify/dist/ReactToastify.css"
import oidcConfig from "utils/OidcConfig"
import handleNotify from "utils/Toast-notify"

const userManager = new UserManager(oidcConfig)
function App() {
    const ignore = useRef(false)
    const dispatch = useAppDispatch()
    const token = useAppSelector(selectToken)
    useEffect(() => {
        if (!ignore.current) {
            if (!!token) {
                ignore.current = true
                const session = sessionStorage.getItem("oidc.user:https://localhost:5001:js-dev")
                ;(async () => {
                    try {
                        if (!session) {
                            await userManager.signinSilent(token)
                        }
                        dispatch(cartSliceAction.getCart(token))
                        console.log(token)
                    } catch (error) {
                        dispatch(cartSliceAction.removeAllCartItem)
                        dispatch(AuthSliceAction.logout)
                        handleNotify.error(error as string)
                    }
                })()
            }
        }
    }, [token])

    return (
        <ColorModeContext>
            <RouterProvider router={router} />
        </ColorModeContext>
    )
}

export default App
