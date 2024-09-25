import { createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";
import { ProductVariant } from "../types/productTypes";

export interface CartItem {
  id: string;
  name: string;
  quantity: number;
  unitPrice: number;
  subTotal: number;
}

export interface Cart {
  showCart: boolean;
  items: CartItem[];
}

const initialState: Cart = {
  showCart: false,
  items: [],
};

export const cartSlice = createSlice({
  name: "cart",
  initialState,
  reducers: {
    addToCart: (state, PayloadAction) => {
      const existingCartItem = state.items.find(
        (item) => item.id === PayloadAction.payload.id
      );
      if (existingCartItem) {
        existingCartItem.quantity++;
        existingCartItem.subTotal =
          existingCartItem.quantity * existingCartItem.unitPrice;
        return;
      }

      state.items.push(PayloadAction.payload);
    },
    clearCart: (state) => {
      state.items = [];
    },
    showCart: (state, PayloadAction) => {
      state.showCart = PayloadAction.payload;
    },
  },
});

export const { addToCart, clearCart, showCart } = cartSlice.actions;
export default cartSlice.reducer;
