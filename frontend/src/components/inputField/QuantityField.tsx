import AddIcon from "@mui/icons-material/Add"
import RemoveIcon from "@mui/icons-material/Remove"
import { Box, IconButton, TextField } from "@mui/material"
import { useTheme } from "@mui/material/styles"
import { useEffect, useState } from "react"
import { Control, useController } from "react-hook-form"
type Props = {
    handleQuantityChange: (value: number) => void
    control: Control<any>
}

const QuantityField = ({ handleQuantityChange, control }: Props) => {
    const theme = useTheme()
    const {
        field: { onBlur, onChange, ref, value },
    } = useController({
        control,
        name: "quantity",
    })
    const [quantity, setQuantity] = useState<number>(value)
    useEffect(() => {
        handleQuantityChange(quantity)
    }, [handleQuantityChange, quantity])
    return (
        <Box display="flex" alignItems={"center"} width="100%">
            <IconButton
                color="secondary"
                onClick={() => {
                    if (quantity > 0) setQuantity(quantity - 1)
                }}
                disabled={quantity === 0}
                sx={{
                    " &:hover": {
                        border: `solid 2px ${theme.palette.secondary.main}`,
                    },
                    border: `solid 2px transparent`,
                    p: 0,
                }}
            >
                <RemoveIcon />
            </IconButton>
            <Box mx={0.6} flex={1}>
                <TextField
                    ref={ref}
                    variant="outlined"
                    value={value}
                    type="number"
                    size="small"
                    name="quantity"
                    onChange={onChange}
                    onBlur={onBlur}
                    sx={{
                        "& input[type=number]::-webkit-inner-spin-button": {
                            WebkitAppearance: "none",
                            margin: 0,
                        },
                        "& input": {
                            textAlign: "center",
                            p: 0,
                        },
                    }}
                />
            </Box>
            <IconButton
                sx={{
                    " &:hover": {
                        border: `solid 2px ${theme.palette.secondary.main}`,
                    },
                    border: `solid 2px transparent`,
                    p: 0,
                }}
                color="secondary"
                onClick={() => setQuantity(quantity + 1)}
            >
                <AddIcon />
            </IconButton>
        </Box>
    )
}

export default QuantityField
