import { toast, ToastOptions } from "react-toastify"

const handleNotify = {
    success: (message?: string, option?: ToastOptions) =>
        toast.success(message || "Successfully!", option),
    error: (message: string, option?: ToastOptions) => toast.error(message, option),
    warn: (message: string, option?: ToastOptions) => toast.warn(message, option),
    infor: (message: string, option?: ToastOptions) => toast.info(message, option),
}

export default handleNotify
