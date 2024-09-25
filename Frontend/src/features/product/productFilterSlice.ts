import { createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";
import { ProductVariant } from "../types/productTypes";

export interface ProductFilter {
  variant: string;
  search: string;
}

const initialState: ProductFilter = {
  variant: "",
  search: "",
};

export const productFilterSlice = createSlice({
  name: "productFilter",
  initialState,
  reducers: {
    setSearchFilter: (state, PayloadAction) => {
      state.search = PayloadAction.payload;
    },
    setVariant: (state, PayloadAction) => {
      state.variant = PayloadAction.payload;
    },
  },
});

export const { setSearchFilter, setVariant } = productFilterSlice.actions;
export default productFilterSlice.reducer;
