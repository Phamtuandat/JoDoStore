/* eslint-disable react-hooks/exhaustive-deps */
import authApi from "ApiClients/AuthApi"
import ColorModeContext from "Context/ColorModeContext"
import router from "Routes/routers"
import { useAppDispatch } from "app/hooks"
import { AuthSliceAction } from "features/authenticate/authSlice"
import { cartSliceAction } from "features/cart/cartSlice"
import { useEffect, useRef } from "react"
import { RouterProvider } from "react-router-dom"
import "react-toastify/dist/ReactToastify.css"
import handleNotify from "utils/Toast-notify"

function App() {
    const ignore = useRef(false)
    const dispatch = useAppDispatch()
    const isLoggedIn =
        localStorage.getItem("persist:root") &&
        JSON.parse(JSON.parse(localStorage.getItem("persist:root") as string)?.auth).isLoggedIn
    useEffect(() => {
        if (!ignore.current) {
            if (isLoggedIn) {
                ignore.current = true
                dispatch(cartSliceAction.getCart())
                ;(async () => {
                    try {
                        var result = (await authApi.refreshCookie()).data
                        dispatch(AuthSliceAction.success(result))
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
