import { PaletteMode } from "@mui/material"
import { createTheme, ThemeProvider } from "@mui/material/styles"
import React, { createContext, useEffect, useMemo, useState } from "react"
import { getDesignTokens } from "utils/CustomPaletteMode"
type IAppProps = {
    children: React.ReactNode
}
export const ColorMode = createContext({ toggleColorMode: () => {} })
const ColorModeContext = (props: IAppProps) => {
    const currentMode = JSON.parse(localStorage.getItem("uiMode") as PaletteMode)

    const [mode, setMode] = useState<PaletteMode>(currentMode || "light")
    useEffect(() => {
        localStorage.setItem("uiMode", JSON.stringify(mode))
    }, [mode])

    const colorMode = useMemo(
        () => ({
            // The dark mode switch would invoke this method
            toggleColorMode: () => {
                setMode((prevMode: PaletteMode) => (prevMode === "light" ? "dark" : "light"))
            },
        }),
        []
    )
    const theme = useMemo(() => createTheme(getDesignTokens(mode)), [mode])
    return (
        <ColorMode.Provider value={colorMode}>
            <ThemeProvider theme={theme}>{props.children}</ThemeProvider>
        </ColorMode.Provider>
    )
}

export default ColorModeContext
