import Brightness4Icon from "@mui/icons-material/Brightness4"
import Brightness7Icon from "@mui/icons-material/Brightness7"
import SpeedDial from "@mui/material/SpeedDial"
import SpeedDialAction from "@mui/material/SpeedDialAction"
import SpeedDialIcon from "@mui/material/SpeedDialIcon"
import { useTheme } from "@mui/material/styles"
import { ThemeContext } from "Context/ColorModeContext"
import { useContext } from "react"

type IProp = {
    label: string
}

export default function BasicSpeedDial({ label }: IProp) {
    const colorMode = useContext(ThemeContext)
    const theme = useTheme()
    return (
        <SpeedDial
            ariaLabel={label}
            sx={{ position: "absolute", bottom: 16, right: 16 }}
            icon={<SpeedDialIcon />}
        >
            <SpeedDialAction
                onClick={colorMode.toggleColorMode}
                icon={theme.palette.mode === "dark" ? <Brightness7Icon /> : <Brightness4Icon />}
                tooltipTitle={theme.palette.mode}
            />
        </SpeedDial>
    )
}
