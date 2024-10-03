import { Box, Typography } from "@mui/material";
import { DateRange, RangeKeyDict } from 'react-date-range';
import { StyledButton } from "../styled/StyledButton";

interface RentalPeriodProps {
    selectedDates: any;
    setSelectedDates: (dates: any) => void;
    openRentModal: () => void;
}

export default function RentalPeriod({ selectedDates, setSelectedDates, openRentModal }: RentalPeriodProps) {
    const handleDateChange = (ranges: RangeKeyDict) => {
        const { selection } = ranges;
        if (selection) {
            setSelectedDates(selection);
        }
    };

    return (
        <Box sx={{ mt: 2 }}>
            <Typography
                color="primary.light"
                fontSize='13px'
                variant="body1"
                sx={{ mb: 2 }}
            >
                The Rental Period
            </Typography>
            <DateRange
                rangeColors={['#ffca8b']}
                ranges={[selectedDates]}
                onChange={handleDateChange}
                disabledDates={[new Date()]}
                minDate={new Date()}
            />
            <StyledButton
                fullWidth
                onClick={openRentModal}
                sx={{ mt: 1 }}
            >
                <Typography>
                    Rent
                </Typography>
            </StyledButton>
        </Box>
    );
}
