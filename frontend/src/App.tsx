/* eslint-disable react-hooks/exhaustive-deps */
import { useTheme } from "@mui/system"
import authApi from "ApiClients/AuthApi"
import ColorModeContext from "Context/ColorModeContext"
import router from "Routes/routers"
import { useAppDispatch } from "app/hooks"
import { AuthSliceAction } from "features/authenticate/authSlice"
import { useEffect, useRef } from "react"
import { RouterProvider } from "react-router-dom"
import { ToastContainer } from "react-toastify"
import "react-toastify/dist/ReactToastify.css"
import handleNotify from "utils/Toast-notify"

function App() {
    const ignore = useRef(false)
    const theme = useTheme()
    const dispatch = useAppDispatch()
    const isLoggedIn =
        localStorage.getItem("persist:root") &&
        JSON.parse(JSON.parse(localStorage.getItem("persist:root") as string)?.auth).isLoggedIn
    useEffect(() => {
        if (!ignore.current) {
            if (isLoggedIn) {
                ignore.current = true
                ;(async () => {
                    try {
                        var result = (await authApi.refreshCookie()).data
                        dispatch(AuthSliceAction.success(result))
                    } catch (error) {
                        console.log(error)
                        dispatch(AuthSliceAction.logout())
                        handleNotify.error("session has expired, please login again!")
                    }
                })()
            }
        }
    }, [])

    return (
        <ColorModeContext>
            <ToastContainer
                position="bottom-right"
                autoClose={2500}
                hideProgressBar={true}
                newestOnTop={false}
                closeOnClick
                rtl={false}
                pauseOnFocusLoss
                draggable
                pauseOnHover
                theme={theme.palette.mode}
            />
            <RouterProvider router={router} />
        </ColorModeContext>
    )
}

export default App
