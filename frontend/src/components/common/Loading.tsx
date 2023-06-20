import { Box } from "@mui/material"
import "./Loading.css"
type Props = {}

const Loading = (props: Props) => {
    return (
        <Box>
            <Box minHeight="100vh" display="flex" justifyContent="center">
                <Box fontSize="30px" id="load">
                    <div>G</div>
                    <div>N</div>
                    <div>I</div>
                    <div>D</div>
                    <div>A</div>
                    <div>O</div>
                    <div>L</div>
                </Box>
            </Box>
        </Box>
    )
}

export default Loading
