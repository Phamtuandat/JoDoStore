import AddIcon from "@mui/icons-material/Add"
import RemoveIcon from "@mui/icons-material/Remove"
import { Box, Button, TextField } from "@mui/material"
import { useState } from "react"
import { Control, useController } from "react-hook-form"
type Props = {
    handleQuantityChange: (value: number) => void
    control: Control<any>
    disabled?: boolean
    setValue?: (value: number) => void
}

const QuantityField = ({ handleQuantityChange, control, disabled, setValue }: Props) => {
    const {
        field: { onBlur, onChange, ref, value },
    } = useController({
        control,
        name: "quantity",
    })
    const [quantity, setQuantity] = useState<number>(value)

    return (
        <Box display="flex" alignItems={"center"} width="100%">
            <Box maxWidth={"30px"}>
                <Button
                    fullWidth
                    color="secondary"
                    variant="outlined"
                    onClick={() => {
                        if (quantity > 0) {
                            setQuantity(quantity - 1)
                            if (setValue) {
                                setValue(quantity - 1)
                            }
                            handleQuantityChange(-1)
                        }
                    }}
                    disabled={quantity === 0 || disabled}
                    sx={{
                        minWidth: "0",
                        p: 0,
                        border: "none",
                        "&:hover": {
                            border: "none",
                            borderRight: "1px solid",
                            borderRadius: "0",
                        },
                        borderRight: "1px solid",
                        borderRadius: "0",
                    }}
                >
                    <RemoveIcon />
                </Button>
            </Box>
            <Box flex={1}>
                <TextField
                    disabled={disabled}
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
                    disabled={disabled}
                    fullWidth
                    variant="outlined"
                    sx={{
                        minWidth: "0",
                        p: 0,
                        border: "none",
                        "&:hover": {
                            borderleft: "1px solid",
                            borderRadius: "0",
                            borderRight: "none",
                            borderBottom: "none",
                            borderTop: "none",
                        },
                        borderLeft: "1px solid",
                        borderRadius: "0",
                    }}
                    color="secondary"
                    onClick={() => {
                        setQuantity(quantity + 1)
                        handleQuantityChange(+1)
                        if (setValue) {
                            setValue(quantity + 1)
                        }
                    }}
                >
                    <AddIcon />
                </Button>
            </Box>
        </Box>
    )
}

export default QuantityField
