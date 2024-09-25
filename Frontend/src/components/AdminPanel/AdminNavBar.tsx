import { Typography } from "@mui/material";
import Grid from "@mui/material/Grid2";
import { CSSProperties, useState } from "react";
import { Link, Navigate, useNavigate } from "react-router-dom";
import KKButton from "../Shared/KKButton";
import { useDispatch } from "react-redux";
import { useLogoutMutation } from "../../features/api/apiSlice";
import { logoutSuccess } from "../../features/auth/authSlice";
import LogoutIcon from "@mui/icons-material/Logout";
import { Height } from "@mui/icons-material";
import { useToken } from "../../features/utilities/checkAuth";
import { useSelector } from "react-redux";
import { RootState } from "../../features/store/store";

const AdminNavBar = () => {
  const dispatch = useDispatch();
  const [logout, { isLoading }] = useLogoutMutation();
  const navigate = useNavigate();

  useToken();

  const loggedInUserName = useSelector(
    (state: RootState) => state.auth.fullName
  );

  const navBarStyle: CSSProperties = {
    width: "300px",
    padding: "20px",
    backgroundColor: "#f0f0f0",
    height: "100vh",
    position: "fixed",
  };

  const ulStyle: CSSProperties = {
    listStyle: "none",
    padding: "0",
    width: "95%",
  };

  const liStyle: CSSProperties = {
    margin: "10px 0",
    fontSize: "1rem",
    paddingBlock: "0.5em",
    paddingLeft: "0.5em",
    width: "100%",
  };

  const liAnchorStyle: CSSProperties = {
    textDecoration: "none",
    color: "#000",
  };

  const brandStyle: CSSProperties = {
    borderBottom: "1px solid #bcbcbc ",
    paddingBottom: "20px",
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

  return (
    <Grid
      style={navBarStyle}
      sx={{ display: { xs: "none", sm: "block" } }}
      direction="column"
    >
      <Grid container>
        <Grid style={brandStyle}>
          <h3>Kitara Kauppa Admin Panel</h3>
          <h4>Logged in as :</h4>
          <Typography>{loggedInUserName}</Typography>
        </Grid>
      </Grid>
      <Grid container style={{ height: "75%" }} width="100%">
        <ul style={ulStyle}>
          <li style={liStyle}>
            <Link style={liAnchorStyle} to="">
              DASHBOARD
            </Link>
          </li>
          <li style={liStyle}>
            <Link style={liAnchorStyle} to="orders">
              ORDERS
            </Link>
          </li>
          <li style={liStyle}>
            <Link style={liAnchorStyle} to="products">
              PRODUCTS
            </Link>
          </li>
          <li style={liStyle}>
            <Link style={liAnchorStyle} to="users">
              USERS
            </Link>
          </li>
        </ul>
      </Grid>
      <Grid container style={{ flexGrow: 1 }}>
        <KKButton
          OnClick={handleOnClickLogout}
          Text="Logout"
          Variant="contained"
          IsFullWidth={true}
          Icon={<LogoutIcon />}
        />
      </Grid>
    </Grid>
  );
};

export default AdminNavBar;
