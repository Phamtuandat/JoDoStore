import { userInfor } from "models"
import { useCallback, useEffect, useState } from "react"

let logoutTimer: any
export const useAuth = () => {
    const [token, setToken] = useState<string | null>()
    const [tokenExpirationDate, setTokenExpirationDate] = useState<Date | null>()
    const [currentUser, setCurrentUser] = useState<userInfor | null>()

    const login = useCallback((currentUser: userInfor, token: string, ExpirationDate: Date) => {
        setToken(token)
        setCurrentUser(currentUser)
        const tokenExpirationDate =
            ExpirationDate || new Date(new Date().getTime() + 1000 * 60 * 60)
        setTokenExpirationDate(tokenExpirationDate)
    }, [])
    const logout = useCallback(() => {
        setToken(null)
        setTokenExpirationDate(null)
        setCurrentUser(null)
        localStorage.removeItem("userData")
    }, [])
    useEffect(() => {
        if (token && tokenExpirationDate) {
            const remainingTime = tokenExpirationDate.getTime() - new Date().getTime()
            logoutTimer = setTimeout(logout, remainingTime)
        } else {
            clearTimeout(logoutTimer)
        }
    }, [token, logout, tokenExpirationDate])

    useEffect(() => {
        const current = localStorage.getItem("currentUser")
        if (!!current) {
            const storedData = JSON.parse(current)
            if (storedData && storedData.token && new Date(storedData.expiration) > new Date()) {
                login(storedData.userId, storedData.token, new Date(storedData.expiration))
            }
        }
    }, [login])

    return { token, login, logout, currentUser }
}
