import { BrowserRouter } from 'react-router-dom';
import AppRouter from './shared/AppRouter';
import { ThemeProvider } from '@mui/material/';
import NavBar from './shared/NavBar';
import theme from './styles/theme';

function App() {
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
