import { useTheme } from "@mui/system"
import ErrorPage from "components/common/ErorrPage"
import { MainLayout } from "components/Layout/MainLayout"
import ColorModeContext from "Context/ColorModeContext"
import AuthenticatePage from "features/authenticate/page/AuthenticatePage"
import { createBrowserRouter, RouterProvider } from "react-router-dom"
import { ToastContainer } from "react-toastify"
import "react-toastify/dist/ReactToastify.css"

function App() {
    const theme = useTheme()
    const router = createBrowserRouter([
        {
            path: "/",
            element: <MainLayout />,
            errorElement: <ErrorPage />,
        },
        {
            path: "/auth",
            element: <AuthenticatePage />,
        },
    ])
    return (
        <ColorModeContext>
            <ToastContainer
                position="bottom-right"
                autoClose={5000}
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
