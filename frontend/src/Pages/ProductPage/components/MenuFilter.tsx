import { Box, Button, Collapse, Divider, ListItemButton, Slider } from "@mui/material"
import List from "@mui/material/List"
import ListItemText from "@mui/material/ListItemText"
// import { useTheme } from "@mui/material/styles"
import AddIcon from "@mui/icons-material/Add"
import RemoveIcon from "@mui/icons-material/Remove"
import Typography from "@mui/material/Typography"
import { Brand, Category } from "models"
import { useEffect, useRef, useState } from "react"
import { Params } from "../ProductShopPage"
import CheckboxesForm from "./CheckboxesForm"
import categoryApi from "ApiClients/CategoryApi"
import { brandApi } from "ApiClients/BrandApi"

type Props = {
    handleFilterChange: (value: Params) => void
    param: Params
    queryParam: any
}

export default function MenuFilterOption({ handleFilterChange, param, queryParam }: Props) {
    const ignore = useRef(false)
    const [open, setOpen] = useState<{
        category: boolean
        brand: boolean
        price: boolean
    }>({
        category: true,
        brand: true,
        price: true,
    })
    const [brandChecList, setBrandCheck] = useState<{ [key: string]: boolean }>({})
    const [categoryCheckList, setCategoryCheck] = useState<{ [key: string]: boolean }>({})
    useEffect(() => {
        if (!ignore.current) {
            window.scrollTo({ top: 0, left: 0, behavior: "smooth" })
            ignore.current = true
            ;(async () => {
                try {
                    const categoryList = (await categoryApi.getAll()).data
                    const brandList = (await brandApi.getAll()).data
                    const categoryQs =
                        (queryParam.category as { name: { in: string[] } })?.name.in || []
                    const brandQs = (queryParam.brand as { name: { in: string[] } })?.name.in || []
                    setCategoryCheck(mapArrayTopObj(categoryList, categoryQs))
                    setBrandCheck(mapArrayTopObj(brandList, brandQs))
                } catch (error) {}
            })()
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])
    const mapArrayTopObj = (paramList: Array<Category | Brand>, queryParam: string[]) => {
        const keyList = paramList.map((p) => p.name)
        const obj = keyList.reduce((accumulator, value) => {
            return { ...accumulator, [value]: queryParam.includes(value) }
        }, {})
        return obj
    }
    const handleOpen = (option: "category" | "brand" | "price") => {
        setOpen((prv) => {
            return {
                ...prv,
                [option]: !prv[option],
            }
        })
    }

    const handleCategoryChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const categoryCheck = {
            ...categoryCheckList,
            [event.target.name]: event.target.checked,
        }
        setCategoryCheck(categoryCheck)
        const categoryParam = Object.keys(categoryCheck).filter((x) => categoryCheck[x])
        if (categoryParam.length < 1) {
            handleFilterChange({
                ...param,
                category: undefined,
            })
        } else {
            handleFilterChange({
                ...param,
                category: { name: { in: categoryParam } },
            })
        }
    }
    const handlebrandChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const brandCheck = {
            ...brandChecList,
            [event.target.name]: event.target.checked,
        }
        setBrandCheck(brandCheck)
        const brandParams = Object.keys(brandCheck).filter((x) => brandCheck[x])
        if (brandParams.length < 1) {
            handleFilterChange({
                ...param,
                brand: undefined,
            })
        } else {
            handleFilterChange({
                ...param,
                brand: { name: { in: brandParams } },
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
        <>
            <List sx={{ width: "100%", maxWidth: 360 }}>
                <ListItemButton
                    onClick={() => handleOpen("category")}
                    sx={{
                        "&:hover": {
                            bgcolor: "background.paper",
                        },
                    }}
                >
                    <ListItemText
                        primary={
                            <Typography variant="h6" fontWeight={500}>
                                CATEGORIES
                            </Typography>
                        }
                    />
                    {open.category ? <AddIcon /> : <RemoveIcon />}
                </ListItemButton>
                <Divider variant="fullWidth" />
                <Collapse in={open.category} timeout="auto" unmountOnExit>
                    <CheckboxesForm
                        filterState={categoryCheckList}
                        handleChange={handleCategoryChange}
                    />
                </Collapse>
            </List>
            <List sx={{ width: "100%", maxWidth: 360 }}>
                <ListItemButton
                    onClick={() => handleOpen("brand")}
                    sx={{
                        "&:hover": {
                            bgcolor: "background.paper",
                        },
                    }}
                >
                    <ListItemText
                        primary={
                            <Typography variant="h6" fontWeight={500}>
                                BRANDS
                            </Typography>
                        }
                    />
                    {open.brand ? <AddIcon /> : <RemoveIcon />}
                </ListItemButton>
                <Divider variant="fullWidth" />
                <Collapse in={open.brand} timeout="auto" unmountOnExit>
                    <CheckboxesForm filterState={brandChecList} handleChange={handlebrandChange} />
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
        </>
    )
}
function valuetext(value: number) {
    return `${value}°C`
}
