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

const ProtectedRoute = ({ children }: ProtectedRouteProps) => {
  const token = useSelector((state: RootState) => state.auth.token);
  const dispatch = useDispatch();

  const storedToken = useToken();

  if (!storedToken) {
    dispatch(logoutSuccess());
    return <Navigate to="/login" />;
  }

  return <>{children}</>;
};

export default ProtectedRoute;
