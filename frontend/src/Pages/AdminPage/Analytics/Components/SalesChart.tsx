import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend,
} from "chart.js"
import { Line } from "react-chartjs-2"
import { faker } from "@faker-js/faker"
import { Box, Typography } from "@mui/material"
import { Theme, useTheme } from "@mui/material/styles"

type Props = {}

ChartJS.register(CategoryScale, LinearScale, PointElement, LineElement, Title, Tooltip, Legend)
export const options = (theme: Theme) => {
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
const labels = ["January", "February", "March", "April", "May", "June", "July"]
export const data = (theme: Theme) => {
    return {
        labels,
        datasets: [
            {
                label: "Facebook",
                data: labels.map(() =>
                    faker.datatype.number({ min: 0, max: 1000, precision: 100 })
                ),
                borderColor: "#1a73e8",
                backgroundColor: "#1a73e8",
                pointBorderColor: "#1a73e8",
                pointBackgroundColor: "#1a73e8",
            },
            {
                label: "Google Ads",
                data: labels.map(() => faker.datatype.number({ min: 0, max: 1000 })),
                borderColor: "#344767",
                backgroundColor: "#344767",
                pointBorderColor: "#344767",
                pointBackgroundColor: "#344767",
            },
        ],
    }
}
const SalesChart = (props: Props) => {
    const theme = useTheme()
    return (
        <Box
            sx={{
                position: "relative",
                pt: 8,
                px: 3,
                pb: 3,
            }}
        >
            <Box position="absolute" fontSize="12px" display="flex" flexDirection="column" top={5}>
                <Typography variant="h6" component="span">
                    Revenue
                </Typography>
                <Box>
                    <Box
                        component="span"
                        mr={5}
                        sx={{
                            position: "relative",
                        }}
                    >
                        <Box
                            component="span"
                            sx={{
                                p: 0.6,
                                bgcolor: "#1a73e8",
                                lineHeight: 0,
                                borderRadius: "50%",
                                display: "inline-block",
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
                </Box>
            </Box>
            <Line options={options(theme)} data={data(theme)} />
        </Box>
    )
}

export default SalesChart
