import HighlightOffIcon from "@mui/icons-material/HighlightOff"
import { Box, CardMedia, Paper, Stack } from "@mui/material"
import { useTheme } from "@mui/material/styles"
import { ImageFeild } from "components/inputField/ImageFeild"
import { useEffect, useState } from "react"
import { Control } from "react-hook-form"
type Props = {
    control: Control<any>
    setValue: (values: File[]) => void
    value: any
}
type FileObj = {
    file: File
    url: string
}
const ProductMedia = ({ control, setValue, value }: Props) => {
    const [files, setfiles] = useState<FileObj[]>([])
    const theme = useTheme()
    const handleReview = (value: FileList) => {
        let fileList: FileObj[] = []
        if (value) {
            for (let i = 0; i < value.length; i++) {
                const file = value[i]
                const url = URL.createObjectURL(file)
                fileList.push({
                    file: value[i],
                    url,
                })
            }
            setfiles((prv) => prv.concat(fileList))
        }
        setValue(files.concat(fileList).map((file) => file.file))
    }
    useEffect(() => {
        let fileList: FileObj[] = []
        if (value) {
            for (let i = 0; i < value.length; i++) {
                const file = value[i]
                const url = URL.createObjectURL(file)
                fileList.push({
                    file: value[i],
                    url,
                })
            }
            setfiles(fileList)
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])

    const removeItem = (url: string) => {
        setfiles((prev) => prev.filter((x) => x.url !== url))
    }
    return (
        <Box>
            <Box
                sx={{
                    p: 2,
                    height: "160px",
                    overflowX: "scroll",
                    width: "auto",
                    borderRadius: 1,
                    border: `solid 1px ${theme.palette.divider}`,
                    "&:hover::-webkit-scrollbar": {
                        display: "block",
                        height: "4px",
                    },
                    "&::-webkit-scrollbar": {
                        width: "0.512rem",
                        height: "4px",
                    },
                    "&::-webkit-scrollbar-track": {
                        boxShadow: "inset 0 0 6px rgba(0,0,0,0.00)",
                        webkitBoxShadow: "inset 0 0 6px rgba(0,0,0,0.00)",
                    },
                    "&::-webkit-scrollbar-thumb": {
                        backgroundColor: "#8d8e90",
                        height: "4px",
                        borderRadius: "8px",
                    },
                }}
            >
                <Box my="auto" display="flex" flexDirection="column" justifyContent="center">
                    <Stack direction="row" spacing={2}>
                        {files.map((file) => (
                            <Paper
                                elevation={5}
                                key={file.url}
                                sx={{
                                    position: "relative",
                                    "&:hover>span": {
                                        display: "block",
                                    },
                                }}
                            >
                                <Box
                                    component="span"
                                    onClick={() => removeItem(file.url)}
                                    color={theme.palette.error.light}
                                    position="absolute"
                                    display="none"
                                    sx={{
                                        cursor: "pointer",
                                        right: "50%",
                                        bottom: "50%",
                                        transform: "translate(50%, 50%)",
                                        boxShadow: theme.shadows[7],
                                        p: 5,
                                        bgcolor: "#1b243052",
                                    }}
                                >
                                    <HighlightOffIcon />
                                </Box>
                                <CardMedia
                                    component="img"
                                    image={file.url}
                                    sx={{ width: "100px", height: "100px" }}
                                />
                            </Paper>
                        ))}
                    </Stack>
                </Box>
            </Box>
            <ImageFeild name="thumbnail" control={control} handleUpload={handleReview} />
        </Box>
    )
}

export default ProductMedia
