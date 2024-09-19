import { BrowserRouter } from 'react-router-dom';
import AppRouter from './components/AppRouter';
import { ThemeProvider } from '@mui/material/';
import NavBar from './components/NavBar';
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
