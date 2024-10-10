import { Box, Typography } from "@mui/material";

interface InfoRowProps {
    label: string;
    value: string | number;
}

export default function InfoRow({ label, value }: InfoRowProps) {
    return (
        <Box display="flex" justifyContent="space-between" sx={{ mt: 2, borderBottom: '1px solid', pb: 1 }}>
            <Typography color="primary.light">
                {label}
            </Typography>
            <Typography color="primary.light">
                {value}
            </Typography>
        </Box>
    );
}