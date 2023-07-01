import DOMPurify from "dompurify"
import { convertToHTML } from "draft-convert"
import { convertFromRaw } from "draft-js"

const convert = {
    convertToHtml(value: string | null): string {
        if (value) {
            return DOMPurify.sanitize(convertToHTML(convertFromRaw(JSON.parse(value))))
        }
        return ""
    },
}

export default convert
