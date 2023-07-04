import { useAppDispatch } from "app/hooks"
import { AuthSliceAction } from "features/authenticate/authSlice"
import { UserManager } from "oidc-client"
import { useEffect } from "react"
import { useNavigate } from "react-router-dom"
import oidcConfig from "utils/OidcConfig"
import Loading from "./common/Loading"
type Props = {}

const userManager = new UserManager(oidcConfig)
const Redirect = (props: Props) => {
    const dispatch = useAppDispatch()
    const navigate = useNavigate()
    useEffect(() => {
        ;(async () => {
            try {
                const user = await userManager.signinRedirectCallback(window.location.href)
                dispatch(
                    AuthSliceAction.success({
                        access_Token: user.access_token,
                        email: user.profile.email || user.profile.name || "",
                        firstName: user.profile.family_name || "",
                        lastName: user.profile.given_name || "",
                        id: user.profile.id,
                        emailConfirmed: true,
                        address: null,
                    })
                )
                navigate("/")
            } catch (error) {
                navigate("/")
            }
        })()
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])
    return (
        <div>
            <Loading />
        </div>
    )
}

export default Redirect
