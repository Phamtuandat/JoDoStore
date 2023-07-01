/* eslint-disable react-hooks/exhaustive-deps */
import authApi from "ApiClients/AuthApi"
import ColorModeContext from "Context/ColorModeContext"
import router from "Routes/routers"
import { useAppDispatch } from "app/hooks"
import axios from "axios"
import { AuthSliceAction } from "features/authenticate/authSlice"
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
    const cookie = document.cookie.includes("idsrv.session")
    useEffect(() => {
        if (!ignore.current) {
            if (!!cookie) {
                ignore.current = true
                dispatch(cartSliceAction.getCart())
                ;(async () => {
                    try {
                        // userManager.signinRedirect()
                    } catch (error) {
                        dispatch(AuthSliceAction.logout())
                        dispatch(cartSliceAction.removeAllCartItem)
                        handleNotify.error(error as string)
                    }
                })()
            }
        }
    }, [])

    return (
        <ColorModeContext>
            <RouterProvider router={router} />
        </ColorModeContext>
    )
}

export default App
