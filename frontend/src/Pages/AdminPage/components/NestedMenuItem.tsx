import ExpandLess from "@mui/icons-material/ExpandLess"
import ExpandMore from "@mui/icons-material/ExpandMore"
import Collapse from "@mui/material/Collapse"
import { List, Box, ListItem } from "@mui/material"
import ListItemButton from "@mui/material/ListItemButton"
import ListItemIcon from "@mui/material/ListItemIcon"
import ListItemText from "@mui/material/ListItemText"
import React, { ReactNode } from "react"
import { Link as RouterLink } from "react-router-dom"

interface nestedItem {
    title: string
    icon: ReactNode
    nestedLink: string
}

type Props = {
    icon: ReactNode
    menu: string
    nestedItemList: nestedItem[]
    to?: string
}

export const NestedMenuItem = ({ icon, menu, nestedItemList, to }: Props) => {
    const [open, setOpen] = React.useState(true)

    const handleClick = () => {
        setOpen(!open)
    }
    return (
        <Box>
            <ListItem button component={RouterLink as any} to={to} onClick={handleClick}>
                <ListItemIcon>{icon}</ListItemIcon>
                <ListItemText primary={menu} />
                {open ? <ExpandLess /> : <ExpandMore />}
            </ListItem>
            <Collapse in={open} timeout="auto" unmountOnExit>
                <List component="div" disablePadding>
                    {nestedItemList.map((item) => (
                        <RouterLink
                            to={item.nestedLink}
                            key={item.title}
                            style={{ textDecoration: "none", color: "inherit" }}
                        >
                            <ListItemButton sx={{ pl: 4 }} selected>
                                {item.icon}
                                <ListItemText primary={item.title} />
                            </ListItemButton>
                        </RouterLink>
                    ))}
                </List>
            </Collapse>
        </Box>
    )
}
