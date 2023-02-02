import DeleteOutlineOutlinedIcon from "@mui/icons-material/DeleteOutlineOutlined"
import EditOutlinedIcon from "@mui/icons-material/EditOutlined"
import { Box } from "@mui/material"
import Avatar from "@mui/material/Avatar"
import Divider from "@mui/material/Divider"
import IconButton from "@mui/material/IconButton"
import List from "@mui/material/List"
import ListItem from "@mui/material/ListItem"
import ListItemAvatar from "@mui/material/ListItemAvatar"
import ListItemText from "@mui/material/ListItemText"
import { useTheme } from "@mui/material/styles"
import Tooltip from "@mui/material/Tooltip"
import Typography from "@mui/material/Typography"
import { Product } from "models"
type Props = {
    products?: Product[]
    handleClickEdit: (id: number, e: React.MouseEvent<HTMLDivElement, MouseEvent>) => void
}

const ProductSearchList = ({ products, handleClickEdit }: Props) => {
    const theme = useTheme()
    return (
        <List
            sx={{
                width: "100%",
                bgcolor: "background.default",
                p: 0,
                boxShadow: theme.shadows[1],
                position: "absolute",
                zIndex: 90,
            }}
        >
            {products?.map((product) => (
                <Box key={product.id}>
                    <ListItem
                        disablePadding
                        sx={{
                            bgcolor: "background.paper",
                            px: 2,
                        }}
                    >
                        <ListItemAvatar>
                            <Avatar alt={product.name} src={product.thumbnails[0].imageUrl} />
                        </ListItemAvatar>
                        <ListItemText
                            primary={product.name}
                            secondary={
                                <Typography
                                    sx={{ display: "inline" }}
                                    component="span"
                                    variant="body2"
                                    color="text.secondary"
                                >
                                    {product.brand?.name || ""}
                                </Typography>
                            }
                        />
                        <Box
                            sx={{
                                display: "flex",
                                justifyContent: "space-evenly",
                                width: "100px",
                            }}
                        >
                            <Tooltip title="Delete">
                                <IconButton color="error">
                                    <DeleteOutlineOutlinedIcon />
                                </IconButton>
                            </Tooltip>
                            <Tooltip
                                title="Edit"
                                onClick={(e) => {
                                    if (product.id) {
                                        handleClickEdit(+product.id, e)
                                    }
                                }}
                            >
                                <IconButton color="secondary">
                                    <EditOutlinedIcon />
                                </IconButton>
                            </Tooltip>
                        </Box>
                    </ListItem>
                    <Divider component="li" />
                </Box>
            ))}
        </List>
    )
}

export default ProductSearchList
