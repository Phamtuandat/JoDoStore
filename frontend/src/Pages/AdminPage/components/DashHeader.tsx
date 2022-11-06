import AccountCircleIcon from "@mui/icons-material/AccountCircle"
import MenuOpenIcon from "@mui/icons-material/MenuOpen"
import NotificationsNoneIcon from "@mui/icons-material/NotificationsNone"
import { Box, Breadcrumbs, Hidden, Link, Typography } from "@mui/material"
import Badge from "@mui/material/Badge"
import IconButton from "@mui/material/IconButton"
import Toolbar from "@mui/material/Toolbar"
import { Link as RouterLink, LinkProps, useLocation } from "react-router-dom"
type Props = {
    toggleDrawer: Function
}
const breadcrumbNameMap: { [key: string]: string } = {
    "/admin/product/new": "new",
    "/admin/product": "product",
    "/admin": "admin",
}
interface LinkRouterProps extends LinkProps {
    to: string
    replace?: boolean
}

function LinkRouter(props: LinkRouterProps) {
    return <Link {...props} component={RouterLink as any} />
}
const DashHeader = ({ toggleDrawer }: Props) => {
    const location = useLocation()
    const pathnames = location.pathname.split("/").filter((x) => x)
    function handleClick(event: React.MouseEvent<HTMLDivElement, MouseEvent>) {
        event.preventDefault()
        console.log(location.pathname.split("/").slice(1))
    }
    return (
        <Box sx={{ flexGrow: 1 }}>
            <Toolbar sx={{ justifyContent: "space-between" }}>
                <Box>
                    <div role="presentation" onClick={handleClick}>
                        <Breadcrumbs aria-label="breadcrumb">
                            {pathnames.map((value, index) => {
                                const last = index === pathnames.length - 1
                                const to = `/${pathnames.slice(0, index + 1).join("/")}`
                                return last ? (
                                    <Typography color="text.primary" key={to}>
                                        {breadcrumbNameMap[to]}
                                    </Typography>
                                ) : (
                                    <LinkRouter color="inherit" to={to} key={to}>
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
            </Toolbar>
        </Box>
    )
}

export default DashHeader
