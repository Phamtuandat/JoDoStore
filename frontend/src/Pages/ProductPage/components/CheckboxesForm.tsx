import { Box, Checkbox, FormControl, FormControlLabel, FormGroup } from "@mui/material"
import RadioButtonUncheckedIcon from "@mui/icons-material/RadioButtonUnchecked"
import RadioButtonCheckedIcon from "@mui/icons-material/RadioButtonChecked"
type Props = {
    filterState: { [key: string]: boolean }
    handleChange: (vevent: React.ChangeEvent<HTMLInputElement>) => void
}

export default function CheckboxesForm({ filterState, handleChange }: Props) {
    // const {} = useForm({})

    const onChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        handleChange(event)
    }

    return (
        <Box sx={{ display: "flex" }}>
            <FormControl sx={{ m: 3 }} component="fieldset" variant="standard">
                <FormGroup>
                    {Object.keys(filterState).map((key) => (
                        <FormControlLabel
                            key={key}
                            control={
                                <Checkbox
                                    checked={filterState[key]}
                                    onChange={onChange}
                                    name={key}
                                    icon={<RadioButtonUncheckedIcon />}
                                    checkedIcon={<RadioButtonCheckedIcon />}
                                />
                            }
                            label={key}
                        />
                    ))}
                </FormGroup>
            </FormControl>
        </Box>
    )
}
