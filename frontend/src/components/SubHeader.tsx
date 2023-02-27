import ExpandMoreIcon from "@mui/icons-material/ExpandMore"
import { Box, Divider, Typography, useMediaQuery } from "@mui/material"
import { Link, useLocation } from "react-router-dom"
import { useTheme } from "@mui/material/styles"

const SubHeader = () => {
    const location = useLocation()
    const theme = useTheme()
    const mathes = useMediaQuery(theme.breakpoints.up("sm"))
    return (
        <Box
            height={60}
            display="flex"
            sx={{
                flexDirection: { xs: "column", md: "row" },
                justifyContent: { md: "center" },
                "&>a": {
                    my: { xs: 2, md: 0 },
                    justifyContent: { xs: "flex-start", md: "center" },
                    color: mathes && location.pathname === "/" ? "text.secondary" : "text.primary",
                },
            }}
        >
            <Box
                component={Link}
                to="/shop"
                p={1}
                mx={1}
                sx={{
                    transition: "all 0.3s ease-in-out",
                    textDecoration: "none",
                    "&:hover": {
                        bgcolor: { md: "white" },
                        color: { md: "black" },
                    },
                }}
                justifyContent="center"
                display="flex"
            >
                <Typography my="auto" fontSize={18} component="span" fontWeight={700}>
                    SHOP
                </Typography>
                {mathes && <ExpandMoreIcon fontSize="large" sx={{ alignSelf: "center" }} />}
            </Box>
            {!mathes && <Divider />}

            <Box
                justifyContent="center"
                component={Link}
                to="/shop"
                p={1}
                mx={1}
                sx={{
                    transition: "all 0.3s ease-in-out",
                    textDecoration: "none",
                    "&:hover": {
                        bgcolor: { md: "white" },
                        color: { md: "black" },
                    },
                }}
                display="flex"
            >
                <Typography my="auto" fontSize={18} component="span" fontWeight={700}>
                    FEATURED
                </Typography>
                {mathes && <ExpandMoreIcon fontSize="large" sx={{ alignSelf: "center" }} />}
            </Box>
            {!mathes && <Divider />}
            <Box
                component={Link}
                to="/shop"
                p={1}
                mx={1}
                sx={{
                    transition: "all 0.3s ease-in-out",
                    textDecoration: "none",
                    "&:hover": {
                        bgcolor: { md: "white" },
                        color: { md: "black" },
                    },
                }}
                justifyContent="center"
                display="flex"
            >
                <Typography my="auto" fontSize={18} component="span" fontWeight={700}>
                    LOOKBOOK
                </Typography>
                {mathes && <ExpandMoreIcon fontSize="large" sx={{ alignSelf: "center" }} />}
            </Box>
            {!mathes && <Divider />}
            <Box
                component={Link}
                to="/shop"
                p={1}
                mx={1}
                sx={{
                    transition: "all 0.3s ease-in-out",
                    textDecoration: "none",
                    "&:hover": {
                        bgcolor: { md: "white" },
                        color: { md: "black" },
                    },
                }}
                justifyContent="center"
                display="flex"
            >
                <Typography my="auto" fontSize={18} component="span" fontWeight={700}>
                    JOURNAL
                </Typography>
                {mathes && <ExpandMoreIcon fontSize="large" sx={{ alignSelf: "center" }} />}
            </Box>
            {!mathes && <Divider />}
            <Box
                justifyContent="center"
                component={Link}
                to="/shop"
                p={1}
                mx={1}
                sx={{
                    transition: "all 0.3s ease-in-out",
                    textDecoration: "none",
                    "&:hover": {
                        bgcolor: { md: "white" },
                        color: { md: "black" },
                    },
                }}
                display="flex"
            >
                <Typography my="auto" fontSize={18} component="span" fontWeight={700}>
                    ABOUT
                </Typography>
                {mathes && <ExpandMoreIcon fontSize="large" sx={{ alignSelf: "center" }} />}
            </Box>
            {!mathes && <Divider />}
        </Box>
    )
}

export default SubHeader
