import FmdGoodOutlinedIcon from "@mui/icons-material/FmdGoodOutlined"
import {
    Box,
    Button,
    ButtonBase,
    CircularProgress,
    Collapse,
    Divider,
    FormControl,
    List,
    ListItem,
    Paper,
    Radio,
    RadioGroup,
    Stack,
    TextField,
    Typography,
} from "@mui/material"
import { useTheme } from "@mui/material/styles"
import { CartItemReview } from "features/cart/components/MiniCart"
import { Address } from "models"
import { ChangeEvent, useState } from "react"
import { Link, Navigate, useNavigate } from "react-router-dom"
type Props = {
    handleCreateOrder: () => void
    addressList: Address[]
    carts: CartItemReview[]
    Subtotal: number
    loading: boolean
    handleAddresChange: (value: number) => void
    address: Address | undefined
}

const OrderInfo = ({
    handleCreateOrder,
    Subtotal,
    addressList = [],
    carts,
    loading,
    handleAddresChange,
    address,
}: Props) => {
    const theme = useTheme()
    const [open, setOpen] = useState(false)
    const navigate = useNavigate()
    function onChange(event: ChangeEvent<HTMLInputElement>, value: string): void {
        handleAddresChange(+value)
    }

    return (
        <div>
            <Stack>
                <Paper elevation={0} square>
                    <Box p={2}>
                        <Stack direction="row" justifyContent="space-between">
                            <Typography color="text.secondary">Location</Typography>
                            <ButtonBase
                                sx={{
                                    color: theme.palette.primary.main,
                                }}
                                onClick={() => setOpen(!open)}
                            >
                                {!open ? "Change" : "Done"}
                            </ButtonBase>
                        </Stack>
                        {address ? (
                            <Box display="flex" py={2}>
                                <FmdGoodOutlinedIcon
                                    sx={{
                                        opacity: 0.6,
                                        mr: 1,
                                    }}
                                />
                                <Box flexGrow={1}>
                                    <Box display="flex" justifyContent="space-between" width="100%">
                                        <Typography>{address.name}</Typography>
                                        <Typography>{address.phoneNumber}</Typography>
                                    </Box>
                                    <Typography component="span">{address.address}, </Typography>
                                    <Typography component="span">{address.ward}, </Typography>
                                    <Typography component="span">{address.district}, </Typography>
                                    <Typography component="span">{address.province}</Typography>
                                </Box>
                            </Box>
                        ) : (
                            <Link to={{ pathname: "/user/account/address" }}>
                                Go to Address Page
                            </Link>
                        )}
                        {addressList.length > 0 && (
                            <FormControl>
                                <RadioGroup onChange={onChange}>
                                    {addressList.length > 0 ? (
                                        <Collapse in={open} timeout="auto" unmountOnExit>
                                            <List
                                                component="div"
                                                disablePadding
                                                sx={{
                                                    borderTop: "1px solid #ccc",
                                                }}
                                            >
                                                {addressList.map((add) => (
                                                    <ListItem key={add.id} disableGutters>
                                                        <Box display="flex" alignItems="center">
                                                            <Radio value={add.id} />
                                                            <Box>
                                                                <Box>
                                                                    <Typography component="span">
                                                                        {`${
                                                                            add.name
                                                                                ? add.name + ", "
                                                                                : ""
                                                                        }
                                                                `}
                                                                    </Typography>
                                                                    <Typography component="span">
                                                                        {`${
                                                                            add.phoneNumber
                                                                                ? add.phoneNumber +
                                                                                  ", "
                                                                                : ""
                                                                        }
                                                                `}
                                                                    </Typography>
                                                                </Box>
                                                                <Typography component="span">
                                                                    {`${
                                                                        add.address
                                                                            ? add.address + ", "
                                                                            : ""
                                                                    }
                                                                `}
                                                                </Typography>
                                                                <Typography component="span">
                                                                    {`${
                                                                        add.ward
                                                                            ? add.ward + ", "
                                                                            : ""
                                                                    }`}
                                                                </Typography>
                                                                <Typography component="span">
                                                                    {`${
                                                                        add.district
                                                                            ? add.district + ", "
                                                                            : ""
                                                                    }`}
                                                                </Typography>
                                                                <Typography component="span">
                                                                    {`${add.province}`}
                                                                </Typography>
                                                            </Box>
                                                        </Box>
                                                    </ListItem>
                                                ))}
                                            </List>
                                        </Collapse>
                                    ) : (
                                        <Box>
                                            <Link to="">Add a new addressboox</Link>
                                        </Box>
                                    )}
                                </RadioGroup>
                            </FormControl>
                        )}
                    </Box>
                </Paper>
                <Divider />
                <Paper elevation={0} square>
                    <Stack
                        sx={{
                            p: 2,
                        }}
                        spacing={2}
                    >
                        <Typography variant="h6">Order Summary</Typography>
                        <Box
                            display="flex"
                            justifyContent="space-between"
                            sx={{
                                opacity: 0.8,
                            }}
                        >
                            <Typography component="span">
                                Subtotal {`(${carts.length} items)`}
                            </Typography>
                            <Typography component="span">$ {Subtotal}</Typography>
                        </Box>
                        <Box
                            display="flex"
                            justifyContent="space-between"
                            sx={{
                                opacity: 0.8,
                            }}
                        >
                            <Typography component="span">Shipping Fee</Typography>
                            <Box display="flex">
                                <Typography
                                    component="span"
                                    mr={1}
                                    sx={{
                                        textDecoration: "line-through #0000006e",
                                    }}
                                >
                                    - $ 3
                                </Typography>
                                <Typography>Free</Typography>
                            </Box>
                        </Box>
                        <Box
                            display="flex"
                            justifyContent="center"
                            sx={{
                                height: "40px",
                                my: 3,
                            }}
                        >
                            <TextField
                                label="Enter Voucher Code"
                                variant="outlined"
                                autoComplete="new-password"
                                sx={{
                                    height: "100%",
                                    "& input": {
                                        py: 1,
                                    },
                                }}
                            />
                            <Box mx={1}>
                                <Button
                                    fullWidth
                                    variant="contained"
                                    sx={{
                                        height: "100%",
                                    }}
                                    color="secondary"
                                >
                                    Apply
                                </Button>
                            </Box>
                        </Box>
                        <Box textAlign="right">
                            <Box display="flex" justifyContent="space-between">
                                <Typography>Total</Typography>
                                <Typography color="primary">$ {Subtotal}</Typography>
                            </Box>
                            <Typography variant="caption">
                                VAT included, where applicable
                            </Typography>
                        </Box>
                        <Box>
                            <Button
                                fullWidth
                                variant="contained"
                                onClick={handleCreateOrder}
                                disabled={loading}
                                disableElevation
                                startIcon={
                                    loading && <CircularProgress color="secondary" size={20} />
                                }
                            >
                                CONFIRM CART{`(${carts.length})`}
                            </Button>
                        </Box>
                    </Stack>
                </Paper>
            </Stack>
        </div>
    )
}

export default OrderInfo
