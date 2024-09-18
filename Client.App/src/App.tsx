import { BrowserRouter } from 'react-router-dom';
import AppRouter from './components/AppRouter';
import { ThemeProvider, createTheme } from '@mui/material/';
import NavBar from './components/NavBar';

function App() {
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
  });

  return (
    <BrowserRouter>
      <ThemeProvider theme={theme}>
        <NavBar />
        <AppRouter />
      </ThemeProvider>
    </BrowserRouter>
  )
}

export default App
