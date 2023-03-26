import EditOutlinedIcon from "@mui/icons-material/EditOutlined"
import { Box, Button, IconButton, Modal } from "@mui/material"
import Paper from "@mui/material/Paper"
import Table from "@mui/material/Table"
import TableBody from "@mui/material/TableBody"
import TableCell from "@mui/material/TableCell"
import TableContainer from "@mui/material/TableContainer"
import TableHead from "@mui/material/TableHead"
import TableRow from "@mui/material/TableRow"
import { addressApi } from "ApiClients/AddressApi"
import { Address } from "models"
import { useEffect, useRef, useState } from "react"
import handleNotify from "utils/Toast-notify"
import AddressForm from "./AddressForm"

export type tableRow = {
    name: string | undefined
    address: string
    province: string
    district: string
    ward: string
    phoneNumber: string
}
function createData({ name, address, province, ward, phoneNumber, district }: tableRow) {
    return { name, address, province, ward, phoneNumber, district }
}

type Props = {}

const AddressBook = (props: Props) => {
    const [mode, setMode] = useState<"create" | "edit">("create")
    const [open, setOpen] = useState(false)
    const handleClose = () => setOpen(false)
    const [editValue, setEditValue] = useState<tableRow>()
    const ignore = useRef(false)
    const [address, setAddress] = useState<Address[]>([])
    useEffect(() => {
        if (!ignore.current) {
            ignore.current = true
            ;(async () => {
                const res = await addressApi.getAll()
                setAddress(res.data)
            })()
        }
        return () => {
            ignore.current = true
        }
    }, [])
    const handleOpen = (mode: "create" | "edit", row?: tableRow) => {
        setOpen(true)
        setMode(mode)
        setEditValue(row)
    }
    const handleSubmit = async (value: Address) => {
        try {
            if (mode === "create") {
                await addressApi.create(value)
                const res = await addressApi.getAll()
                setAddress(res.data)
            } else {
                await addressApi.update(value)
                const res = await addressApi.getAll()
                setAddress(res.data)
            }
            setOpen(false)
        } catch (error) {
            handleNotify.error(error as string)
        }
    }
    const rows = address.map((x) => {
        return createData({
            name: x.name,
            address: x.address,
            province: x.province,
            district: x.district,
            ward: x.ward,
            phoneNumber: x.phoneNumber,
        })
    })
    return (
        <TableContainer
            component={Paper}
            elevation={0}
            sx={{
                p: 3,
            }}
        >
            <Table sx={{ minWidth: 650, mb: 10 }} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell>Full Name</TableCell>
                        <TableCell align="left">Address</TableCell>
                        <TableCell align="left">Ward</TableCell>
                        <TableCell align="left">District</TableCell>
                        <TableCell align="left">Province</TableCell>
                        <TableCell align="left">Phone Number</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {rows.map((row, i) => (
                        <TableRow
                            key={i}
                            sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
                        >
                            <TableCell component="th" scope="row">
                                {row.name}
                            </TableCell>
                            <TableCell align="left">{row.address}</TableCell>
                            <TableCell align="left">{row.ward}</TableCell>
                            <TableCell align="left">{row.district}</TableCell>
                            <TableCell align="left">{row.province}</TableCell>
                            <TableCell align="left">{row.phoneNumber}</TableCell>
                            <TableCell align="left">
                                <Box display="flex" alignItems="center">
                                    <IconButton
                                        size="small"
                                        color="warning"
                                        onClick={() => handleOpen("edit", row)}
                                    >
                                        <EditOutlinedIcon />
                                    </IconButton>
                                </Box>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
            <Box textAlign="right">
                <Button variant="contained" onClick={() => handleOpen("create")}>
                    + ADD NEW ADDRESS
                </Button>
            </Box>
            <Box>
                <Modal open={open} onClose={handleClose}>
                    <Box
                        sx={{
                            position: "absolute" as "absolute",
                            width: "fit-content",
                            top: "50%",
                            left: "50%",
                            transform: "translate(-50%, -50%)",
                        }}
                    >
                        <AddressForm onSubmit={handleSubmit} editValue={editValue} />
                    </Box>
                </Modal>
            </Box>
        </TableContainer>
    )
}

export default AddressBook
