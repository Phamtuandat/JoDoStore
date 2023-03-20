import Box from "@mui/material/Box"
import Tab from "@mui/material/Tab"
import Tabs from "@mui/material/Tabs"
import * as React from "react"
import ManagerProfile from "./Components/ManagerProfile"
import AddressBook from "./Components/AddressBook"
import { Hidden } from "@mui/material"

type Props = {}
interface TabPanelProps {
    children?: React.ReactNode
    index: number
    value: number
}

function TabPanel(props: TabPanelProps) {
    const { children, value, index, ...other } = props

    return (
        <div
            role="tabpanel"
            hidden={value !== index}
            id={`simple-tabpanel-${index}`}
            aria-labelledby={`simple-tab-${index}`}
            {...other}
        >
            {value === index && <Box sx={{ p: 3 }}>{children}</Box>}
        </div>
    )
}

function a11yProps(index: number) {
    return {
        id: `simple-tab-${index}`,
        "aria-controls": `simple-tabpanel-${index}`,
    }
}
const AccountPage = (props: Props) => {
    const [value, setValue] = React.useState(0)

    const handleChange = (event: React.SyntheticEvent, newValue: number) => {
        setValue(newValue)
    }

    return (
        <Box sx={{ width: "100%" }}>
            <Hidden mdDown>
                <Box sx={{ borderBottom: 1, borderColor: "divider" }}>
                    <Tabs value={value} onChange={handleChange} aria-label="basic tabs example">
                        <Tab label="My Profile" {...a11yProps(0)} />
                        <Tab label="Address Books" {...a11yProps(1)} />
                        <Tab label="My Payment Options" {...a11yProps(2)} />
                    </Tabs>
                </Box>
            </Hidden>
            <TabPanel value={value} index={0}>
                <ManagerProfile />
            </TabPanel>
            <TabPanel value={value} index={1}>
                <AddressBook />
            </TabPanel>
            <TabPanel value={value} index={2}>
                My Payment Option
            </TabPanel>
        </Box>
    )
}

export default AccountPage
