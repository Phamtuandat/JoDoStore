import ArrowUpwardIcon from "@mui/icons-material/ArrowUpward"
import DeleteForeverOutlinedIcon from "@mui/icons-material/DeleteForeverOutlined"
import { Box, Checkbox, IconButton, Typography } from "@mui/material"
import Pagination from "@mui/material/Pagination"
import Paper from "@mui/material/Paper"
import Table from "@mui/material/Table"
import TableBody from "@mui/material/TableBody"
import TableCell, { TableCellBaseProps } from "@mui/material/TableCell"
import TableContainer from "@mui/material/TableContainer"
import TableHead from "@mui/material/TableHead"
import TableRow from "@mui/material/TableRow"
import { useTheme } from "@mui/material/styles"
import { orderApi } from "ApiClients/OrderApi"
import { Variants, motion } from "framer-motion"
import buildQuery from "odata-query"
import qs from "qs"
import { useEffect, useMemo, useState } from "react"
import { useLocation, useNavigate } from "react-router-dom"

type params = {
    orderBy: string
    filter?: { [key: string]: any }
    top: number
    skip: number
}

type orderDetail = {
    id: string | number
    customer: string
    totalPrice: number | string
    status: string
}
function createData({ id, customer, totalPrice, status }: orderDetail) {
    return { id, customer, totalPrice, status }
}
type TableSortCellProps = {
    orderBy?: (value: "asc" | "desc") => void
    label: string
    component?: React.ElementType<TableCellBaseProps>
}

const variants: Variants = {
    asc: {},
    desc: {
        transform: "rotate(180deg)",
    },
}
const TableSortCell = ({ orderBy, label, component }: TableSortCellProps) => {
    const [state, setState] = useState<"asc" | "desc">("asc")
    return (
        <TableCell align="right" component={component}>
            <Box
                display="flex"
                justifyContent="flex-end"
                sx={{
                    cursor: "pointer",
                    userSelect: "none",
                }}
                onClick={() => {
                    setState(state === "asc" ? "desc" : "asc")
                    if (orderBy) {
                        orderBy(state === "asc" ? "desc" : "asc")
                    }
                }}
            >
                <Typography>{label}</Typography>
                <Box
                    component={motion.div}
                    sx={{
                        alignItems: "center",
                        display: "flex",
                        opacity: 0.8,
                    }}
                    transition={{
                        type: "spring",
                        duration: 0.2,
                    }}
                    variants={variants}
                    animate={state}
                >
                    <ArrowUpwardIcon fontSize="small" />
                </Box>
            </Box>
        </TableCell>
    )
}

const OrderListPage = () => {
    const [loading, setLoading] = useState(false)
    const [orderList, setList] = useState<orderDetail[]>([])
    const [totalPages, setTotal] = useState<number>()

    const location = useLocation()
    const theme = useTheme()
    const navigate = useNavigate()

    const queryParams = useMemo(() => {
        const params = qs.parse(location.search.slice(1))
        return {
            page: params.page || 1,
            orderBy: params.orderBy as string,
        }
    }, [location.search])
    const handlepageChange = (page: number) => {
        navigate({
            search: qs.stringify({ ...queryParams, page }),
        })
    }
    useEffect(() => {
        setLoading(true)
        try {
            ;(async () => {
                const skip = 5 * (+queryParams.page - 1)
                const count = true
                const orderBy = queryParams.orderBy || "orderDate asc"
                const params = buildQuery({ skip, count, orderBy })
                const res = await orderApi.getAll(params)
                setTotal(Math.ceil(+res.headers["x-pagination-total-count"] / 5))
                const result = res.data
                const orderDetailList: orderDetail[] = result.map((x) =>
                    createData({
                        id: x.id || 0,
                        customer: x.userName || " ",
                        totalPrice: x.totalPrice || 0,
                        status: x.status || "Pending",
                    })
                )
                setList(orderDetailList)
                setLoading(false)
            })()
        } catch (error) {}
        return () => {
            setLoading(false)
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [queryParams])

    const statusOption = (status: string) => {
        let color: string
        switch (status.toUpperCase()) {
            case "PENDING":
                color = theme.palette.info.light
                break
            case "PROCESSING":
                color = theme.palette.primary.light
                break
            case "SHIPPED":
                color = theme.palette.secondary.light
                break
            case "CANCELLED":
                color = theme.palette.success.light
                break
            default:
                color = theme.palette.action.disabled
        }

        return color
    }
    return (
        <TableContainer component={Paper} sx={{ mt: 5, px: 1, pt: 2 }}>
            <Typography variant="h6">Orders</Typography>
            <Table sx={{ minWidth: 650 }} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell component="th">
                            <Checkbox />
                        </TableCell>
                        <TableSortCell
                            orderBy={(value) => {
                                navigate({
                                    search: qs.stringify({
                                        ...queryParams,
                                        orderBy: `id ${value}`,
                                    }),
                                })
                            }}
                            label="ORDER"
                        />
                        <TableSortCell
                            orderBy={(value) => {
                                navigate({
                                    search: qs.stringify({
                                        ...queryParams,
                                        orderBy: `userName ${value}`,
                                    }),
                                })
                            }}
                            label="CUSTOMER"
                        />
                        <TableCell align="right">STATUS</TableCell>
                        <TableCell align="right">PAYMENT METHOD</TableCell>
                        <TableSortCell
                            orderBy={(value) => {
                                navigate({
                                    search: qs.stringify({
                                        ...queryParams,
                                        orderBy: `totalPrice ${value}`,
                                    }),
                                })
                            }}
                            label="TOTAL"
                        />

                        <TableCell align="right">ACTION</TableCell>
                    </TableRow>
                </TableHead>
                {!loading && (
                    <TableBody>
                        {orderList.map((row) => (
                            <TableRow
                                key={row.id}
                                sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
                            >
                                <TableCell component="th" scope="row">
                                    <Checkbox />
                                </TableCell>
                                <TableCell align="right">{row.id}</TableCell>
                                <TableCell align="right">{row.customer}</TableCell>
                                <TableCell
                                    align="right"
                                    sx={{
                                        color: statusOption(row.status),
                                    }}
                                >
                                    {row.status}
                                </TableCell>
                                <TableCell align="right">COD</TableCell>
                                <TableCell align="right">{row.totalPrice}</TableCell>
                                <TableCell align="right">
                                    <Box>
                                        <IconButton color="error">
                                            <DeleteForeverOutlinedIcon />
                                        </IconButton>
                                    </Box>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                )}
            </Table>
            <Box py={2} textAlign="right">
                <Pagination
                    page={+queryParams.page}
                    count={totalPages}
                    variant="outlined"
                    color="secondary"
                    onChange={(e, page) => handlepageChange(page)}
                    size="small"
                    sx={{
                        "& > .MuiPagination-ul": {
                            justifyContent: "center",
                        },
                    }}
                />
            </Box>
        </TableContainer>
    )
}
export default OrderListPage
