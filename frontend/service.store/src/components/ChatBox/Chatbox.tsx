import * as signalR from "@microsoft/signalr"
import { Box } from "@mui/material"
import { useAppSelector } from "app/hooks"
import { selectCurrentUser } from "features/authenticate/authSlice"
import { useEffect, useRef, useState } from "react"

type Props = {}

const Chatbox = (props: Props) => {
    const isNegotiatingRef = useRef(true)
    const authSelector = useAppSelector(selectCurrentUser)
    const ignore = useRef(false)
    const isLoggedIn =
        localStorage.getItem("persist:root") &&
        JSON.parse(JSON.parse(localStorage.getItem("persist:root") as string)?.auth).isLoggedIn
    useEffect(() => {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:5071/chatHub", {
                withCredentials: true,
            })
            .configureLogging(signalR.LogLevel.Information)
            .build()
        if (!ignore.current) {
            ignore.current = true
            if (isLoggedIn) {
                ;(async () => {
                    try {
                        await connection.start()
                        isNegotiatingRef.current = false
                        connection.invoke("OnConnect", authSelector?.id)
                    } catch (error) {
                        console.log(error)
                    }
                })()
                connection.on("ReceiveMessage", (msg) => console.log(msg))
                console.log("ReceiveMessage")
            }
        }
        return () => {
            if (!isNegotiatingRef.current) {
                connection.stop()
            }
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])

    return <Box p={1}>Chatbox</Box>
}

export default Chatbox
