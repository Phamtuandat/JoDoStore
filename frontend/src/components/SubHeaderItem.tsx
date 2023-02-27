import { Box, Grid, List, ListItem, Paper, Typography } from "@mui/material"
import { Variants, motion } from "framer-motion"
import { Link } from "react-router-dom"

const variants: Variants = {
    open: { opacity: 1, visibility: "visible", height: "auto" },
    closed: { opacity: 0, visibility: "hidden", height: 0 },
}
type linkItem = {
    name: string
    link: string
}
type category = {
    category: string
    linkList: linkItem[]
}
type Props = {
    isOpen?: boolean
    categoryList?: category[]
}

const SubHeaderItem = ({ isOpen, categoryList }: Props) => {
    return (
        <Box
            component={motion.div}
            width="100%"
            position="absolute"
            top="60px"
            zIndex={1}
            variants={variants}
            animate={isOpen ? "open" : "closed"}
            transition={{ type: "spring" }}
            left="0"
            right="0"
            maxHeight="100vh"
        >
            <Paper elevation={0}>
                <Box px={20} py={10}>
                    <Grid container>
                        {categoryList?.map((category) => (
                            <Grid item md={3} sm={4} key={category.category}>
                                <Typography variant="body1">{category.category}</Typography>
                                <List>
                                    {category.linkList.map((link) => (
                                        <ListItem key={link.name} disableGutters>
                                            <Typography
                                                component={Link}
                                                to={link.link}
                                                variant="body2"
                                                sx={{
                                                    textDecoration: "none",
                                                    color: "text.primary",
                                                    opacity: 0.7,
                                                    "&:hover": {
                                                        opacity: 1,
                                                    },
                                                }}
                                            >
                                                {link.name}
                                            </Typography>
                                        </ListItem>
                                    ))}
                                </List>
                            </Grid>
                        ))}
                    </Grid>
                </Box>
            </Paper>
        </Box>
    )
}

export default SubHeaderItem
