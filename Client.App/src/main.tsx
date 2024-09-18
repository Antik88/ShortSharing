import { createRoot } from 'react-dom/client'
import { Auth0Provider } from '@auth0/auth0-react'
import App from './App.tsx'
import './styles/main.css'

createRoot(document.getElementById('root')!).render(
  <Auth0Provider
    domain="dev-xcm2jj7b47bzuyh8.us.auth0.com"
    clientId="LVJeToW0iWHr7XmfbWtznZnES3m5UDrk"
    authorizationParams={{
      redirect_uri: window.location.origin,
      audience: "https://sharing.com"
    }}
  >
    <App />
  </Auth0Provider>
)
