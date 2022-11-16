import AccountCircleIcon from "@mui/icons-material/AccountCircle"
import MenuOpenIcon from "@mui/icons-material/MenuOpen"
import NotificationsNoneIcon from "@mui/icons-material/NotificationsNone"
import { Box, Breadcrumbs, Hidden, Link, Typography } from "@mui/material"
import Badge from "@mui/material/Badge"
import IconButton from "@mui/material/IconButton"
import { Link as RouterLink, LinkProps, useLocation } from "react-router-dom"
type Props = {
    toggleDrawer: Function
}
const breadcrumbNameMap: { [key: string]: string } = {
    "/admin/product/new": "new",
    "/admin/product": "product",
    "/admin": "admin",
    "/admin/product/edit": "edit",
    "/admin/product/list": "list",
}
interface LinkRouterProps extends LinkProps {
    to: string
    replace?: boolean
}

function LinkRouter(props: LinkRouterProps) {
    return <Link underline="hover" {...props} component={RouterLink as any} />
}
const DashHeader = ({ toggleDrawer }: Props) => {
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
                bgcolor: "background.paper",
            }}
        >
            <Box>
                <div role="presentation" onClick={handleClick}>
                    <Breadcrumbs aria-label="breadcrumb" color="text.primary">
                        {pathnames.map((value, index) => {
                            const last = index === pathnames.length - 1
                            const to = `/${pathnames.slice(0, index + 1).join("/")}`
                            return last ? (
                                <Typography color="text.primary" key={to}>
                                    {breadcrumbNameMap[to]}
                                </Typography>
                            ) : (
                                <LinkRouter
                                    to={to}
                                    key={to}
                                    style={{
                                        opacity: 0.6,
                                    }}
                                >
                                    {breadcrumbNameMap[to]}
                                </LinkRouter>
                            )
                        })}
                    </Breadcrumbs>
                </div>
            </Box>
            <Box
                sx={{
                    "&>*": {
                        ml: 1,
                    },
                }}
            >
                <IconButton>
                    <Badge badgeContent={4} color="secondary">
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
