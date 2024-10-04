import { Box, IconButton, Modal, Typography, Snackbar, Alert } from "@mui/material";
import { useState } from "react";
import { RentData } from "../types/types";
import CloseIcon from '@mui/icons-material/Close';
import { StyledButton } from "../styled/StyledButton";
import InfoRow from "../components/InfoRow";
import { rentThing } from "../http/rentAPI";

interface RentModalProps {
    open: boolean;
    handleClose: () => void;
    rentData: RentData;
}

export default function RentModal({ open, handleClose, rentData }: RentModalProps) {
    const [snackbarOpen, setSnackbarOpen] = useState(false);

    const handleRentThing = async () => {
        await rentThing(rentData);
        setSnackbarOpen(true);
        handleClose();
    };

    const handleCloseSnackbar = () => {
        setSnackbarOpen(false);
    };

    const dateDiff: number = Math.round((rentData.endRentDate.getTime()
        - rentData.startRentDate.getTime()) / (24 * 60 * 60 * 1000) + 1);

    return (
        <>
            <Modal
                open={open}
                onClose={handleClose}
            >
                <Box
                    sx={{
                        position: 'absolute',
                        top: '50%',
                        left: '50%',
                        transform: 'translate(-50%, -50%)',
                        width: 400,
                        bgcolor: '#2d2d2d',
                        borderRadius: 0,
                        boxShadow: 24,
                        p: 3,
                    }}
                >
                    <Box display="flex" flexDirection="column">
                        <Box display="flex" justifyContent="space-between" alignItems="center">
                            <Typography
                                variant="h6"
                                id="modal-modal-title"
                                fontFamily="Fahkwang, sans-serif"
                                textTransform="uppercase"
                                sx={{
                                    color: 'primary.main',
                                    fontWeight: '600',
                                }}
                            >
                                Rent Data
                            </Typography>
                            <IconButton
                                onClick={handleClose}
                                sx={{ color: 'primary.main' }}
                            >
                                <CloseIcon />
                            </IconButton>
                        </Box>

                        <InfoRow label="Thing" value={rentData.thingName} />
                        <InfoRow label="Start Date" value={rentData.startRentDate.toDateString()} />
                        <InfoRow label="End Date" value={rentData.endRentDate.toDateString()} />
                        <InfoRow label="Days" value={dateDiff} />
                        <InfoRow label="Price" value={`$${dateDiff * rentData.price}`} />

                        <StyledButton onClick={handleRentThing}>
                            <Typography>Confirm</Typography>
                        </StyledButton>
                    </Box>
                </Box>
            </Modal>

            <Snackbar
                open={snackbarOpen}
                autoHideDuration={1000}
                onClose={handleCloseSnackbar}
            >
                <Alert onClose={handleCloseSnackbar}>
                    <Typography>Thing has been rented!</Typography>
                </Alert>
            </Snackbar>
        </>
    );
}
