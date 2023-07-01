import Box from "@mui/material/Box"
import Button from "@mui/material/Button"
import Step from "@mui/material/Step"
import StepLabel from "@mui/material/StepLabel"
import Stepper from "@mui/material/Stepper"
import { ReactNode, useEffect, useState } from "react"

type step = {
    title: string
    fieldList: string[]
}

const steps: step[] = [
    {
        title: "1. PRODUCT INFO",
        fieldList: ["name", "categories", "brand"],
    },
    {
        title: "2. MEDIA",
        fieldList: ["thumbnail"],
    },
    {
        title: "4. PRICING",
        fieldList: ["price", "salePrice"],
    },
]

type IProps = {
    children: ReactNode
    handleNextStep: (value: number) => void
    handleFormSubmit: () => void
    errors: any
    isSubmitted: boolean
    handleResetForm: () => void
}

export default function AdminStepper({
    children,
    handleNextStep,
    handleFormSubmit,
    errors,
    isSubmitted,
    handleResetForm,
}: IProps) {
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
    const onReset = () => {
        if (isSubmitted) {
            handleResetForm()
            setActiveStep(0)
        }
    }
    return (
        <Box component="form" onSubmit={handleFormSubmit}>
            <Box sx={{ width: "100%" }}>
                <Stepper alternativeLabel activeStep={activeStep}>
                    {steps.map((step, index) => (
                        <Step key={index}>
                            <StepLabel
                                onClick={handleStep(index)}
                                error={step.fieldList.some((field) => !!errors[field])}
                                sx={{
                                    cursor: "pointer",
                                }}
                            >
                                {step.title}
                            </StepLabel>
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
                            <Box display="flex" width={220} justifyContent="space-between">
                                {isSubmitted && (
                                    <Button variant="outlined" color="secondary" onClick={onReset}>
                                        Reset Form
                                    </Button>
                                )}
                                <Button
                                    variant="outlined"
                                    color="warning"
                                    type="submit"
                                    sx={{ ml: "auto" }}
                                >
                                    Finish
                                </Button>
                            </Box>
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
