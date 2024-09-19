import { createRoot } from 'react-dom/client'
import { Auth0Provider } from '@auth0/auth0-react'
import App from './App.tsx'
import './styles/main.css'

createRoot(document.getElementById('root')!).render(
  <Auth0Provider
    domain={import.meta.env.VITE_DOMAIN}
    clientId={import.meta.env.VITE_CLIENT_ID}
    authorizationParams={{
      redirect_uri: window.location.origin,
      audience: import.meta.env.VITE_AUDIENCE_URL
    }}
  >
    <App />
  </Auth0Provider>
)
