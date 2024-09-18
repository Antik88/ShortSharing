import SearchIcon from '@mui/icons-material/Search';
import EastIcon from '@mui/icons-material/East';
import { ABOUT_ROUTE } from "../utils/consts";
import BackgroundImage from "../assets/bg.jpg";
import { useNavigate } from "react-router-dom";
import { Box, Button, Fade, TextField, Typography } from "@mui/material";
import HowToUse from '../components/HowToUse';

export default function MainPage() {
  const navigate = useNavigate();

  return (
    <Box
      sx={{
        backgroundImage: `url(${BackgroundImage})`,
        backgroundSize: 'cover',
        backgroundPosition: 'center',
        backgroundRepeat: 'no-repeat',
        minHeight: '100vh',
      }}
    >
      <Box sx={{
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'flex-end'
      }}>
        <SearchIcon sx={{
          color: 'primary.main',
          mr: 1, my: 0.5
        }} />
        <TextField
          label="Search"
          variant="standard"
          color="primary"
          autoComplete="off"
          InputLabelProps={{
            sx: {
              color: 'primary.light'
            }
          }}
          InputProps={{
            sx: {
              color: 'primary.main',
              borderBottomColor: 'primary.main',
              '&:before': {
                borderBottomColor: 'primary.light',
              },
            }
          }}
        />
      </Box>
      <Box sx={{
        color: 'white',
        maxWidth: '50%',
        mt: '10%',
        ml: '25px'
      }}>
        <Fade in={true} timeout={1000}>
          <Typography
            variant="h2"
            fontFamily='Fahkwang, sans-serif'
            sx={{
              fontWeight: '600',
              fontSize: '7vh',
              maxWidth: '700px',
              mb: 2
            }}
          >
            IT'S EASY TO TAKE EVERYTHING YOU NEED
          </Typography>
        </Fade>
        <Fade in={true} timeout={1100}>
          <Box
            maxWidth='330px'
          >
            <Typography
              color="primary.light"
              variant="body1"
              fontSize='12px'
              sx={{ mb: 3, mt: 8 }}
            >
              At our rental store, we offer a wide range of high-quality products and equipment to meet all your rental needs.
              Whether you're planning a special event, tackling a home improvement project, or exploring the great outdoors,
              we have the tools and gear to help you get the job done.
            </Typography>
          </Box>
        </Fade>
        <Fade in={true} timeout={1200}>
          <Button
            onClick={() => navigate(ABOUT_ROUTE)}
            sx={{ color: 'orange', textDecoration: 'none', fontWeight: 'bold' }}>
            <Box sx={{ display: "flex" }}>
              <Typography sx={{ mr: 2 }}>
                Read More
              </Typography>
              <EastIcon />
            </Box>
          </Button>
        </Fade>
      </Box>
      <Fade in={true} timeout={1250}>
        <Box
          sx={{
            display: 'flex',
            justifyContent: 'right',
            alignItems: 'flex-end',
            color: 'white',
            mr: 6,
            mt: 4
          }}>
          <Typography
            variant="h3"
            fontFamily='Fahkwang, sans-serif'
            sx={{
              textTransform: 'uppercase',
              fontWeight: '600',
              fontSize: '6vh',
              mb: 2
            }}
          >
            How to use it
          </Typography>
        </Box>
      </Fade>
      <HowToUse />
    </Box >
  );
}
