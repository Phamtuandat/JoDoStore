import { Box, Divider, Typography, useMediaQuery } from "@mui/material"
import { useTheme } from "@mui/material/styles"
import { Link } from "react-router-dom"
import SubHeaderItem from "./SubHeaderItem"
import { useState } from "react"

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
const SubHeader = () => {
    const obj = subHeaderList.reduce((o, key) => Object.assign(o, { [key]: false }), {})
    const [drop, setDrop] = useState<{
        [key: string]: any
    }>(obj)
    const theme = useTheme()
    const mathes = useMediaQuery(theme.breakpoints.up("sm"))
    const handleDropMenu = (item: string, isDrop: boolean) => {
        setDrop({
            [item]: isDrop,
        })
    }
    return (
        <Box
            height={60}
            display="flex"
            sx={{
                flexDirection: { xs: "column", md: "row" },
                justifyContent: { md: "center" },
            }}
        >
            {subHeaderList.map((item) => (
                <Box
                    key={item}
                    sx={{
                        my: { xs: 2, md: 0 },
                        justifyContent: { xs: "flex-start", md: "center" },
                        borderBottom: "0px solid",
                        transition: "0.1s",
                        mx: 2,
                        "&:hover": {
                            borderBottom: "1px solid",
                        },
                        "&>div>a": {
                            textDecoration: "none",
                            color: "text.primary",
                        },
                        display: "flex",
                    }}
                >
                    <Box justifyContent="center" p={1} display="flex">
                        <Typography
                            my="auto"
                            component={Link}
                            to="/shop"
                            onMouseEnter={() => {
                                handleDropMenu(item, true)
                            }}
                        >
                            {item}
                        </Typography>
                    </Box>
                    {!mathes && <Divider />}
                    <Box
                        onMouseLeave={() => {
                            handleDropMenu(item, false)
                        }}
                    >
                        <SubHeaderItem
                            isOpen={drop[item]}
                            categoryList={fakeCategories}
                            key={item}
                        />
                    </Box>
                </Box>
            ))}
        </Box>
    )
}

export default SubHeader
