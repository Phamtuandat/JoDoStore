import { useEffect, useRef, useState } from "react"
import * as signalR from "@microsoft/signalr"
import { useAppSelector } from "app/hooks"
import { selectCurrentUser } from "features/authenticate/authSlice"
import { Callback } from "yup/lib/types"

export const UseSignalR = (methodName: string = "ReceiveMessage", callback: Callback) => {
    const [connection, setConnection] = useState<signalR.HubConnection | null>(null)
    const isNegotiatingRef = useRef(true)
    const authSelector = useAppSelector(selectCurrentUser)
    const ignore = useRef(false)
    const isLoggedIn =
        localStorage.getItem("persist:root") &&
        JSON.parse(JSON.parse(localStorage.getItem("persist:root") as string)?.auth).isLoggedIn
    useEffect(() => {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl(process.env.SIGNALR_SERVER_URL || "https://localhost:7243/chathub", {
                withCredentials: true,
            })
            .configureLogging(signalR.LogLevel.Information)
            .build()
        setConnection(connection)
        if (!ignore.current) {
            ignore.current = true
            if (isLoggedIn) {
                ;(async () => {
                    try {
                        await connection.start()
                        isNegotiatingRef.current = false
                    } catch (error) {
                        console.log(error)
                    }
                })()
                connection.on(methodName, callback)
            }
        }
        return () => {
            if (!isNegotiatingRef.current) {
                connection.stop()
            }
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])

    return connection
}
