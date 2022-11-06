import { PaletteMode } from "@mui/material"
import { createTheme, responsiveFontSizes, ThemeProvider } from "@mui/material/styles"
import React, { createContext, useEffect, useMemo, useState } from "react"
import { getDesignTokens } from "utils/CustomThemeMode"
type IAppProps = {
    children: React.ReactNode
}
export const ThemeContext = createContext({ toggleColorMode: () => {} })
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
    let theme = useMemo(() => createTheme(getDesignTokens(mode)), [mode])
    theme = responsiveFontSizes(theme)
    return (
        <ThemeContext.Provider value={colorMode}>
            <ThemeProvider theme={theme}>{props.children}</ThemeProvider>
        </ThemeContext.Provider>
    )
}

export default ColorModeContext
