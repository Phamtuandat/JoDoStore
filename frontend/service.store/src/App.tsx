/* eslint-disable react-hooks/exhaustive-deps */
import ColorModeContext from "Context/ColorModeContext"
import router from "Routes/routers"
import { useAppDispatch, useAppSelector } from "app/hooks"
import axios from "axios"
import { AuthSliceAction, selectToken } from "features/authenticate/authSlice"
import { cartSliceAction } from "features/cart/cartSlice"
import { User, UserManager } from "oidc-client"
import { useEffect, useRef } from "react"
import { RouterProvider } from "react-router-dom"
import "react-toastify/dist/ReactToastify.css"
import oidcConfig from "utils/OidcConfig"
import handleNotify from "utils/Toast-notify"

const userMgr = new UserManager(oidcConfig)
function App() {
    const ignore = useRef(false)
    const dispatch = useAppDispatch()
    const token = useAppSelector(selectToken)
    useEffect(() => {
        if (!ignore.current) {
            if (!!token) {
                ignore.current = true
                ;(async () => {
                    try {
                        dispatch(cartSliceAction.getCart())
                        const user = (
                            await axios.get(
                                (process.env.REACT_APP_IDENTITY_URL || "https://localhost:5001") +
                                    "/connect/userinfo",
                                {
                                    headers: {
                                        Authorization: "Bearer " + token,
                                    },
                                }
                            )
                        ).data as User
                        dispatch(
                            AuthSliceAction.success({
                                access_Token: user.access_token,
                                email: user.profile.email || user.profile.name || "",
                                firstName: user.profile.family_name || "",
                                lastName: user.profile.given_name || "",
                                id: user.profile.id,
                                emailConfirmed: true,
                                address: null,
                            })
                        )
                    } catch (error) {
                        userMgr.signoutPopupCallback()
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
