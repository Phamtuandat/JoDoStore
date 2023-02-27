import { ClickAwayListener } from "@mui/material"
import Box from "@mui/material/Box"
import { useAppDispatch, useAppSelector } from "app/hooks"
import SearchField from "components/inputField/SearchField"
import ProductSearchList from "components/product/ProductSearchItem"
import {
    filterSelector,
    productListSelector,
    ProductSliceActions,
} from "features/ListProduct/listProductSlice"
import { useEffect, useState } from "react"
import { useForm } from "react-hook-form"
import { Outlet, useNavigate } from "react-router-dom"
type Props = {}

type IForm = {
    search: string
}

const EditPage = (props: Props) => {
    const [hidden, setHidden] = useState<boolean>(false)
    const productList = useAppSelector(productListSelector)
    const filter = useAppSelector(filterSelector)
    const dispatch = useAppDispatch()
    const navigate = useNavigate()
    const { handleSubmit, control, reset } = useForm<IForm>({
        defaultValues: {
            search: "",
        },
    })
    useEffect(() => {
        dispatch(ProductSliceActions.getList(filter))
    }, [dispatch, filter])

    const handleClickEdit = (id: number, e: React.MouseEvent<HTMLDivElement, MouseEvent>) => {
        e.preventDefault()
        navigate(`/admin/product/edit/${id}`)
        dispatch(ProductSliceActions.setFilterWithDebounce({ top: 0 }))
        reset()
    }
    const onSubmit = (value: IForm) => {
        console.log(value.search)
    }
    const handleSearchChange = (value: string) => {
        dispatch(
            ProductSliceActions.setFilterWithDebounce({
                filter: { normalizedName: { contains: value.toUpperCase() } },
                top: 3,
            })
        )
        if (value === "") {
            dispatch(ProductSliceActions.setFilter({ top: 0 }))
        }
    }
    const handleClickAway = () => {
        setHidden(true)
    }
    return (
        <Box mt={3} px={{ xs: 0, md: 2 }}>
            <ClickAwayListener onClickAway={handleClickAway}>
                <Box
                    component="form"
                    onSubmit={handleSubmit(onSubmit)}
                    onFocus={() => setHidden(false)}
                    position="relative"
                >
                    <SearchField
                        control={control}
                        label="Search"
                        name="search"
                        handleSearchChange={handleSearchChange}
                    />
                    {!hidden && (
                        <ProductSearchList
                            handleClickEdit={handleClickEdit}
                            products={productList}
                        />
                    )}
                </Box>
            </ClickAwayListener>
            <Outlet />
        </Box>
    )
}

export default EditPage
