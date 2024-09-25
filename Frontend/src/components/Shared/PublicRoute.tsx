import { ReactNode } from "react";
import { useSelector } from "react-redux";
import { RootState } from "../../features/store/store";
import { useDispatch } from "react-redux";
import { loginSuccess, logoutSuccess } from "../../features/auth/authSlice";
import { Navigate } from "react-router-dom";
import { useToken } from "../../features/utilities/checkAuth";

interface ProtectedRouteProps {
  children: ReactNode;
}

const PublicRoute = ({ children }: ProtectedRouteProps) => {
  const storedToken = useToken();

  const isAdmin = useSelector((state: RootState) => state.auth.isAdmin);

  if (storedToken) {
    return isAdmin ? <Navigate to="/admin" /> : <Navigate to="/" />;
  }

  return <>{children}</>;
};

export default PublicRoute;
