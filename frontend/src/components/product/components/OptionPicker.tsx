import { Box, Checkbox, Typography } from "@mui/material"
import { useTheme } from "@mui/material/styles"
import { useState } from "react"
type Props = {}
const OptionPicker = (props: Props) => {
    const [colorOption, setColor] = useState<"green" | "pink" | "violet">("violet")
    const [sizeOption, setSizeOption] = useState<"XS" | "S" | "M" | "L" | "XL">("S")
    const theme = useTheme()
    const handleChange = (name: "green" | "pink" | "violet") => {
        setColor(name)
        console.log(name)
    }

    return (
        <Box>
            <Box display="flex" mt={{ xs: 2, md: 6 }}>
                <Typography alignSelf="center" variant="h6" mr={3}>
                    Colors :
                </Typography>
                <Box
                    bgcolor="transparent"
                    display="flex"
                    width="24px"
                    height="24px"
                    justifyContent="center"
                    border={colorOption === "violet" ? "solid 2px violet" : "none"}
                    borderRadius="50%"
                >
                    <Checkbox
                        color="secondary"
                        checked={colorOption === "violet"}
                        onChange={() => handleChange("violet")}
                        icon={<Box component="span" p={1} bgcolor="violet" borderRadius="50%" />}
                        checkedIcon={
                            <Box component="span" p={1} bgcolor="violet" borderRadius="50%" />
                        }
                    />
                </Box>
                <Box
                    ml={1}
                    bgcolor="transparent"
                    display="flex"
                    width="24px"
                    height="24px"
                    justifyContent="center"
                    border={colorOption === "green" ? "solid 2px green" : "none"}
                    borderRadius="50%"
                >
                    <Checkbox
                        color="secondary"
                        checked={colorOption === "green"}
                        onChange={() => handleChange("green")}
                        icon={<Box component="span" p={1} bgcolor="green" borderRadius="50%" />}
                        checkedIcon={
                            <Box component="span" p={1} bgcolor="green" borderRadius="50%" />
                        }
                    />
                </Box>
            </Box>
            <Box display="flex" mt={{ xs: 2, md: 6 }}>
                <Typography alignSelf="center" variant="h6" mr={3}>
                    Size :
                </Typography>

                <Checkbox
                    sx={{ textAlign: "center" }}
                    color="secondary"
                    checked={sizeOption === "XS"}
                    onChange={() => setSizeOption("XS")}
                    icon={
                        <Box
                            display="block"
                            component="span"
                            p={1}
                            bgcolor="transparent"
                            borderRadius="50%"
                            width="42px"
                            height="42px"
                        >
                            XS
                        </Box>
                    }
                    checkedIcon={
                        <Box
                            component="span"
                            p={1}
                            bgcolor="transparent"
                            borderRadius="50%"
                            fontWeight={700}
                            color={theme.palette.action.selected}
                            border="solid 2px"
                            width="42px"
                            height="42px"
                        >
                            XS
                        </Box>
                    }
                />
                <Checkbox
                    sx={{ textAlign: "center" }}
                    color="secondary"
                    checked={sizeOption === "M"}
                    onChange={() => setSizeOption("M")}
                    icon={
                        <Box
                            display="block"
                            component="span"
                            p={1}
                            bgcolor="transparent"
                            borderRadius="50%"
                            width="42px"
                            height="42px"
                            justifyContent="center"
                        >
                            M
                        </Box>
                    }
                    checkedIcon={
                        <Box
                            display="flex"
                            justifyContent="center"
                            component="span"
                            p={1}
                            bgcolor="transparent"
                            borderRadius="50%"
                            fontWeight={700}
                            color={theme.palette.action.selected}
                            border="solid 2px"
                            width="42px"
                            height="42px"
                        >
                            M
                        </Box>
                    }
                />
                <Checkbox
                    sx={{ textAlign: "center" }}
                    color="secondary"
                    checked={sizeOption === "L"}
                    onChange={() => setSizeOption("L")}
                    icon={
                        <Box
                            display="block"
                            component="span"
                            p={1}
                            bgcolor="transparent"
                            borderRadius="50%"
                            width="42px"
                            height="42px"
                            justifyContent="center"
                        >
                            L
                        </Box>
                    }
                    checkedIcon={
                        <Box
                            display="flex"
                            justifyContent="center"
                            component="span"
                            p={1}
                            bgcolor="transparent"
                            borderRadius="50%"
                            fontWeight={700}
                            color={theme.palette.action.selected}
                            border="solid 2px"
                            width="42px"
                            height="42px"
                        >
                            L
                        </Box>
                    }
                />
            </Box>
        </Box>
    )
}

export default OptionPicker
