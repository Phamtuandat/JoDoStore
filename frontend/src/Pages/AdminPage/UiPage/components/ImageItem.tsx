import HighlightOff from "@mui/icons-material/HighlightOff"
import { Box, ImageListItemBar } from "@mui/material"
import CircularProgress from "@mui/material/CircularProgress"
import IconButton from "@mui/material/IconButton"
import ImageListItem from "@mui/material/ImageListItem"
import { Photo } from "models"
import { useState } from "react"
type IProps = {
    item: Photo
    handleRemove: (id: number) => Promise<void>
}

const ImageItem = ({ item, handleRemove }: IProps) => {
    const [isLoading, setLoading] = useState(false)

    const onRemove = async (id: number) => {
        setLoading(true)
        await handleRemove(id)
        setLoading(false)
    }

    return (
        <div>
            <ImageListItem>
                <ImageListItemBar
                    sx={{
                        background:
                            "linear-gradient(to bottom, rgba(0,0,0,0.7) 0%, " +
                            "rgba(0,0,0,0.3) 70%, rgba(0,0,0,0) 100%)",
                    }}
                    position="top"
                    actionIcon={
                        <IconButton sx={{ color: "red" }} onClick={() => onRemove(+item.id)}>
                            <HighlightOff />
                        </IconButton>
                    }
                />
                <Box
                    sx={{
                        position: "absolute",
                        top: "50%",
                        left: "50%",
                        transform: "translate(-50%, -50%)",
                        display: isLoading ? "flex" : "none",
                        width: "100%",
                        height: "100%",
                        justifyContent: "center",
                        alignItems: "center",
                        bgcolor: "#81818154",
                    }}
                >
                    <CircularProgress />
                </Box>
                <img
                    src={`${item.imageUrl}?w=164&h=164&fit=crop&auto=format`}
                    srcSet={`${item.imageUrl}?w=164&h=164&fit=crop&auto=format&dpr=2 2x`}
                    alt={item.title}
                    loading="lazy"
                />
            </ImageListItem>
        </div>
    )
}

export default ImageItem
