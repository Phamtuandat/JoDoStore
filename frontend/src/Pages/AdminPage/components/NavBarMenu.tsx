import CategoryIcon from "@mui/icons-material/Category"
import DashboardIcon from "@mui/icons-material/Dashboard"
import { Box, ListSubheader } from "@mui/material"
import List from "@mui/material/List"
import { styled } from "@mui/material/styles"
import { NestedMenuItem } from "./NestedMenuItem"
type Props = {}

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
    },
    {
        icon: <ListItemIconCustom>E</ListItemIconCustom>,
        title: "Edit Product",
        nestedLink: "product/edit",
    },
    {
        icon: <ListItemIconCustom>P</ListItemIconCustom>,
        title: "Product Page",
        nestedLink: "product/list",
    },
]
const NavBarMenu = (props: Props) => {
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
                <NestedMenuItem
                    icon={<DashboardIcon />}
                    menu="Dashboard"
                    nestedItemList={[]}
                    to={""}
                />
                <NestedMenuItem
                    icon={<CategoryIcon />}
                    menu="Product"
                    nestedItemList={productMenu}
                    to="product"
                />
                <NestedMenuItem
                    to={""}
                    icon={<DashboardIcon />}
                    menu="Dashboard"
                    nestedItemList={[]}
                />
                <NestedMenuItem
                    to={""}
                    icon={<DashboardIcon />}
                    menu="Dashboard"
                    nestedItemList={[]}
                />
                <NestedMenuItem
                    to={""}
                    icon={<DashboardIcon />}
                    menu="Dashboard"
                    nestedItemList={[]}
                />
            </List>
        </Box>
    )
}

export default NavBarMenu
