import Box from "@mui/material/Box"
import Button from "@mui/material/Button"
import Step from "@mui/material/Step"
import StepButton from "@mui/material/StepButton"
import Stepper from "@mui/material/Stepper"
import { ReactNode, useEffect, useState } from "react"
const steps = ["1. PRODUCT INFO", "2. MEDIA", "4. PRICING"]

type IProps = {
    children: ReactNode
    handleNextStep: (value: number) => void
    handleFormSubmit: () => void
}

export default function AdminStepper({ children, handleNextStep, handleFormSubmit }: IProps) {
    const [activeStep, setActiveStep] = useState(0)
    useEffect(() => {
        handleNextStep(activeStep)
    }, [activeStep, handleNextStep])

    const totalSteps = () => {
        return steps.length
    }

    const isLastStep = () => {
        return activeStep === totalSteps() - 1
    }

    const handleNext = () => {
        const newActiveStep = !isLastStep() ? activeStep + 1 : activeStep
        setActiveStep(newActiveStep)
    }

    const handleBack = () => {
        setActiveStep((prevActiveStep) => prevActiveStep - 1)
    }

    const handleStep = (step: number) => () => {
        setActiveStep(step)
    }

    return (
        <Box component="form" onSubmit={handleFormSubmit}>
            <Box sx={{ width: "100%" }}>
                <Stepper alternativeLabel activeStep={activeStep}>
                    {steps.map((label, index) => (
                        <Step key={label}>
                            <StepButton onClick={handleStep(index)}>{label}</StepButton>
                        </Step>
                    ))}
                </Stepper>
                <>
                    {children}
                    <Box sx={{ display: "flex", flexDirection: "row", pt: 2 }}>
                        <Button
                            color="inherit"
                            disabled={activeStep === 0}
                            onClick={handleBack}
                            sx={{ mr: 1 }}
                        >
                            Back
                        </Button>
                        <Box sx={{ flex: "1 1 auto" }} />
                        {isLastStep() && (
                            <Button color="inherit" type="submit">
                                Finish
                            </Button>
                        )}
                        {!isLastStep() && (
                            <Button onClick={handleNext} sx={{ mr: 1 }} color="inherit">
                                Next
                            </Button>
                        )}
                    </Box>
                </>
            </Box>
        </Box>
    )
}
