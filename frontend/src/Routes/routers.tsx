import authApi from "ApiClients/AuthApi"
import { productApi } from "ApiClients/ProductApi"
import AdminPage from "Pages/AdminPage/AdminPage"
import AnalyticsPage from "Pages/AdminPage/Analytics/AnalyticsPage"
import OrderListPage from "Pages/AdminPage/OrderListPage/OrderListPage"
import ProductPage from "Pages/AdminPage/Product"
import AddProductPage from "Pages/AdminPage/Product/AddProductPage"
import EditPage from "Pages/AdminPage/Product/EditPage"
import EditForm from "Pages/AdminPage/Product/ProductComponent/EditForm"
import UIPage from "Pages/AdminPage/UiPage/UIPage"
import HomePage from "Pages/HomePage/HomePage"
import OrderPage from "Pages/ProductPage/OrderPage"
import ProductDetailPage from "Pages/ProductPage/ProductDetailPage"
import ProductShopPage from "Pages/ProductPage/ProductShopPage"
import AccountPage from "Pages/UserPage/AccountPage"
import CustomerPage from "Pages/UserPage/CustomerPage"
import UserPage from "Pages/UserPage/Index"
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
    {
        path: "/admin",
        loader: async () => {
            return await authApi.adminValidate().catch(() => {
                throw new Response("Unauthorize!", { status: 400 })
            })
        },
        element: <AdminPage />,
        errorElement: <ErrorPage />,

        children: [
            {
                path: "/admin/Analytics",
                element: <AnalyticsPage />,
            },
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
                        element: <EditPage />,
                        children: [
                            {
                                path: "/admin/product/edit/:productId",
                                loader: async ({ params }) => {
                                    if (params.productId) {
                                        return (await productApi.getById(+params.productId)).data
                                    }
                                },
                                element: <EditForm />,
                            },
                        ],
                    },
                    {
                        path: "/admin/product/list",
                        element: <ProductShopPage />,
                    },
                ],
            },
            {
                path: "/admin/Ecommerce",
                element: <ProductShopPage />,
                children: [],
            },
            {
                path: "/admin/orders",
                element: <OrderListPage />,
                children: [],
            },
            {
                path: "/admin/uiPage",
                element: <UIPage />,
                children: [],
            },
        ],
    },
])

export default router
