import SearchIcon from "@mui/icons-material/Search"
import { Box, IconButton, InputAdornment } from "@mui/material"
import TextField from "@mui/material/TextField"
import { Control, useController } from "react-hook-form"

type Props = {
    control: Control<any>
    name: string
    label: string
    handleSearchChange: (value: string) => void
    handleBlur?: () => void
}

const SearchField = ({ control, name, label, handleSearchChange }: Props) => {
    const {
        field: { onBlur, onChange, value },
    } = useController({
        control,
        name,
    })
    return (
        <Box>
            <TextField
                margin="normal"
                autoComplete="new-password"
                fullWidth
                value={value}
                size="small"
                onChange={(e) => {
                    handleSearchChange(e.target.value)
                    onChange(e)
                }}
                onBlur={() => {
                    onBlur()
                }}
                type="text"
                InputProps={{
                    endAdornment: (
                        <InputAdornment position="end">
                            <Box
                                sx={{
                                    bgcolor: "background.paper",
                                    mr: "-12px",
                                }}
                            >
                                <IconButton aria-label="search">
                                    <SearchIcon />
                                </IconButton>
                            </Box>
                        </InputAdornment>
                    ),
                }}
            />
        </Box>
    )
}

export default SearchField
