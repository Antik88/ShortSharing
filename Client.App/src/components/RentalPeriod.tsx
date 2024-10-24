import { Box, Typography } from "@mui/material";
import { DateRange, RangeKeyDict } from 'react-date-range';
import { StyledButton } from "../styled/StyledButton";
import useUserStore from "../store/useUserStore";

interface RentalPeriodProps {
    selectedDates: any;
    disabledDatesValues: Date[];
    ownerId: string;
    setSelectedDates: (dates: any) => void;
    openRentModal: () => void;
}

export default function RentalPeriod({ selectedDates, setSelectedDates, ownerId, openRentModal, disabledDatesValues }: RentalPeriodProps) {
    const user = useUserStore();

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
                disabledDates={disabledDatesValues}
                minDate={new Date()}
            />
            {ownerId === user.user.id ?
                <Typography sx={{ mt: 2 }}>You are owner of this thing</Typography>
                :
                <StyledButton
                    fullWidth
                    onClick={openRentModal}
                    sx={{ mt: 1 }}
                >
                    <Typography>Rent</Typography>
                </StyledButton>
            }
        </Box >
    );
}
