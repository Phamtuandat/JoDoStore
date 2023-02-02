import { Box, Button, Grid, Typography } from "@mui/material"
import { blue, deepOrange, deepPurple } from "@mui/material/colors"
import { Theme, useTheme } from "@mui/material/styles"
import { ArcElement, Chart as ChartJS, Legend, Tooltip } from "chart.js"
import { Pie } from "react-chartjs-2"
type Props = {}
ChartJS.register(ArcElement, Tooltip, Legend)

export const data = (theme: Theme) => {
    return {
        labels: ["Facebook", "Purple", "Orange"],
        datasets: [
            {
                label: "# of Votes",
                data: [2, 2, 3],
                backgroundColor: [blue[900], deepPurple[900], deepOrange[900]],
                borderColor: theme.palette.background.paper,
                borderWidth: 2,
            },
        ],
    }
}
const options = (theme: Theme) => {
    return {
        responsive: true,
        plugins: {
            legend: {
                position: "top" as const,
                display: false,
            },
            title: {
                display: false,
                text: "Revenue",
                font: {
                    size: 18,
                },
                color: theme.palette.text.primary,
            },
        },
    }
}
const Channels = (props: Props) => {
    const theme = useTheme()
    return (
        <Box
            sx={{
                p: 2,
                display: "flex",
                flexDirection: "column",
                height: "100%",
            }}
        >
            <Typography fontWeight={900}>Channels</Typography>
            <Grid container mt={2} spacing={3}>
                <Grid item lg={8}>
                    <Pie data={data(theme)} options={options(theme)} />
                </Grid>
                <Grid item lg={4} fontSize="12px">
                    <Box component="span" mr={5} display="flex" alignItems="center">
                        <Box
                            component="span"
                            sx={{
                                p: 0.6,
                                bgcolor: "#1a73e8",
                                borderRadius: "50%",
                                display: "block",
                                mr: 1,
                            }}
                        />
                        Facebook
                    </Box>
                    <Box component="span">
                        <Box
                            component="span"
                            sx={{
                                p: 0.6,
                                bgcolor: "#344767",
                                lineHeight: 0,
                                borderRadius: "50%",
                                display: "inline-block",
                                mr: 1,
                            }}
                        />
                        Google Ads
                    </Box>
                </Grid>
            </Grid>
            <Box mt="auto" display="flex" justifyContent="space-between">
                <Typography variant="caption" width="200px">
                    More than 1,200,000 sales are made using referral marketing, and 700,000 are
                    from social media.
                </Typography>
                <Box mt="auto">
                    <Button variant="contained" size="small">
                        Read more
                    </Button>
                </Box>
            </Box>
        </Box>
    )
}

export default Channels
