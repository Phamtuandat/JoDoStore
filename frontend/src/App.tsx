import { useTheme } from "@mui/system"
import ErrorPage from "components/common/ErorrPage"
import ColorModeContext from "Context/ColorModeContext"
import AuthenticatePage from "features/authenticate/page/AuthenticatePage"
import AdminPage from "Pages/AdminPage/AdminPage"
import AddProductPage from "Pages/AdminPage/Product/AddProductPage"
import ProductListPage from "Pages/AdminPage/Product/ProductListPage"
import ProductPage from "Pages/AdminPage/Product/ProductPage"
import HomePage from "Pages/HomePage/HomePage"
import { createBrowserRouter, RouterProvider } from "react-router-dom"
import { ToastContainer } from "react-toastify"
import "react-toastify/dist/ReactToastify.css"

function App() {
    const theme = useTheme()
    const router = createBrowserRouter([
        {
            path: "/",
            element: <HomePage />,
            errorElement: <ErrorPage />,
        },
        {
            path: "/auth",
            element: <AuthenticatePage />,
        },
        {
            path: "/admin",
            element: <AdminPage />,
            errorElement: <ErrorPage />,
            children: [
                {
                    path: "/admin/product",
                    element: <ProductPage />,
                    children: [
                        {
                            path: "/admin/product/new",
                            element: <AddProductPage />,
                        },
                        {
                            path: "/admin/product/edit",
                            element: <AddProductPage />,
                        },
                        {
                            path: "/admin/product/list",
                            element: <ProductListPage />,
                        },
                    ],
                },
            ],
        },
    ])
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
