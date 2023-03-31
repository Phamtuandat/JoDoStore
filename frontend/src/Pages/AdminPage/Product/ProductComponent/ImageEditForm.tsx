import NavigateBeforeIcon from "@mui/icons-material/NavigateBefore"
import NavigateNextIcon from "@mui/icons-material/NavigateNext"
import { Box, Button, CardMedia, CircularProgress } from "@mui/material"
import PhotoApi from "ApiClients/PhotoApi"
import { ImageFeild } from "components/inputField/ImageFeild"
import { motion } from "framer-motion"
import { Photo, Product } from "models"
import buildQuery from "odata-query"
import { useEffect, useRef, useState } from "react"
import { Control } from "react-hook-form"
import handleNotify from "utils/Toast-notify"
// type FileObj = {
//     file: File
//     url: string
// }
type Props = {
    product: Product
    control: Control<any>
    productId: string | undefined
}

const ImageEditForm = ({ product, control, productId }: Props) => {
    const ignore = useRef(false)
    const [isLoading, setLoading] = useState(false)
    const [thumbs, setThumbs] = useState<Photo[]>([])
    const [idx, setIdx] = useState(0)
    const [isHover, setHover] = useState<boolean>(false)
    useEffect(() => {
        if (!ignore.current) {
            ignore.current = true
            if (productId) {
                const query = buildQuery({
                    filter: {
                        productId: +productId,
                    },
                })
                ;(async () => {
                    const result = await PhotoApi.getAll(query)
                    setThumbs(result.data)
                })()
            }
        }
    }, [productId])
    const handleReview = async (value: FileList) => {
        setLoading(true)
        if (value.length > 0) {
            for (let i = 0; i < value.length; i++) {
                const formData = new FormData()
                formData.append("title", product.name)
                formData.append("productId", `${product.id}`)
                formData.append("formFile", value[i])
                formData.append("Description", `${" None "}`)
                formData.append("collections", "Product Image")
                try {
                    var result = await PhotoApi.create(formData)
                    setThumbs(thumbs.concat(result.data))
                } catch (error) {
                    console.log(error)
                }
            }
        }
        setLoading(false)
    }

    const handleRemove = async (id: number | string) => {
        try {
            setLoading(true)
            await PhotoApi.delete(+id)
            setThumbs(thumbs.filter((x) => x.id !== id))
            setLoading(false)
            if (idx === thumbs.length - 1) {
                setIdx(thumbs.length - 2)
            }
            handleNotify.success()
        } catch (error) {
            handleNotify.error(error as string)
        }
    }
    return (
        <Box
            onMouseEnter={() => setHover((prv) => !prv)}
            onMouseLeave={() => setHover((prv) => !prv)}
            sx={{
                display: "flex",
                flexDirection: "column",
                justifyContent: "center",
                height: "100%",
                p: 1,
                minHeight: "360px",
                width: "100%",
            }}
        >
            <Box
                sx={{
                    width: "100%",
                    position: "relative",
                    mx: "auto",
                    height: { lg: "400px" },
                }}
            >
                <CardMedia
                    component="img"
                    height="100%"
                    image={thumbs[idx]?.imageUrl || ""}
                    sx={{
                        backgroundPosition: "center",
                        opacity: isLoading ? 0.4 : 1,
                    }}
                />
                <Box
                    position="absolute"
                    sx={{
                        top: "50%",
                        right: "50%",
                        display: isLoading ? "block" : "none",
                    }}
                >
                    <CircularProgress />
                </Box>
                <Box
                    component={motion.div}
                    initial={{ opacity: 0 }}
                    animate={isHover ? "open" : "closed"}
                    variants={navigate}
                    transition={{ duration: 0.3 }}
                    sx={{
                        position: "absolute",
                        bottom: "50%",
                        display: "flex",
                        justifyContent: "space-between",
                        width: "100%",
                    }}
                >
                    <Box>
                        <Button color="warning" onClick={() => setIdx(idx - 1)} disabled={idx < 1}>
                            <NavigateBeforeIcon fontSize="large" />
                        </Button>
                    </Box>
                    <Box>
                        <Button
                            color="warning"
                            onClick={() => setIdx(idx + 1)}
                            disabled={idx + 1 >= thumbs.length}
                        >
                            <NavigateNextIcon fontSize="large" />
                        </Button>
                    </Box>
                </Box>
                <Box
                    component={motion.div}
                    initial={{ opacity: 0 }}
                    animate={isHover ? "open" : "closed"}
                    variants={variants}
                    transition={{ duration: 0.2 }}
                    sx={{
                        position: "absolute",
                        bottom: 0,
                        left: "50%",
                    }}
                >
                    <Box minWidth="220px" display="flex" justifyContent="space-around">
                        <Box mt="auto">
                            <Button
                                color="warning"
                                variant="contained"
                                onClick={() => handleRemove(thumbs[idx].id)}
                            >
                                Remove
                            </Button>
                        </Box>
                        <ImageFeild
                            label="ADD"
                            control={control}
                            name="thumbnail"
                            handleUpload={handleReview}
                            variant="contained"
                        />
                    </Box>
                </Box>
            </Box>
        </Box>
    )
}
const navigate = {
    open: { opacity: 1 },
    closed: { opacity: 0 },
}
const variants = {
    open: { opacity: 1, y: "-100%", x: "-50%" },
    closed: { opacity: 0, x: "-50%" },
}
export default ImageEditForm
