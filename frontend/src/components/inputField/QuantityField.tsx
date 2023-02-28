import AddIcon from "@mui/icons-material/Add"
import RemoveIcon from "@mui/icons-material/Remove"
import { Box, Button, TextField } from "@mui/material"
import { useEffect, useState } from "react"
import { Control, useController } from "react-hook-form"
type Props = {
    handleQuantityChange: (value: number) => void
    control: Control<any>
}

const QuantityField = ({ handleQuantityChange, control }: Props) => {
    const {
        field: { onBlur, onChange, ref, value },
    } = useController({
        control,
        name: "quantity",
    })
    const [quantity, setQuantity] = useState<number>(value)
    useEffect(() => {
        handleQuantityChange(quantity)
        console.log()
    }, [handleQuantityChange, quantity])
    return (
        <Box display="flex" alignItems={"center"} width="100%">
            <Box maxWidth={"30px"}>
                <Button
                    fullWidth
                    color="secondary"
                    variant="outlined"
                    onClick={() => {
                        if (quantity > 0) setQuantity(quantity - 1)
                    }}
                    disabled={quantity === 0}
                    sx={{
                        minWidth: "0",
                        p: 0,
                    }}
                >
                    <RemoveIcon />
                </Button>
            </Box>
            <Box flex={1}>
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
                        "& fieldset": {
                            border: "none",
                        },
                    }}
                />
            </Box>
            <Box maxWidth={"30px"}>
                <Button
                    fullWidth
                    variant="outlined"
                    sx={{
                        minWidth: "0",
                        p: 0,
                    }}
                    color="secondary"
                    onClick={() => setQuantity(quantity + 1)}
                >
                    <AddIcon />
                </Button>
            </Box>
        </Box>
    )
}

export default QuantityField
