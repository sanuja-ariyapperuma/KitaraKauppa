import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { AuthenticationResult, AuthState } from "../types/authentication";

const initialState: AuthState = {
  isAuthenticated: localStorage.getItem("token") ? true : false,
  fullName: localStorage.getItem("fullName") || "",
  isAdmin: localStorage.getItem("isAdmin") === "true",
  token: localStorage.getItem("token") || "",
};

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    loginSuccess: (state, action: PayloadAction<AuthenticationResult>) => {
      state.token = action.payload.token;
      state.fullName = action.payload.fullName;
      state.isAdmin = action.payload.isAdmin;
      state.isAuthenticated = true;
      localStorage.setItem("token", action.payload.token);
      localStorage.setItem("fullName", action.payload.fullName);
      localStorage.setItem("isAdmin", JSON.stringify(action.payload.isAdmin));
    },
    logoutSuccess: (state) => {
      state.token = "";
      state.fullName = "";
      state.isAdmin = false;
      localStorage.removeItem("token");
      localStorage.removeItem("fullName");
      localStorage.removeItem("isAdmin");
    },
  },
});

export const { loginSuccess, logoutSuccess } = authSlice.actions;
export default authSlice.reducer;
