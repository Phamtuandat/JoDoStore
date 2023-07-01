import { UserInfor } from "models"
import { createContext } from "react"

type IAuthContext = {
    isLoggedIn: boolean
    userInfor: null | UserInfor
    token: null | string
    login: (currentUser: UserInfor, token: string, ExpirationDate: Date) => void
    logout: () => void
}
export const AuthContext = createContext<IAuthContext>({
    isLoggedIn: false,
    userInfor: null,
    token: null,
    login: () => {},
    logout: () => {},
})
