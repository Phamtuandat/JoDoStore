import EmailIcon from "@mui/icons-material/Email"
import MailOutlineOutlinedIcon from "@mui/icons-material/MailOutlineOutlined"
import {
    Box,
    CardContent,
    CardMedia,
    InputAdornment,
    Paper,
    TextField,
    Typography,
    useTheme,
} from "@mui/material"
import Grid from "@mui/material/Unstable_Grid2"
import { useState } from "react"

const contentList = [
    {
        title: "Fast & Secure Delivery",
        content: "Tell about your service.",
        image: "	https://new.axilthemes.com/demo/template/etrade/assets/images/icons/service1.png",
    },
    {
        title: "Money Back Guarantee",
        content: "Within 10 days.",
        image: "https://new.axilthemes.com/demo/template/etrade/assets/images/icons/service2.png",
    },
    {
        title: "Pro Quality Support",
        content: "24/7 Live support.",
        image: "https://new.axilthemes.com/demo/template/etrade/assets/images/icons/service3.png",
    },
    {
        title: "24 Hour Return Policy",
        content: "No question ask.",
        image: "https://new.axilthemes.com/demo/template/etrade/assets/images/icons/service4.png",
    },
]

const Footer = () => {
    const [state, setState] = useState(false)
    const theme = useTheme()
    const Item = contentList.map((item) => (
        <Grid xs={6} sm={6} lg={3} key={item.content}>
            <Box display="flex" justifyContent={"center"}>
                <CardMedia
                    component="img"
                    image={item.image}
                    alt="green iguana"
                    sx={{ maxWidth: "45px", mr: 1 }}
                />
                <Box>
                    <Typography variant="body2" fontStyle="italic" fontWeight="600">
                        {item.title}
                    </Typography>
                    <Typography
                        variant="body2"
                        fontStyle="italic"
                        sx={{
                            opacity: 0.8,
                        }}
                    >
                        {item.content}
                    </Typography>
                </Box>
            </Box>
        </Grid>
    ))
    return (
        <Box
            sx={{
                display: "flex",
                alignItems: "center",
                flexDirection: "column",
                bgcolor: "background.default",
                color: "text.primary",
                pt: 20,
            }}
        >
            <Box
                sx={{
                    width: {
                        lg: "960px",
                        md: "720px",
                        sm: "540px",
                    },
                }}
            >
                <Box
                    sx={{
                        borderRadius: 3,
                        backgroundImage:
                            "url(https://new.axilthemes.com/demo/template/etrade/assets/images/bg/bg-image-5.jpg)",
                        padding: {
                            xs: "60px 20px 40px", // theme.breakpoints.up('xs')
                            md: "80px 30px 65px", // theme.breakpoints.up('sm')
                            lg: "100px 107px 85px", // theme.breakpoints.up('md')
                        },
                        backgroundSize: "cover",
                        backgroundPosition: "center",
                    }}
                >
                    <Box display="flex" flexDirection="column" color={"#3577f0"}>
                        <Box display="flex">
                            <Box
                                sx={{
                                    borderRadius: "100%",
                                    backgroundColor: "#3577f0",
                                    color: "whitesmoke",
                                    display: "block",
                                    lineHeight: 0,
                                    padding: "2px",
                                }}
                            >
                                <MailOutlineOutlinedIcon />
                            </Box>
                            <Box ml={2} component="span">
                                Newsletter
                            </Box>
                        </Box>
                        <Typography
                            variant="h4"
                            sx={{
                                my: 2,
                                wordBreak: "break-word",
                                lineHeight: 1.3,
                                color: "#292930",
                            }}
                        >
                            Get weekly update
                        </Typography>
                        <Paper
                            sx={{
                                maxWidth: "390px",
                                backgroundColor: "white",
                                borderRadius: 2,
                                minWidth: "342px",
                            }}
                        >
                            <TextField
                                onFocus={() => setState(true)}
                                onBlur={() => setState(false)}
                                color="primary"
                                id="outlined-basic"
                                variant="filled"
                                size="medium"
                                sx={{
                                    "& > :not(style)": { color: "black" },
                                }}
                                InputProps={{
                                    startAdornment: (
                                        <InputAdornment
                                            position="start"
                                            sx={{ color: theme.palette.primary.main }}
                                        >
                                            <Box mr={2}>
                                                <EmailIcon />
                                            </Box>
                                            {!state && (
                                                <Box component="span">example@gmail.com</Box>
                                            )}
                                        </InputAdornment>
                                    ),
                                }}
                                fullWidth
                            />
                        </Paper>
                    </Box>
                </Box>
            </Box>
            <Box sx={{ flexGrow: 2, my: 5 }} width="100%">
                <Grid container spacing={1} justifyContent="space-between">
                    {Item}
                </Grid>
            </Box>
            <CardContent sx={{ display: "flex", flexDirection: "column", textAlign: "center" }}>
                <Typography gutterBottom variant="h5" component="div">
                    Support
                </Typography>
                <Typography variant="body2" color="text.secondary">
                    Tap Phuoc, Long Phuoc, Long Thanh, Dong Nai, Viet Nam.
                </Typography>
            </CardContent>
        </Box>
    )
}

export default Footer
