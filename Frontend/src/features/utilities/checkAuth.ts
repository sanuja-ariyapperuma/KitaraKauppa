import { useSelector } from "react-redux";
import { useDispatch } from "react-redux";
import { RootState } from "../store/store";
import { loginSuccess } from "../auth/authSlice";

// Utility function to check token and restore auth state from localStorage
export const useToken = () => {
  const dispatch = useDispatch();
  const token = useSelector((state: RootState) => state.auth.token);

  // Check if token exists in Redux or localStorage
  let storedToken: string | null = token;

  if (!token) {
    storedToken = localStorage.getItem("token");

    // If token exists in localStorage but not in Redux, dispatch loginSuccess
    if (storedToken) {
      const fullName = localStorage.getItem("fullName")!;
      const isAdmin: boolean = localStorage.getItem("isAdmin")! === "true";
      dispatch(
        loginSuccess({
          token: storedToken,
          fullName: fullName,
          isAdmin: isAdmin,
          isAuthenticated: true,
        })
      );
    }
  }

  return storedToken; // Return token if it exists, either from Redux or localStorage
};
