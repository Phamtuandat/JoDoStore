import { Box, Button, Collapse, Divider, ListItemButton, Slider } from "@mui/material"
import List from "@mui/material/List"
import ListItemText from "@mui/material/ListItemText"
// import { useTheme } from "@mui/material/styles"
import AddIcon from "@mui/icons-material/Add"
import RemoveIcon from "@mui/icons-material/Remove"
import Typography from "@mui/material/Typography"
import { Icon, Category, ListParams } from "models"
import { useEffect, useRef, useState } from "react"
import CheckboxesForm from "./CheckboxesForm"
import categoryApi from "ApiClients/CategoryApi"
import { brandApi as iconApi } from "ApiClients/IconApi"
import Categories from "./Categories"

type Props = {
    handleFilterChange: (value: ListParams) => void
    param: ListParams
    queryParam: any
}

export default function MenuFilterOption({ handleFilterChange, param, queryParam }: Props) {
    const ignore = useRef(false)
    const [icons, setIcon] = useState<Icon[]>([])
    const [categories, setCategories] = useState<Category[]>([])
    const [open, setOpen] = useState<{
        category: boolean
        icon: boolean
        price: boolean
    }>({
        category: true,
        icon: true,
        price: true,
    })
    const [IconCheckList, setIconCheck] = useState<{ [key: string]: boolean }>({})
    const [categoryCheckList, setCategoryCheck] = useState<{ [key: string]: boolean }>({})
    useEffect(() => {
        if (!ignore.current) {
            window.scrollTo({ top: 0, left: 0, behavior: "smooth" })
            ignore.current = true
            ;(async () => {
                try {
                    const categoryList = (await categoryApi.getAll()).data
                    const icons = (await iconApi.getAll()).data
                    setIcon(icons)
                    setCategories(categoryList)
                    const categoryQs =
                        (queryParam.category as { id: { in: string[] } })?.id.in || []
                    const iconqs = (queryParam.icon as { id: { in: string[] } })?.id.in || []
                    setCategoryCheck(mapArrayTopObj(categoryList, categoryQs))
                    setIconCheck(mapArrayTopObj(icons, iconqs))
                } catch (error) {}
            })()
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])
    const mapArrayTopObj = (paramList: Array<Category | Icon>, queryParam: string[]) => {
        const keyList = paramList.map((p) => p.id)
        const obj = keyList.reduce((accumulator, value) => {
            return { ...accumulator, [value]: queryParam.includes(value.toString()) }
        }, {})
        return obj
    }
    const handleOpen = (option: "category" | "icon" | "price") => {
        setOpen((prv) => {
            return {
                ...prv,
                [option]: !prv[option],
            }
        })
    }

    const handleCategoryChange = (id: string | number, value: boolean) => {
        const categoryCheck = {
            ...categoryCheckList,
            [id]: value,
        }
        setCategoryCheck(categoryCheck)
        const categoryParam = Object.keys(categoryCheck)
            .filter((x) => categoryCheck[x])
            .map(Number)
        if (categoryParam.length < 1) {
            handleFilterChange({
                ...param,
                categoryIds: undefined,
            })
        } else {
            handleFilterChange({
                ...param,
                categoryIds: categoryParam,
            })
        }
    }
    const handlerIconChange = (id: string | number, value: boolean) => {
        const iconCheck = {
            ...IconCheckList,
            [id]: value,
        }
        setIconCheck(iconCheck)
        const iconParams = Object.keys(iconCheck)
            .filter((x) => iconCheck[x])
            ?.map(Number)
        if (iconParams.length < 1) {
            handleFilterChange({
                ...param,
                iconId: undefined,
            })
        } else {
            handleFilterChange({
                ...param,
                iconId: iconParams,
            })
        }
    }

    const handleChangePriceRage = (event: Event, newValue: number | number[]) => {
        const value = newValue as number[]
        handleFilterChange({
            ...param,
            gt: value[0] || 0,
            lt: value[1],
        })
    }
    return (
        <Box
            sx={{
                overflow: "hidden",
            }}
        >
            <Categories handleCategoryChange={handleCategoryChange} />
            <List sx={{ width: "100%", maxWidth: 360 }}>
                <ListItemButton
                    onClick={() => handleOpen("icon")}
                    sx={{
                        "&:hover": {
                            bgcolor: "background.paper",
                        },
                    }}
                >
                    <ListItemText
                        primary={
                            <Typography variant="h6" fontWeight={500}>
                                ICONS
                            </Typography>
                        }
                    />
                    {open.icon ? <AddIcon /> : <RemoveIcon />}
                </ListItemButton>
                <Divider variant="fullWidth" />
                <Collapse in={open.icon} timeout="auto" unmountOnExit>
                    <CheckboxesForm
                        icons={icons}
                        filterState={IconCheckList}
                        handleChange={handlerIconChange}
                    />
                </Collapse>
            </List>
            <List sx={{ width: "100%", maxWidth: 360 }}>
                <ListItemButton
                    onClick={() => handleOpen("price")}
                    sx={{
                        "&:hover": {
                            bgcolor: "background.paper",
                        },
                    }}
                >
                    <ListItemText
                        primary={
                            <Typography variant="h6" fontWeight={500}>
                                PRICE
                            </Typography>
                        }
                    />
                    {open.price ? <AddIcon /> : <RemoveIcon />}
                </ListItemButton>
                <Divider />
                <Collapse in={open.price} timeout="auto" unmountOnExit>
                    <Box p={2} mt={2}>
                        <Slider
                            marks
                            step={100}
                            getAriaLabel={() => "Temperature range"}
                            value={[param.gt, param.lt]}
                            onChange={handleChangePriceRage}
                            valueLabelDisplay="auto"
                            getAriaValueText={valuetext}
                            max={3000}
                            min={0}
                        />
                        <Box display="flex" width="200px" px={1} alignContent="center">
                            <Typography
                                mr={2}
                                component="span"
                                fontWeight={500}
                                alignSelf="flex-start"
                            >
                                Price:
                            </Typography>
                            <Typography mr={2} component="span" fontWeight={500} fontSize={"18px"}>
                                ${param.gt}
                            </Typography>
                            <RemoveIcon />
                            <Typography ml={2} component="span" fontWeight={500} fontSize={"18px"}>
                                ${param.lt}
                            </Typography>
                        </Box>
                    </Box>
                </Collapse>
            </List>
            <Box textAlign="center">
                <Button variant="contained" fullWidth>
                    ALL Reset
                </Button>
            </Box>
        </Box>
    )
}
function valuetext(value: number) {
    return `${value}°C`
}
