import {
    Box,
    Button,
    CircularProgress,
    Divider,
    Grid,
    Paper,
    Stack,
    Typography,
} from "@mui/material"
import authApi from "ApiClients/AuthApi"
import { useAppDispatch, useAppSelector } from "app/hooks"
import { InputField } from "components/inputField"
import SelectTextFields from "components/inputField/SelectField"
import { AuthSliceAction, selectCurrentUser } from "features/authenticate/authSlice"
import { editForm } from "models"
import { useState } from "react"
import DatePicker from "react-datepicker"
import "react-datepicker/dist/react-datepicker.css"
import { useForm } from "react-hook-form"

const ManagerProfile = () => {
    const [editMode, setMode] = useState(false)
    const [loading, setLoading] = useState(false)
    const dispatch = useAppDispatch()
    const currentUser = useAppSelector(selectCurrentUser)
    const [startDate, setStartDate] = useState(new Date(currentUser?.birthday || ""))
    const { handleSubmit, control, setValue } = useForm<editForm>({
        defaultValues: {
            firstName: currentUser?.firstName,
            lastName: currentUser?.lastName,
            birthday: currentUser?.birthday,
            gender: currentUser?.gender,
            phoneNumber: currentUser?.phoneNumber || "",
        },
    })
    const onSubmit = async (value: editForm) => {
        console.log(loading)
        setLoading(true)
        try {
            const edited: editForm = {
                birthday: startDate.toISOString().split("T")[0],
                firstName: value.firstName,
                lastName: value.lastName,
                gender: value.gender,
                phoneNumber: value.phoneNumber,
            }
            const res = await authApi.updateInfo(edited)
            console.log(res.data)
            dispatch(AuthSliceAction.success(res.data))
            setMode(false)
        } catch (error) {
            setMode(false)
        }
        setLoading(false)
    }
    return (
        <Box component="form" onSubmit={handleSubmit(onSubmit)}>
            <Paper elevation={0}>
                <Stack p={{ xs: 2, md: 5 }} minHeight={"60vh"}>
                    {loading ? (
                        <Box display="flex" justifyContent="center" my="auto">
                            <CircularProgress />
                        </Box>
                    ) : (
                        <>
                            <Grid mb={7} container spacing={2}>
                                <Grid
                                    item
                                    sx={{
                                        display: "-webkit-flex",
                                        flexDirection: "column",
                                        textOverflow: "ellipsis",
                                        overflow: "hidden",
                                    }}
                                    xl={4}
                                    md={4}
                                >
                                    {editMode ? (
                                        <Box display="flex">
                                            <Box>
                                                <InputField
                                                    control={control}
                                                    disabled={loading}
                                                    label="First Name"
                                                    name="firstName"
                                                    variant="standard"
                                                    autoComplete="new-password"
                                                />
                                            </Box>
                                            <Divider
                                                orientation="vertical"
                                                flexItem
                                                sx={{ mx: 2 }}
                                            />
                                            <Box>
                                                <InputField
                                                    control={control}
                                                    disabled={loading}
                                                    label="Last Name"
                                                    name="lastName"
                                                    variant="standard"
                                                    autoComplete="new-password"
                                                />
                                            </Box>
                                        </Box>
                                    ) : (
                                        <>
                                            <Typography
                                                sx={{
                                                    opacity: 0.8,
                                                }}
                                                variant="caption"
                                            >
                                                Full Name
                                            </Typography>
                                            <Typography mt={2}>
                                                {currentUser?.firstName} {currentUser?.lastName}
                                            </Typography>
                                        </>
                                    )}
                                </Grid>
                                <Grid
                                    item
                                    sx={{
                                        display: "-webkit-flex",
                                        flexDirection: "column",
                                        textOverflow: "ellipsis",
                                        overflow: "hidden",
                                    }}
                                    xl={4}
                                    md={4}
                                >
                                    <Typography
                                        sx={{
                                            opacity: 0.8,
                                        }}
                                        variant="caption"
                                    >
                                        Email Address
                                    </Typography>
                                    <Typography mt={2}>{currentUser?.email}</Typography>
                                </Grid>
                                <Grid
                                    item
                                    sx={{
                                        display: "-webkit-flex",
                                        flexDirection: "column",
                                        textOverflow: "ellipsis",
                                        overflow: "hidden",
                                    }}
                                    xl={4}
                                    md={4}
                                >
                                    {editMode ? (
                                        <InputField
                                            control={control}
                                            disabled={loading}
                                            label="Mobile"
                                            name="phoneNumber"
                                            variant="standard"
                                            autoComplete="new-password"
                                        />
                                    ) : (
                                        <>
                                            <Typography
                                                sx={{
                                                    opacity: 0.8,
                                                }}
                                                variant="caption"
                                            >
                                                Mobile
                                            </Typography>
                                            <Typography mt={2}>
                                                {currentUser?.phoneNumber}
                                            </Typography>
                                        </>
                                    )}
                                </Grid>
                            </Grid>
                            <Grid container textAlign={"left"} spacing={2}>
                                <Grid
                                    item
                                    sx={{
                                        display: "flex",
                                        flexDirection: "column",
                                    }}
                                    xl={4}
                                    md={4}
                                >
                                    <Typography
                                        sx={{
                                            opacity: 0.8,
                                        }}
                                        variant="caption"
                                    >
                                        Birthday
                                    </Typography>
                                    {editMode ? (
                                        <DatePicker
                                            disabled={loading}
                                            selected={startDate}
                                            onChange={(date: Date) => setStartDate(date)}
                                        />
                                    ) : (
                                        <Typography mt={2}>{currentUser?.birthday}</Typography>
                                    )}
                                </Grid>
                                <Grid
                                    item
                                    sx={{
                                        display: "flex",
                                        flexDirection: "column",
                                    }}
                                    xl={4}
                                    md={4}
                                >
                                    <Typography
                                        sx={{
                                            opacity: 0.8,
                                        }}
                                        variant="caption"
                                    >
                                        Gender
                                    </Typography>
                                    {editMode ? (
                                        <SelectTextFields
                                            control={control}
                                            disabled={loading}
                                            name="gender"
                                            isMutiple={false}
                                            options={[
                                                { id: 1, name: "Male" },
                                                { id: 1, name: "Female" },
                                                { id: 1, name: "Order" },
                                            ]}
                                            setValue={(value) => setValue("gender", value)}
                                        />
                                    ) : (
                                        <Typography mt={2}>{currentUser?.gender}</Typography>
                                    )}
                                </Grid>
                                <Grid
                                    item
                                    sx={{
                                        display: "flex",
                                        flexDirection: "column",
                                    }}
                                    xl={4}
                                    md={4}
                                >
                                    <Typography
                                        sx={{
                                            opacity: 0.8,
                                        }}
                                        variant="caption"
                                    >
                                        Tax code
                                    </Typography>
                                    <Typography mt={2}>{currentUser?.phoneNumber}</Typography>
                                </Grid>
                            </Grid>
                            <Box width={{ xs: "100%", sm: 250 }} mt="50px">
                                <Box mb={2}>
                                    {!editMode ? (
                                        <Button
                                            variant="contained"
                                            fullWidth
                                            onClick={() => setMode(true)}
                                            key={1}
                                        >
                                            Edit Profile
                                        </Button>
                                    ) : (
                                        <Button key={2} variant="contained" fullWidth type="submit">
                                            Save changes
                                        </Button>
                                    )}
                                </Box>
                                <Box>
                                    <Button variant="contained" fullWidth>
                                        Change Password
                                    </Button>
                                </Box>
                            </Box>
                        </>
                    )}
                </Stack>
            </Paper>
        </Box>
    )
}

export default ManagerProfile
