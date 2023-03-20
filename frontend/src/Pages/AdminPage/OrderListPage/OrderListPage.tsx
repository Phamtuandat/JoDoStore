import * as React from "react"
import Table from "@mui/material/Table"
import TableBody from "@mui/material/TableBody"
import TableCell from "@mui/material/TableCell"
import TableContainer from "@mui/material/TableContainer"
import TableHead from "@mui/material/TableHead"
import TableRow from "@mui/material/TableRow"
import Paper from "@mui/material/Paper"

function createData(
    id: string | number,
    subtotalPrice: number | string,
    totalPrice: number | string,
    address: string,
    status: string,
    shippingCash: string | number
) {
    return { id, subtotalPrice, totalPrice, status, shippingCash, address }
}

const rows = [createData(1, 159, 6.0, "long thanh", "pending", 0)]

const OrderListPage = () => {
    return (
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 650 }} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell component="th">ID</TableCell>
                        <TableCell align="right">Subtotal Price</TableCell>
                        <TableCell align="right">Shipping Cash</TableCell>
                        <TableCell align="right">Total Price</TableCell>
                        <TableCell align="right">Address</TableCell>
                        <TableCell align="right">Status</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {rows.map((row) => (
                        <TableRow
                            key={row.id}
                            sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
                        >
                            <TableCell component="th" scope="row">
                                {row.id}
                            </TableCell>
                            <TableCell align="right">{row.subtotalPrice}</TableCell>
                            <TableCell align="right">{row.shippingCash}</TableCell>
                            <TableCell align="right">{row.subtotalPrice}</TableCell>
                            <TableCell align="right">{row.totalPrice}</TableCell>
                            <TableCell align="right">{row.status}</TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    )
}

export default OrderListPage
