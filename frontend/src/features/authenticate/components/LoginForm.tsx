import LockOutlinedIcon from "@mui/icons-material/LockOutlined"
import { Box, Button, Link } from "@mui/material"
import Avatar from "@mui/material/Avatar"
import Checkbox from "@mui/material/Checkbox"
import FormControlLabel from "@mui/material/FormControlLabel"
import Grid from "@mui/material/Grid"
import Typography from "@mui/material/Typography"
import { InputField, PasswordField } from "components/inputField"
import { loginRequest } from "models"
import { useForm } from "react-hook-form"
import { yupResolver } from "@hookform/resolvers/yup"
import * as Yup from "yup"
type Props = {
    onSubmit: (formValue: loginRequest) => void
    isLoggedIn: boolean
    isLogging: boolean
    handleLogout: () => void
    toggleMode: () => void
}

export const LoginForm = ({ onSubmit, isLoggedIn, isLogging, toggleMode }: Props) => {
    const initialValue: loginRequest = {
        email: "",
        password: "",
    }
    const schema = Yup.object({
        email: Yup.string().required(),
        password: Yup.string().required("No password provided."),
    }).required()

    const { control, handleSubmit } = useForm<loginRequest>({
        defaultValues: initialValue,
        resolver: yupResolver(schema),
    })
    const handleFormSubmit = (formValue: loginRequest) => {
        if (formValue !== null) {
            onSubmit(formValue)
        }
    }

    return (
        <Box
            sx={{
                my: 8,
                mx: 4,
                display: "flex",
                flexDirection: "column",
                alignItems: "center",
            }}
        >
            <Avatar sx={{ m: 1, bgcolor: "secondary.main" }}>
                <LockOutlinedIcon />
            </Avatar>
            <Typography component="h1" variant="h5">
                Sign in
            </Typography>
            <Box
                component="form"
                noValidate
                sx={{ mt: 1 }}
                onSubmit={handleSubmit(handleFormSubmit)}
            >
                <InputField control={control} name="email" label="Email" disabled={isLogging} />
                <PasswordField
                    control={control}
                    name="password"
                    label="Password"
                    disabled={isLogging}
                />
                <FormControlLabel
                    control={<Checkbox value="remember" color="primary" />}
                    label="Remember me"
                />
                <Button type="submit" fullWidth variant="contained" sx={{ mt: 3, mb: 2 }}>
                    Sign In
                </Button>
                <Grid container>
                    <Grid item xs>
                        <Link href="#" variant="body2">
                            Forgot password?
                        </Link>
                    </Grid>
                    <Grid item>
                        <Button variant="text" onClick={() => toggleMode()}>
                            {"Don't have an account? Sign Up"}
                        </Button>
                    </Grid>
                </Grid>
            </Box>
        </Box>
    )
}
