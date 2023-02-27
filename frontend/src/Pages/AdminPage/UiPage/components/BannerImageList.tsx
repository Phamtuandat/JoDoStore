import HighlightOffIcon from "@mui/icons-material/HighlightOff"
import { Box, Button, CardMedia, IconButton } from "@mui/material"
import Backdrop from "@mui/material/Backdrop"
import CircularProgress from "@mui/material/CircularProgress"
import ImageList from "@mui/material/ImageList"
import Modal from "@mui/material/Modal"
import Paper from "@mui/material/Paper/Paper"
import Typography from "@mui/material/Typography"
import PhotoApi from "ApiClients/PhotoApi"
import { InputField } from "components/inputField"
import { ImageFeild } from "components/inputField/ImageFeild"
import { Photo, SavePhotoReq } from "models"
import buildQuery from "odata-query"
import { useEffect, useRef, useState } from "react"
import { useForm } from "react-hook-form"
import handleNotify from "utils/Toast-notify"
import ImageItem from "./ImageItem"
type IProps = {
    title: string
}

export default function BannerImageList({ title }: IProps) {
    const [imageUrl, setImage] = useState("")
    const [open, setOpen] = useState(false)
    const { control, handleSubmit, setValue, reset } = useForm<SavePhotoReq>({
        defaultValues: {
            FormFile: null,
            collections: "",
            title: "",
        },
    })
    const [isLoading, setLoading] = useState(false)
    const ignore = useRef(false)
    const [imageList, setImages] = useState<Photo[] | []>([])
    useEffect(() => {
        if (!ignore.current) {
            ignore.current = true
            ;(async () => {
                const param = buildQuery({
                    filter: {
                        imageCollections: {
                            name: "hero",
                        },
                    },
                })
                const list = (await PhotoApi.getAll(param)).data
                setImages(list)
            })()
        }
    }, [])
    const onSubmit = async (value: SavePhotoReq) => {
        if (value.FormFile) {
            setLoading(true)
            const formData = new FormData()
            formData.append("title", value.title)
            formData.append("collections", value.collections)
            formData.append("formFile", value.FormFile)
            try {
                await PhotoApi.create(formData)
                setLoading(false)
                reset()
                const param = buildQuery({
                    filter: {
                        imageCollections: {
                            name: "hero",
                        },
                    },
                })
                const list = (await PhotoApi.getAll(param)).data
                setImages(list)
                console.log(list)
                setOpen(false)
                handleNotify.success("successfully!")
            } catch (error) {}
        }
    }

    const handleReview = (value: FileList) => {
        if (value) {
            for (let i = 0; i < value.length; i++) {
                const file = value[i]
                const url = URL.createObjectURL(file)
                setImage(url)
            }
        }
        setValue("FormFile", value[0])
    }
    const handleRemove = async (id: number) => {
        setLoading(true)
        try {
            await PhotoApi.delete(id)
            handleNotify.success("successfully!")
            const newList = imageList.filter((x) => +x.id !== id)
            setImages(newList)
            setLoading(false)
        } catch (error) {
            handleNotify.error(error as string)
        }
    }
    return (
        <Box>
            <ImageList sx={{ width: "100%" }} cols={3} rowHeight={164} gap={10}>
                {imageList.length > 0 &&
                    imageList.map((item) => (
                        <Box key={item.id}>
                            <ImageItem item={item} handleRemove={handleRemove} />
                        </Box>
                    ))}
            </ImageList>
            <Button color="success" variant="contained" onClick={() => setOpen(true)}>
                ADD
            </Button>
            <Modal open={open} onClose={() => setOpen(false)}>
                <Box
                    component="form"
                    mx="auto"
                    mt={2}
                    onSubmit={handleSubmit(onSubmit)}
                    sx={{
                        width: { md: 680, xs: "95%" },
                    }}
                >
                    <Paper>
                        <Backdrop
                            sx={{ color: "#fff", zIndex: (theme) => theme.zIndex.modal + 1 }}
                            open={isLoading}
                            onClick={() => setLoading(false)}
                        >
                            <CircularProgress color="inherit" />
                        </Backdrop>
                        <Box textAlign="right">
                            <IconButton onClick={() => setOpen(false)}>
                                <HighlightOffIcon color="primary" />
                            </IconButton>
                        </Box>
                        <Box display="flex" flexDirection="column" px={{ md: 6, xs: 1 }} py={1}>
                            <Typography component="span" variant="h4" textAlign="center">
                                PHOTO FORM
                            </Typography>
                            <InputField
                                control={control}
                                name="title"
                                disabled={isLoading}
                                label="Title"
                                fullWidth={false}
                            />
                            <InputField
                                control={control}
                                name="collections"
                                disabled={isLoading}
                                label="Collections"
                                fullWidth={false}
                            />
                            <Box
                                sx={{
                                    width: "300px",
                                    height: "300px",
                                    mx: "auto",
                                    border: "solid 1px",
                                    display: "flex",
                                }}
                            >
                                <CardMedia
                                    component="img"
                                    image={imageUrl}
                                    sx={{ width: "300px", my: "auto" }}
                                />
                            </Box>
                            <ImageFeild
                                label="Choose image"
                                control={control}
                                name="thumbnail"
                                handleUpload={handleReview}
                                variant="outlined"
                                color="secondary"
                            />
                            <Box mx="auto" mt="25px">
                                <Button type="submit" variant="contained">
                                    Upload
                                </Button>
                            </Box>
                        </Box>
                    </Paper>
                </Box>
            </Modal>
        </Box>
    )
}
