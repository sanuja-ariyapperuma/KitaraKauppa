import Grid from "@mui/material/Grid2";
import React from "react";
import KKTextBox from "./KKTextBox";
import KKButton from "./KKButton";
import SearchIcon from "@mui/icons-material/Search";
import LoginIcon from "@mui/icons-material/Login";
import { useNavigate } from "react-router-dom";
import { useToken } from "../../features/utilities/checkAuth";
import LogoutIcon from "@mui/icons-material/Logout";
import { useDispatch } from "react-redux";
import { logoutSuccess } from "../../features/auth/authSlice";
import { useLogoutMutation } from "../../features/api/apiSlice";
import { set } from "react-hook-form";
import { setSearchFilter } from "../../features/product/productFilterSlice";
import AddShoppingCartIcon from "@mui/icons-material/AddShoppingCart";
import { showCart } from "../../features/cart/cartSlice";

const KKTopBar = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();

  const [logout, { isLoading }] = useLogoutMutation();

  const [searchText, setSearchText] = React.useState("");

  const token = useToken();

  const handleOnClickLogin = () => {
    navigate("/login");
  };

  const handleOnClickLogout = async () => {
    await logout({})
      .unwrap()
      .then(() => {
        dispatch(logoutSuccess());
        navigate("/");
      })
      .catch((error) => {
        console.error("An unexpected error occurred:", error);
      });
  };

  const handleSearch = () => {
    dispatch(setSearchFilter(searchText));
  };

  const handleSearchTextChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setSearchText(event.target.value);
  };

  return (
    <Grid
      container
      size={12}
      style={{
        justifyContent: "center",
      }}
    >
      <Grid size={{ xs: 11, sm: 6, md: 4 }}>
        <KKTextBox
          Id="itemSearch"
          Label="Search for items"
          onChange={handleSearchTextChange}
          Value={searchText}
        />
      </Grid>
      <Grid size={1} sx={{ paddingTop: "10px" }}>
        <KKButton OnClick={handleSearch} Icon={<SearchIcon />} Variant="text" />
      </Grid>
      <Grid
        size={{ xs: 12, sm: 1 }}
        sx={{
          paddingTop: "10px",
          justifyContent: { xs: "center", sm: "flex-start" },
        }}
      >
        {token ? (
          <KKButton
            OnClick={handleOnClickLogout}
            Text="Logout"
            Variant="contained"
            IsFullWidth={true}
            Icon={<LogoutIcon />}
          />
        ) : (
          <KKButton
            OnClick={handleOnClickLogin}
            Text="Login"
            Variant="contained"
            IsFullWidth={true}
            Icon={<LoginIcon />}
          />
        )}
      </Grid>
      {token && (
        <Grid size={1} style={{ paddingTop: "10px" }}>
          <KKButton
            OnClick={() => dispatch(showCart(true))}
            Variant="text"
            IsFullWidth={true}
            Icon={<AddShoppingCartIcon />}
          />
        </Grid>
      )}
    </Grid>
  );
};

export default KKTopBar;
