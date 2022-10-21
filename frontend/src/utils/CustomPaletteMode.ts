import { PaletteMode } from "@mui/material"
import { blueGrey, brown } from "@mui/material/colors"

export const getDesignTokens = (mode: PaletteMode) => ({
    palette: {
        mode,
        ...(mode === "light"
            ? {
                  // palette values for light mode
                  primary: brown,
                  divider: "#7D9D9C",
                  background: {
                      default: "#E4DCCF",
                      paper: "#F0EBE3",
                  },
                  text: {
                      primary: "#576F72",
                      secondary: "#9F8772",
                  },
              }
            : {
                  // palette values for dark mode
                  primary: blueGrey,
                  divider: "#A5C9CA",
                  background: {
                      default: "#395B64",
                      paper: "#1B2430",
                  },
                  text: {
                      primary: "#F7CCAC",
                      secondary: "#826F66",
                  },
              }),
    },
})
