import AnalyticsIcon from "@mui/icons-material/Analytics"
import CategoryIcon from "@mui/icons-material/Category"
import DashboardIcon from "@mui/icons-material/Dashboard"
import FilterVintageIcon from "@mui/icons-material/FilterVintage"
import { Box, ListSubheader } from "@mui/material"
import Divider from "@mui/material/Divider/Divider"
import List from "@mui/material/List"
import { styled } from "@mui/material/styles"
import { NestedMenuItem } from "./NestedMenuItem"
type Props = {
    onClose?: () => void
}

const ListItemIconCustom = styled("span")(({ theme }) => ({
    fontSize: "14px",
    marginRight: "15px",
    fontWeight: 300,
}))

const productMenu = [
    {
        icon: <ListItemIconCustom>N</ListItemIconCustom>,
        title: "New Product",
        nestedLink: "product/new",
        pathName: "/admin/product/new",
    },
    {
        icon: <ListItemIconCustom>E</ListItemIconCustom>,
        title: "Edit Product",
        nestedLink: "product/edit",
        pathName: "/admin/product/edit",
    },
    {
        icon: <ListItemIconCustom>P</ListItemIconCustom>,
        title: "Product Page",
        nestedLink: "product/list",
        pathName: "/admin/product/list",
    },
]
// const initialSate = {
//     Product: false,
//     orders: false,
//     Analytics: false,
// }
const NavBarMenu = ({ onClose }: Props) => {
    // const [open, setOpen] = useState<{
    //     Product: boolean
    //     orders: boolean
    //     Analytics: boolean
    // }>(initialSate)
    // const handleOpen = (value: "Product" | "Analytics" | "orders") => {
    //     setOpen((prevState) => {
    //         return {
    //             ...prevState,
    //             [value]: !prevState[value],
    //         }
    //     })
    //     // console.log(local)
    // }
    return (
        <Box
            sx={{
                width: "250px",
            }}
        >
            <List
                sx={{
                    width: "100%",
                    maxWidth: 360,
                    bgcolor: "background.paper",
                    minHeight: "100vh",
                }}
                component="nav"
                aria-labelledby="nested-list-subheader"
                subheader={
                    <ListSubheader
                        component="div"
                        sx={{
                            fontSize: 16,
                            borderRadius: 5,
                            color: "text.primary",
                            position: "static",
                        }}
                    >
                        TechStore Dashboard
                    </ListSubheader>
                }
            >
                <NestedMenuItem icon={<AnalyticsIcon />} menu="Analytics" to="Analytics" />
                <Divider />
                <NestedMenuItem
                    icon={<CategoryIcon />}
                    menu="Product"
                    nestedItemList={productMenu}
                    to="product"
                    // setOpen={() => handleOpen("Product")}
                />
                <Divider />
                <NestedMenuItem
                    to="orders"
                    icon={<DashboardIcon />}
                    menu="Order List"
                    nestedItemList={[]}
                />
                <Divider />
                <NestedMenuItem
                    to="uiPage"
                    icon={<FilterVintageIcon />}
                    menu="UI "
                    nestedItemList={[]}
                />
            </List>
        </Box>
    )
}

export default NavBarMenu
