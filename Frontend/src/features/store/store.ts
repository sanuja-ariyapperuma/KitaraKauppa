import { configureStore } from "@reduxjs/toolkit";
import productReducer from "../product/productFilterSlice";
import cartReducer from "../cart/cartSlice";
import { apiSlice } from "../api/apiSlice";
import authReducer from "../auth/authSlice";

export const store = configureStore({
  reducer: {
    product: productReducer,
    auth: authReducer,
    cart: cartReducer,
    [apiSlice.reducerPath]: apiSlice.reducer,
  },

  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(apiSlice.middleware),
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
