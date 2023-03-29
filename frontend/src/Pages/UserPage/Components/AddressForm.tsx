import { Button, Typography } from "@mui/material"
import Box from "@mui/material/Box"
import { InputField } from "components/inputField"
import { Address } from "models"
import { useForm } from "react-hook-form"
import { tableRow } from "./AddressBook"
import { useEffect } from "react"

type Props = {
    onSubmit: (value: Address) => void
    editValue?: tableRow
}

const AddressForm = ({ onSubmit, editValue }: Props) => {
    const { handleSubmit, control, reset } = useForm<Address>({
        defaultValues: {
            id: "",
            name: "",
            phoneNumber: "",
            province: "",
            address: "",
            ward: "",
            district: "",
        },
    })
    useEffect(() => {
        if (editValue) {
            reset(editValue)
        }
    }, [editValue, reset])

    return (
        <Box
            component="form"
            sx={{
                bgcolor: "background.paper",
                display: "flex",
                flexDirection: "column",
                px: { xs: 1, sm: 2, md: 5 },
                py: 10,
                width: { lg: 600, md: 480, xs: 360 },
            }}
            onSubmit={handleSubmit(onSubmit)}
        >
            <Typography variant="h5" mx="auto">
                ADD ADDRESS
            </Typography>
            <InputField control={control} name="name" label="Name" disabled={false} />
            <InputField
                control={control}
                name="phoneNumber"
                label="Phone Number"
                disabled={false}
            />
            <InputField control={control} name="address" label="Address" disabled={false} />
            <InputField control={control} name="ward" label="Ward" disabled={false} />
            <InputField control={control} name="district" label="District" disabled={false} />
            <InputField control={control} name="province" label="Province" disabled={false} />
            <Box mx="auto" mt="20px">
                <Button type="submit" variant="contained">
                    Save
                </Button>
            </Box>
        </Box>
    )
}

export default AddressForm
