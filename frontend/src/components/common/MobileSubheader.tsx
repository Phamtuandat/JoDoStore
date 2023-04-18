import ArrowForwardIosIcon from "@mui/icons-material/ArrowForwardIos"
import { Box, Stack, Typography } from "@mui/material"
import { useTheme } from "@mui/material/styles"
import { useState } from "react"
import { Link } from "react-router-dom"
import SwipeableViews from "react-swipeable-views"
type Props = {}
const subHeaderList = ["MEN", "WOMEN", "KIDS", "CUSTOMISE", "SALE"]
const fakeCategories = [
    {
        category: "New & Featured",
        linkList: [
            {
                name: "New Release",
                link: "/shop",
            },
            {
                name: "Member Access",
                link: "/shop",
            },
            {
                name: "Couple Wear ",
                link: "/shop",
            },
            {
                name: "Basic Essentials ",
                link: "/shop",
            },
            {
                name: "Football Club Kit ",
                link: "/shop",
            },
            {
                name: "Top Pick Under",
                link: "/shop",
            },
            {
                name: "Sale ",
                link: "/shop",
            },
        ],
    },
    {
        category: "Shoes",
        linkList: [
            {
                name: "New Release",
                link: "/shop",
            },
            {
                name: "Member Access",
                link: "/shop",
            },
            {
                name: "Couple Wear ",
                link: "/shop",
            },
            {
                name: "Basic Essentials ",
                link: "/shop",
            },
            {
                name: "Football Club Kit ",
                link: "/shop",
            },
            {
                name: "Top Pick Under",
                link: "/shop",
            },
            {
                name: "Sale ",
                link: "/shop",
            },
        ],
    },
    {
        category: "Clothing",
        linkList: [
            {
                name: "New Release",
                link: "/shop",
            },
            {
                name: "Member Access",
                link: "/shop",
            },
            {
                name: "Couple Wear ",
                link: "/shop",
            },
            {
                name: "Basic Essentials ",
                link: "/shop",
            },
            {
                name: "Football Club Kit ",
                link: "/shop",
            },
            {
                name: "Top Pick Under",
                link: "/shop",
            },
            {
                name: "Sale ",
                link: "/shop",
            },
        ],
    },
    {
        category: "Shop By Sport",
        linkList: [
            {
                name: "Rnning",
                link: "/shop",
            },
            {
                name: "Football",
                link: "/shop",
            },
            {
                name: "Basketball ",
                link: "/shop",
            },
            {
                name: "Volleyball ",
                link: "/shop",
            },
            {
                name: "GYM and Tranning",
                link: "/shop",
            },
            {
                name: "Yoga",
                link: "/shop",
            },
            {
                name: "Tennis ",
                link: "/shop",
            },
            {
                name: "Sketeboarding ",
                link: "/shop",
            },
        ],
    },
]
interface TabPanelProps {
    children?: React.ReactNode
    index: number
    value: number
}
function TabPanel(props: TabPanelProps) {
    const { children, value, index, ...other } = props

    return (
        <div
            role="tabpanel"
            hidden={value !== index}
            id={`full-width-tabpanel-${index}`}
            aria-labelledby={`full-width-tab-${index}`}
            {...other}
        >
            {value === index && <Box sx={{ p: 3 }}>{children}</Box>}
        </div>
    )
}
const MobileSubheader = (props: Props) => {
    const theme = useTheme()
    const [ind, setInd] = useState(0)

    const handleDropMenu = (item: string) => {
        setInd(1)
    }
    return (
        <SwipeableViews axis={theme.direction === "rtl" ? "x-reverse" : "x"} index={ind}>
            <TabPanel index={0} value={ind}>
                <Box
                    display="flex"
                    sx={{
                        flexDirection: "column",
                        justifyContent: "center",
                    }}
                >
                    {subHeaderList.map((item) => (
                        <Box
                            key={item}
                            sx={{
                                cursor: "pointer",
                                my: 2,
                                borderBottom: "0px solid",
                                transition: "0.1s",
                                mx: 2,
                                "&>div>a": {
                                    textDecoration: "none",
                                    color: "text.primary",
                                },
                                display: "flex",
                                justifyContent: "space-between",
                            }}
                        >
                            <Box p={1} display="flex" onClick={() => handleDropMenu(item)}>
                                <Typography my="auto">{item}</Typography>
                            </Box>
                            <ArrowForwardIosIcon />
                        </Box>
                    ))}
                </Box>
            </TabPanel>
            <TabPanel index={1} value={ind}>
                <Stack>
                    {fakeCategories.map((x) => (
                        <Box
                            key={x.category}
                            sx={{
                                my: 2,
                                borderBottom: "0px solid",
                                transition: "0.1s",
                                mx: 2,
                                "&>a": {
                                    textDecoration: "none",
                                    color: "text.primary",
                                },
                                display: "flex",
                                justifyContent: "space-between",
                                cursor: "pointer",
                            }}
                        >
                            <Typography component={Link} to="/shop" my="auto">
                                {x.category}
                            </Typography>
                        </Box>
                    ))}
                </Stack>
            </TabPanel>
        </SwipeableViews>
    )
}

export default MobileSubheader
