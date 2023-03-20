import Table from "@mui/material/Table"
import TableBody from "@mui/material/TableBody"
import TableCell from "@mui/material/TableCell"
import TableContainer from "@mui/material/TableContainer"
import TableHead from "@mui/material/TableHead"
import TableRow from "@mui/material/TableRow"
import Paper from "@mui/material/Paper"
import { Box, Button } from "@mui/material"
import { useEffect, useRef, useState } from "react"
import { addressApi } from "ApiClients/AddressApi"
import { Address } from "models"
import { useAppSelector } from "app/hooks"
import { selectCurrentUser } from "features/authenticate/authSlice"

function createData(
    firstName: string | undefined,
    lastName: string | undefined,
    streetAddress: string,
    state: string,
    city: string | undefined,
    phoneNumber: string
) {
    return { name: firstName + " " + lastName, streetAddress, city, state, phoneNumber }
}

type Props = {}

const AddressBook = (props: Props) => {
    const currentUser = useAppSelector(selectCurrentUser)
    const [address, setAddress] = useState<Address[]>([])
    const ignore = useRef(false)
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
    const rows = address.map((x) => {
        return createData(
            currentUser?.firstName,
            currentUser?.lastName,
            x.streetAddress,
            x.state,
            x.city,
            currentUser?.phoneNumber || ""
        )
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
                        <TableCell align="left">State</TableCell>
                        <TableCell align="left">City</TableCell>
                        <TableCell align="left">Phone Number</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {rows.map((row) => (
                        <TableRow
                            key={row.name}
                            sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
                        >
                            <TableCell component="th" scope="row">
                                {row.name}
                            </TableCell>
                            <TableCell align="left">{row.streetAddress}</TableCell>
                            <TableCell align="left">{row.state}</TableCell>
                            <TableCell align="left">{row.city}</TableCell>
                            <TableCell align="left">{row.phoneNumber}</TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
            <Box textAlign="right">
                <Button variant="contained">+ ADD NEW ADDRESS</Button>
            </Box>
        </TableContainer>
    )
}

export default AddressBook
