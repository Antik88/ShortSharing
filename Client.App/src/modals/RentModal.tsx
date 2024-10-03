import { Box, IconButton, Modal, Typography } from "@mui/material";
import { RentData } from "../types/types";
import CloseIcon from '@mui/icons-material/Close';
import { StyledButton } from "../styled/StyledButton";
import InfoRow from "../components/InfoRow";

interface RentModalProps {
    open: boolean;
    handleClose: () => void;
    rentData: RentData;
}

export default function RentModal({ open, handleClose, rentData }: RentModalProps) {
    const dateDiff: number = Math.round((rentData.endRentDate.getTime()
        - rentData.startRentDate.getTime()) / (24 * 60 * 60 * 1000) + 1);

    return (
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
                    <InfoRow label="Start Date" value={new Date(rentData.startRentDate).toDateString()} />
                    <InfoRow label="End Date" value={new Date(rentData.endRentDate).toDateString()} />
                    <InfoRow label="Days" value={dateDiff} />
                    <InfoRow label="Price" value={`$${dateDiff * rentData.price}`} />

                    <StyledButton>
                        <Typography>Confirm</Typography>
                    </StyledButton>
                </Box>
            </Box>
        </Modal>
    );
}
