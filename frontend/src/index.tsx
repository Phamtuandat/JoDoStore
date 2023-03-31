import { CssBaseline } from "@mui/material"
import React from "react"
import { createRoot } from "react-dom/client"
import { Provider } from "react-redux"
import { PersistGate } from "redux-persist/integration/react"
import App from "./App"
import { persistor, store } from "./app/store"
import "./styles.css"
const container = document.getElementById("root")!
const root = createRoot(container)

root.render(
    <React.StrictMode>
        <Provider store={store}>
            <PersistGate loading={null} persistor={persistor}>
                <CssBaseline />
                <App />
            </PersistGate>
        </Provider>
    </React.StrictMode>
)

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
