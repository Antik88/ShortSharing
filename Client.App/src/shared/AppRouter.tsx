import { Routes, Route, Navigate } from 'react-router-dom';
import { authRoutes, publicRoutes } from '../routes';
import { MAINPAGE_ROUTE } from '../utils/consts';
import { RouteConfig } from '../types/types';

function AppRouter() {
  return (
    <Routes>
      {authRoutes.map(({ path, Component }: RouteConfig) => (
        <Route key={path} path={path} element={<Component />} />
      ))}
      {publicRoutes.map(({ path, Component }: RouteConfig) => (
        <Route key={path} path={path} element={<Component />} />
      ))}
      <Route path="*" element={<Navigate to={MAINPAGE_ROUTE} />} />
    </Routes>
  );
}

export default AppRouter;
