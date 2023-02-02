import { Box, Grid, Paper } from "@mui/material"
import Channels from "./Components/Channels"
import SalesChart from "./Components/SalesChart"

type Props = {}

const AnalyticsPage = (props: Props) => {
    return (
        <Box mt={5}>
            <Grid container spacing={2}>
                <Grid item lg={4}>
                    <Paper
                        sx={{
                            borderRadius: 2,
                            overflow: "hidden",
                            height: "100%",
                        }}
                        elevation={3}
                    >
                        <Channels />
                    </Paper>
                </Grid>
                <Grid item lg={8}>
                    <Paper
                        sx={{
                            borderRadius: 2,
                            overflow: "hidden",
                        }}
                        elevation={3}
                    >
                        <SalesChart />
                    </Paper>
                </Grid>
            </Grid>
        </Box>
    )
}

export default AnalyticsPage
