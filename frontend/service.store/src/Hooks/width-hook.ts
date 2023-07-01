import { Breakpoint, Theme, useTheme } from "@mui/material/styles"
import useMediaQuery from "@mui/material/useMediaQuery"

type BreakpointOrNull = Breakpoint | null

export function useWidth() {
    const theme: Theme = useTheme()
    const keys: readonly Breakpoint[] = [...theme.breakpoints.keys].reverse()
    return (
        keys.reduce((output: BreakpointOrNull, key: Breakpoint) => {
            // eslint-disable-next-line react-hooks/rules-of-hooks
            const matches = useMediaQuery(theme.breakpoints.up(key))
            return !output && matches ? key : output
        }, null) || "xs"
    )
}
