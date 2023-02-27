import { Tab, Tabs } from "@mui/material"
import Box from "@mui/material/Box"
import { SyntheticEvent, useState } from "react"
type Props = {
    handleSortChange: (value: "salePrice desc" | "salePrice asc") => void
}

export default function ProductSort({ handleSortChange }: Props) {
    const [sortParam, setParam] = useState("salePrice desc")

    const handleChange = (event: SyntheticEvent, newValue: "salePrice desc" | "salePrice asc") => {
        handleSortChange(newValue)
        setParam(newValue)
    }

    return (
        <Box
            textAlign="center"
            sx={{
                height: 56,
                py: 2,
            }}
        >
            <Tabs
                value={sortParam}
                onChange={handleChange}
                sx={{
                    fontSize: "16px",
                }}
                textColor="secondary"
                indicatorColor="primary"
            >
                <Tab label="price desc" value="salePrice desc" />
                <Tab label="price asc" value="salePrice asc" />
            </Tabs>
        </Box>
    )
}
