import { Typography, Box } from "@mui/material";
import Accordion from '@mui/material/Accordion';
import AccordionSummary from '@mui/material/AccordionSummary';
import AccordionDetails from '@mui/material/AccordionDetails';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';

interface ThingInfoProps {
    name: string;
    description: string;
    price: number;
}

export default function ThingInfo({ name, price, description }: ThingInfoProps) {
    return (
        <Box>
            <Typography
                variant='h4'
                color='primary'
                fontFamily='Fahkwang, sans-serif'
                fontWeight='bold'
                marginRight='20px'
            >
                {name}
            </Typography>
            <Box display='flex'
                alignItems='center'
                flexDirection='row'
                sx={{ mt: 3, mb: 2 }}
            >
                <Typography
                    color="primary.light"
                    fontSize='13px'
                    variant="body1"
                    sx={{ mr: 3 }}
                >
                    Price
                </Typography>
                <Typography
                    variant='h6'
                    color='white'
                    fontFamily='Fahkwang, sans-serif'
                    marginRight='20px'
                >
                    {price} USD
                </Typography>
            </Box>
            <Accordion defaultExpanded
                sx={{
                    borderTop: '1px solid',
                    borderBottom: '1px solid',
                    borderColor: 'white'
                }}
            >
                <AccordionSummary
                    sx={{
                        backgroundColor: '#000',
                    }}
                    expandIcon={<ExpandMoreIcon color="primary" />}
                    aria-controls="panel1-content"
                    id="panel1-header"
                >
                    <Typography color="primary">Description</Typography>
                </AccordionSummary>
                <AccordionDetails sx={{ backgroundColor: "#000" }}>
                    <Typography color="primary.light">
                        {description}
                    </Typography>
                </AccordionDetails>
            </Accordion>
        </Box>

    )

}