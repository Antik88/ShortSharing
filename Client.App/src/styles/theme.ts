import { createTheme } from '@mui/material/styles';

const theme = createTheme({
  palette: {
    primary: {
      light: '#b5b5b5',
      main: '#ffca8b',
      dark: '#002884',
      contrastText: '#fff',
    },
    secondary: {
      light: '#ff7961',
      main: '#f44336',
      dark: '#202020',
      contrastText: '#000',
    },
  },
  components: {
    MuiTextField: {
      styleOverrides: {
        root: {
          '& .MuiOutlinedInput-root': {
            '& fieldset': {
              borderColor: '#b5b5b5',
            },
            '&:hover fieldset': {
              borderColor: '#ffca8b',
            },
            '&.Mui-focused fieldset': {
              borderColor: '#ffca8b',
            },
            color: '#ffca8b',
          },
        },
      },
    },
    MuiInputLabel: {
      styleOverrides: {
        root: {
          color: '#b5b5b5',
          '&.Mui-focused': {
            color: '#ffca8b',
          },
          '&.MuiFormLabel-filled': {
            color: '#ffca8b',
          },
        },
      },
    },
    MuiSelect: {
      styleOverrides: {
        root: {
          '& .MuiOutlinedInput-notchedOutline': {
            borderColor: '#b5b5b5',
          },
          '&:hover .MuiOutlinedInput-notchedOutline': {
            borderColor: '#ffca8b',
          },
          '&.Mui-focused .MuiOutlinedInput-notchedOutline': {
            borderColor: '#ffca8b',
          },
          color: '#ffca8b',
          width: 'auto',
        },
        icon: {
          color: '#ffca8b',
        },
      },
    },
    MuiPagination: {
      styleOverrides: {
        root: {
          '& .MuiPaginationItem-root': {
            color: '#ffca8b',
            '&.Mui-selected': {
              backgroundColor: '#ffca8b',
              color: '#000',
            },
            '&:hover': {
              backgroundColor: '#202020',
              color: '#000',
            },
          },
        },
      },
    },
  },
});

export default theme;