import FormatBoldIcon from "@mui/icons-material/FormatBold"
import FormatItalicIcon from "@mui/icons-material/FormatItalic"
import FormatUnderlinedIcon from "@mui/icons-material/FormatUnderlined"
import { Box, IconButton, Typography } from "@mui/material"
import { useTheme } from "@mui/material/styles"
import { Editor, EditorState, RichUtils } from "draft-js"
import { MouseEvent, useEffect, useState } from "react"
type Props = {
    handleChange: (value: EditorState) => void
}

const TextEditTor = ({ handleChange }: Props) => {
    const theme = useTheme()
    const [editorState, setEditorState] = useState(() => EditorState.createEmpty())

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
                        py: 2,
                        px: 1,
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
