/* eslint-disable react-hooks/exhaustive-deps */
import FormatBoldIcon from "@mui/icons-material/FormatBold"
import FormatItalicIcon from "@mui/icons-material/FormatItalic"
import FormatUnderlinedIcon from "@mui/icons-material/FormatUnderlined"
import { Box, IconButton, Typography } from "@mui/material"
import { useTheme } from "@mui/material/styles"
import { convertFromRaw, Editor, EditorState, RichUtils } from "draft-js"
import { Product } from "models"
import { MouseEvent, useEffect, useState } from "react"
import { Control, useController } from "react-hook-form"
import { useLoaderData, useParams } from "react-router-dom"
type Props = {
    handleChange: (value: EditorState) => void
    control: Control<any>
    name: string
}

const TextEditTor = ({ handleChange, control, name }: Props) => {
    const theme = useTheme()
    const { productId } = useParams()
    const product = useLoaderData() as Product
    const [editorState, setEditorState] = useState<EditorState>(EditorState.createEmpty())
    const {
        field: { value },
    } = useController({
        name,
        control,
    })
    useEffect(() => {
        if (productId) {
            const state = product.description
                ? EditorState.createWithContent(convertFromRaw(JSON.parse(product.description)))
                : EditorState.createEmpty()
            setEditorState(state)
        }
    }, [productId])
    useEffect(() => {
        if (!productId && !!value) {
            setEditorState(EditorState.createWithContent(convertFromRaw(JSON.parse(value))))
        }
    }, [])

    useEffect(() => {
        handleChange(editorState)
    }, [editorState, handleChange])

    const onHandleRichText = (e: MouseEvent, value: "bold" | "underline" | "italic") => {
        e.preventDefault()
        setEditorState(RichUtils.toggleInlineStyle(editorState, value.toUpperCase()))
    }
    const activeStyle = (value: string): boolean => {
        return editorState.getCurrentInlineStyle().has(value)
    }
    return (
        <Box mt={3}>
            <Typography variant="body1">Description (optional)</Typography>
            <Box
                sx={{
                    borderRadius: 1,
                    border: "solid 1px #ccc",
                    "&>div.DraftEditor-root": {
                        borderTop: " solid 1px",
                        px: 1,
                        height: "150px",
                        overflowY: "scroll",
                    },
                }}
            >
                <Box
                    p={1}
                    display="flex"
                    sx={{
                        "&>*": {
                            mr: 1,
                        },
                    }}
                >
                    <IconButton
                        sx={{
                            fontWeight: "bold",
                            bgcolor: activeStyle("BOLD")
                                ? theme.palette.action.selected
                                : "backgroud.default",
                        }}
                        onMouseDown={(e) => onHandleRichText(e, "bold")}
                        size="small"
                    >
                        <FormatBoldIcon />
                    </IconButton>
                    <IconButton
                        sx={{
                            fontStyle: "italic",
                            bgcolor: activeStyle("ITALIC")
                                ? theme.palette.action.selected
                                : "backgroud.default",
                        }}
                        onMouseDown={(e) => onHandleRichText(e, "italic")}
                        size="small"
                    >
                        <FormatItalicIcon />
                    </IconButton>
                    <IconButton
                        sx={{
                            textDecoration: "underline",
                            bgcolor: activeStyle("UNDERLINE")
                                ? theme.palette.action.selected
                                : "backgroud.default",
                        }}
                        onMouseDown={(e) => onHandleRichText(e, "underline")}
                        size="small"
                    >
                        <FormatUnderlinedIcon />
                    </IconButton>
                </Box>
                <Box component={Editor} editorState={editorState} onChange={setEditorState} />
            </Box>
        </Box>
    )
}

export default TextEditTor
