import ExpandLess from "@mui/icons-material/ExpandLess"
import ExpandMore from "@mui/icons-material/ExpandMore"
import { Box, List, ListItem } from "@mui/material"
import Collapse from "@mui/material/Collapse"
import ListItemButton from "@mui/material/ListItemButton"
import ListItemIcon from "@mui/material/ListItemIcon"
import ListItemText from "@mui/material/ListItemText"
import { useTheme } from "@mui/material/styles"
import { ReactNode } from "react"
import { Link as RouterLink, useLocation } from "react-router-dom"
interface nestedItem {
    title: string
    icon: ReactNode
    nestedLink: string
    pathName: string
}

type Props = {
    icon: ReactNode
    menu: string
    nestedItemList?: nestedItem[]
    to?: string
    open?: boolean
    setOpen?: () => void
}

export const NestedMenuItem = ({ icon, menu, nestedItemList, to, open, setOpen }: Props) => {
    const local = useLocation()

    const theme = useTheme()

    const handleClick = () => {
        if (setOpen) {
            setOpen()
        }
    }
    return (
        <Box>
            <ListItem
                button
                component={RouterLink as any}
                to={!nestedItemList && to}
                onClick={handleClick}
            >
                <ListItemIcon>{icon}</ListItemIcon>
                <ListItemText primary={menu} />
                {!!nestedItemList && <>{open ? <ExpandLess /> : <ExpandMore />}</>}
            </ListItem>
            {!!nestedItemList && (
                <Collapse in={open} timeout="auto" unmountOnExit>
                    <List component="div" disablePadding>
                        {nestedItemList.map((item, id) => (
                            <RouterLink
                                to={item.nestedLink}
                                key={item.title}
                                style={{
                                    textDecoration: "none",
                                    color: "inherit",
                                }}
                            >
                                <Box
                                    sx={{
                                        py: 0.3,
                                        px: 1,
                                    }}
                                >
                                    <ListItemButton
                                        sx={{
                                            pl: 4,
                                            bgcolor:
                                                local.pathname === item.pathName
                                                    ? theme.palette.primary.main
                                                    : "none",

                                            borderRadius: 3,
                                        }}
                                        // onClick={(event) => handleListItemClick(event, id)}
                                    >
                                        {item.icon}
                                        <ListItemText primary={item.title} />
                                    </ListItemButton>
                                </Box>
                            </RouterLink>
                        ))}
                    </List>
                </Collapse>
            )}
        </Box>
    )
}
