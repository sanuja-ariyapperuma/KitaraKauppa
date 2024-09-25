import { Box, Modal, Typography } from "@mui/material";
import Grid from "@mui/material/Grid2";
import React from "react";
import { useSelector } from "react-redux";
import { RootState } from "../../features/store/store";
import { useDispatch } from "react-redux";
import { clearCart, showCart } from "../../features/cart/cartSlice";
import KKButton from "../Shared/KKButton";
import ShoppingCartCheckoutIcon from "@mui/icons-material/ShoppingCartCheckout";
import DeleteForeverIcon from "@mui/icons-material/DeleteForever";

const style = {
  position: "absolute" as "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 400,
  bgcolor: "background.paper",
  border: "2px solid #000",
  boxShadow: 24,
  p: 4,
};

const Cart = () => {
  const dispatch = useDispatch();
  const handleClose = () => dispatch(showCart(false));

  const cartVisible = useSelector((state: RootState) => state.cart.showCart);
  const cartItems = useSelector((state: RootState) => state.cart.items);

  return (
    <Modal
      open={cartVisible}
      onClose={handleClose}
      aria-labelledby="modal-modal-title"
      aria-describedby="modal-modal-description"
    >
      <Box sx={style}>
        <Typography
          id="modal-modal-title"
          variant="h5"
          style={{ marginBottom: "20px" }}
        >
          Your Cart
        </Typography>
        {cartItems.length > 0 ? (
          <>
            <table width="100%" cellSpacing={5}>
              <thead>
                <tr>
                  <th></th>
                  <th></th>
                  <th align="right">Unit Price ( € )</th>
                  <th align="right">Subtotal ( € )</th>
                </tr>
              </thead>
              <tbody>
                {cartItems.map((item) => (
                  <tr key={item.id}>
                    <td>{item.name}</td>
                    <td>x{item.quantity}</td>
                    <td align="right">
                      {parseFloat(item.unitPrice.toString()).toFixed(2)}
                    </td>
                    <td align="right">
                      {parseFloat(item.subTotal.toString()).toFixed(2)}
                    </td>
                  </tr>
                ))}
                <tr>
                  <td colSpan={3} align="right">
                    <Typography
                      id="modal-modal-title"
                      variant="h5"
                      style={{ marginBottom: "20px" }}
                    >
                      Total
                    </Typography>
                  </td>
                  <td align="right">
                    <Typography
                      id="modal-modal-title"
                      variant="h5"
                      style={{ marginBottom: "20px" }}
                    >
                      {parseFloat(
                        cartItems
                          .reduce((acc, item) => acc + item.subTotal, 0)
                          .toString()
                      ).toFixed(2)}{" "}
                      €
                    </Typography>
                  </td>
                </tr>
              </tbody>
            </table>
            <Grid style={{ marginTop: "20px" }}>
              <KKButton
                OnClick={() => dispatch(showCart(true))}
                Variant="text"
                Text="Checkout"
                IsFullWidth={true}
                Icon={<ShoppingCartCheckoutIcon />}
              />
            </Grid>
            <Grid style={{ marginTop: "10px" }}>
              <KKButton
                OnClick={() => dispatch(clearCart())}
                Variant="text"
                Text="Clear Cart"
                IsFullWidth={true}
                Icon={<DeleteForeverIcon />}
              />
            </Grid>
          </>
        ) : (
          <Typography id="modal-modal-description" sx={{ mt: 2 }}>
            You have no items in your cart . . . . .
          </Typography>
        )}
      </Box>
    </Modal>
  );
};

export default Cart;
