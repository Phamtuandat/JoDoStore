import { Box, Checkbox, FormControl, FormControlLabel, FormGroup } from "@mui/material"
import RadioButtonUncheckedIcon from "@mui/icons-material/RadioButtonUnchecked"
import RadioButtonCheckedIcon from "@mui/icons-material/RadioButtonChecked"
import { Icon } from "models"
import { useState } from "react"
type Props = {
    filterState: { [key: string]: boolean }
    handleChange: (id: string | number, value: boolean) => void
    icons: Icon[]
}

export default function CheckboxesForm({ filterState, handleChange, icons }: Props) {
    // const {} = useForm({})
    const [checked, setChecked] = useState<{ [key: string]: boolean }>()

    const onChange = (event: React.ChangeEvent<HTMLInputElement>, id: string | number) => {
        setChecked({
            ...checked,
            [id]: !checked ? true : !checked[id],
        })

        handleChange(id, !checked ? true : !checked[id])
    }
    return (
        <Box sx={{ display: "flex" }}>
            <FormControl sx={{ m: 3 }} component="fieldset" variant="standard">
                <FormGroup>
                    {icons.map((icon) => (
                        <FormControlLabel
                            key={icon.id}
                            control={
                                <Checkbox
                                    onChange={(e) => onChange(e, icon.id)}
                                    name={icon.name}
                                    icon={<RadioButtonUncheckedIcon />}
                                    checkedIcon={<RadioButtonCheckedIcon />}
                                />
                            }
                            label={icon.name}
                        />
                    ))}
                </FormGroup>
            </FormControl>
        </Box>
    )
}
