import authApi from "ApiClients/AuthApi"
import { productApi } from "ApiClients/ProductApi"
import HomePage from "Pages/HomePage/HomePage"
import OrderPage from "Pages/ProductPage/OrderPage"
import ProductDetailPage from "Pages/ProductPage/ProductDetailPage"
import ProductShopPage from "Pages/ProductPage/ProductShopPage"
import AccountPage from "Pages/UserPage/AccountPage"
import CustomerPage from "Pages/UserPage/CustomerPage"
import UserPage from "Pages/UserPage/Index"
import Redirect from "components/Redirect"
import ErrorPage from "components/common/ErorrPage"
import AuthenticatePage from "features/authenticate/page/AuthenticatePage"
import { createBrowserRouter, redirect } from "react-router-dom"

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
        path: "/redirect",
        element: <Redirect />,
    },
    {
        path: "/Product/:productId",
        loader: async ({ params }) => {
            if (params.productId) {
                return (
                    await productApi.getById(+params.productId).catch(() => {
                        throw new Response("Could Not Found Product", { status: 404 })
                    })
                ).data
            }
        },
        element: <ProductDetailPage />,
        errorElement: <ErrorPage />,
    },
    {
        path: "/shop",
        element: <ProductShopPage />,
        errorElement: <ErrorPage />,
    },
    {
        path: "/orders",
        element: <OrderPage />,
        errorElement: <ErrorPage />,
    },
    {
        path: "/user",
        element: <UserPage />,
        loader: async () => {
            try {
                await authApi.refreshCookie()
                return null
            } catch (error) {
                return redirect("/auth")
            }
        },
        children: [
            {
                path: "/user/customer",
                element: <CustomerPage />,
            },
            {
                path: "/user/account",
                element: <AccountPage />,
            },
        ],
    },
])

export default router
