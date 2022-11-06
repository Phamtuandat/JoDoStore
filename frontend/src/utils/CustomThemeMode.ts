import { PaletteMode } from "@mui/material"

export const getDesignTokens = (mode: PaletteMode) => ({
    transitions: {
        duration: {
            shortest: 150,
            shorter: 200,
            short: 250,
            // most basic recommended timing
            standard: 300,
            // this is to be used in complex animations
            complex: 500,
            // recommended when something is entering screen
            enteringScreen: 225,
            // recommended when something is leaving screen
            leavingScreen: 195,
        },
        easing: {
            // This is the most common easing curve.
            easeInOut: "cubic-bezier(0.4, 0, 0.2, 1)",
            // Objects enter the screen at full velocity from off-screen and
            // slowly decelerate to a resting point.
            easeOut: "cubic-bezier(0.0, 0, 0.2, 1)",
            // Objects leave the screen at full velocity. They do not decelerate when off-screen.
            easeIn: "cubic-bezier(0.4, 0, 1, 1)",
            // The sharp curve is used by objects that may return to the screen at any time.
            sharp: "cubic-bezier(0.4, 0, 0.6, 1)",
        },
    },
    typography: {
        fontFamily: ["DM Sans", "sans-serif"].join(","),
    },
    components: {
        // Name of the component
        MuiButton: {
            styleOverrides: {
                // Name of the slot
                root: {
                    // Some CSS
                    fontSize: "1rem",
                },
            },
        },
    },

    palette: {
        mode,
        ...(mode === "light"
            ? {
                  // palette values for light mode
                  primary: {
                      main: "#e1f5fe",
                  },
                  divider: "#7D9D9C",
                  background: {
                      default: "#fff",
                      paper: "#f6f7fb",
                  },
                  text: {
                      primary: "#292930",
                      secondary: "#ff497c",
                  },
                  secondary: {
                      main: "#ff497c",
                  },
              }
            : {
                  // palette values for dark mode
                  primary: {
                      main: "#bbb",
                  },
                  divider: "#A5C9CA",
                  background: {
                      default: "#292930",
                      paper: "#1B2430cc",
                  },
                  text: {
                      primary: "#fff",
                      secondary: "#ccc",
                  },
                  secondary: {
                      main: "#ff497c",
                  },
                  info: {
                      main: "#1B2430cc",
                      light: "#f6f7fb",
                      dark: "#1B2430cc",
                  },
              }),
    },
})
