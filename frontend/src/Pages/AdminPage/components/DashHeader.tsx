import AccountCircleIcon from "@mui/icons-material/AccountCircle"
import MenuOpenIcon from "@mui/icons-material/MenuOpen"
import NotificationsNoneIcon from "@mui/icons-material/NotificationsNone"
import { Box, Breadcrumbs, Hidden, Link, Typography } from "@mui/material"
import Badge from "@mui/material/Badge"
import IconButton from "@mui/material/IconButton"
import { useTheme } from "@mui/material/styles"
import { Link as RouterLink, LinkProps, useLocation, useParams } from "react-router-dom"
type Props = {
    toggleDrawer: Function
    scrollY?: number
}
const breadcrumbNameMap: { [key: string]: string } = {
    "/admin/product/new": "Add Product",
    "/admin/product": "Product",
    "/admin": "Admin",
    "/admin/product/edit": "Edit Product",
    "/admin/product/list": "list",
    "/admin/Analytics": "Analytics",
}
interface LinkRouterProps extends LinkProps {
    to: string
    replace?: boolean
}

function LinkRouter(props: LinkRouterProps) {
    return <Link underline="hover" {...props} component={RouterLink as any} />
}
const DashHeader = ({ toggleDrawer, scrollY = 0 }: Props) => {
    const { productId } = useParams()
    const theme = useTheme()
    const location = useLocation()
    const pathnames = location.pathname.split("/").filter((x) => x)
    function handleClick(event: React.MouseEvent<HTMLDivElement, MouseEvent>) {
        event.preventDefault()
    }
    return (
        <Box
            sx={{
                justifyContent: "space-between",
                width: "100%",
                display: "flex",
                height: "60px",
                alignItems: "center",
                p: 2,
                bgcolor:
                    theme.palette.mode === "dark"
                        ? scrollY < 0.9
                            ? "background.default"
                            : "background.paper"
                        : "background.default",
            }}
        >
            <Box>
                <Box display="flex" role="presentation" onClick={handleClick}>
                    <Breadcrumbs aria-label="breadcrumb" color="text.primary">
                        {pathnames.map((value, index) => {
                            const last = index === pathnames.length - 1
                            const to = `/${pathnames.slice(0, index + 1).join("/")}`
                            return last ? (
                                <Typography color="primary" key={to}>
                                    {breadcrumbNameMap[to]} {productId && `${productId}`}
                                </Typography>
                            ) : (
                                <LinkRouter
                                    to={to}
                                    key={to}
                                    style={{
                                        color: theme.palette.text.primary,
                                    }}
                                >
                                    {breadcrumbNameMap[to]}
                                </LinkRouter>
                            )
                        })}
                    </Breadcrumbs>
                </Box>
            </Box>
            <Box
                sx={{
                    "&>*": {
                        ml: 1,
                    },
                    display: "flex",
                }}
            >
                <IconButton>
                    <Badge badgeContent={4} color="primary">
                        <NotificationsNoneIcon />
                    </Badge>
                </IconButton>
                <IconButton>
                    <AccountCircleIcon />
                </IconButton>
                <Hidden lgUp>
                    <IconButton onClick={() => toggleDrawer(true)}>
                        <MenuOpenIcon />
                    </IconButton>
                </Hidden>
            </Box>
        </Box>
    )
}

export default DashHeader
