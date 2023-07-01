import Brightness4Icon from "@mui/icons-material/Brightness4"
import Brightness7Icon from "@mui/icons-material/Brightness7"
import SendIcon from "@mui/icons-material/Send"
import { Box, Grow, Paper } from "@mui/material"
import SpeedDial from "@mui/material/SpeedDial"
import SpeedDialAction from "@mui/material/SpeedDialAction"
import SpeedDialIcon from "@mui/material/SpeedDialIcon"
import { useTheme } from "@mui/material/styles"
import { ThemeContext } from "Context/ColorModeContext"
import Chatbox from "components/ChatBox/Chatbox"
import { useContext, useState } from "react"

type IProp = {
    label: string
}

export default function BasicSpeedDial({ label }: IProp) {
    const colorMode = useContext(ThemeContext)
    const theme = useTheme()
    const [open, setOpen] = useState(false)
    const icon = (
        <Paper sx={{ m: 1 }} elevation={4}>
            <Box
                sx={{
                    width: "300px",
                    height: "400px",
                    position: "relative",
                }}
            >
                <Box
                    onClick={() => setOpen(false)}
                    sx={{
                        position: "absolute",
                        top: 0,
                        right: 0,
                        cursor: "pointer",
                        backgroundColor: theme.palette.primary.light,
                        px: 1,
                        boxShadow: theme.shadows[2],
                    }}
                >
                    X
                </Box>
                <Chatbox />
            </Box>
        </Paper>
    )
    return (
        <Box>
            <SpeedDial
                ariaLabel={label}
                sx={{
                    position: "absolute",
                    bottom: 16,
                    right: 16,
                }}
                icon={<SpeedDialIcon />}
            >
                <SpeedDialAction
                    onClick={() => {
                        colorMode.toggleColorMode()
                    }}
                    icon={theme.palette.mode === "dark" ? <Brightness7Icon /> : <Brightness4Icon />}
                    tooltipTitle={theme.palette.mode}
                />
                <SpeedDialAction
                    onClick={() => {
                        setOpen(true)
                    }}
                    icon={<SendIcon />}
                    tooltipTitle={"Message"}
                />
            </SpeedDial>
            <Box sx={{ display: "flex", position: "absolute", bottom: 0, right: 80 }}>
                <Grow
                    in={open}
                    style={{ transformOrigin: "100px 100px 100px" }}
                    {...(open ? { timeout: 200 } : {})}
                >
                    {icon}
                </Grow>
            </Box>
        </Box>
    )
}
