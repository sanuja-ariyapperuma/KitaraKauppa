import React, { CSSProperties } from "react";
import Grid from "@mui/material/Grid2";
import { useNavigate } from "react-router-dom";
import { useDispatch } from "react-redux";
import { showCart } from "../../features/cart/cartSlice";

const Footer = () => {
  const footerStyle: CSSProperties = {
    backgroundColor: "#000",
    color: "#fff",
    padding: "20px",
    textAlign: "center",
    width: "100%",
  };

  const footerAnchorStyle: CSSProperties = {
    color: "#fff",
    textDecoration: "none",
  };

  const footerListStyle: CSSProperties = {
    listStyle: "none",
    padding: "0",
  };

  const navigate = useNavigate();
  const dispatch = useDispatch();

  return (
    <>
      <Grid
        container
        direction="row"
        style={footerStyle}
        justifyContent="center"
      >
        <Grid container size={3} direction="column" textAlign="left">
          <h3>READ MORE</h3>
          <ul style={footerListStyle}>
            <li>
              <a style={footerAnchorStyle} href="#">
                Shipping policy
              </a>
            </li>
            <li>
              <a style={footerAnchorStyle} href="#">
                Pay later with Klarna
              </a>
            </li>
            <li>
              <a style={footerAnchorStyle} href="#">
                About us
              </a>
            </li>
            <li>
              <a style={footerAnchorStyle} href="#">
                Our shop in Tampere
              </a>
            </li>
            <li>
              <a style={footerAnchorStyle} href="#">
                We Ship Within EU
              </a>
            </li>
            <li>
              <a style={footerAnchorStyle} href="#">
                Guitar Service
              </a>
            </li>
          </ul>
        </Grid>
        <Grid container size={3} direction="column" textAlign="left">
          <div>
            <h3>YOUR PAGES</h3>
            <ul style={footerListStyle}>
              <li>
                <a
                  style={footerAnchorStyle}
                  href="#"
                  onClick={() => navigate("/login")}
                >
                  Log in
                </a>
              </li>
              <li>
                <a
                  style={footerAnchorStyle}
                  href="#"
                  onClick={() => navigate("/register")}
                >
                  Create account
                </a>
              </li>
              <li>
                <a
                  style={footerAnchorStyle}
                  href="#"
                  onClick={() => dispatch(showCart(true))}
                >
                  Cart
                </a>
              </li>
            </ul>
          </div>
        </Grid>
        <Grid container size={3} direction="column" textAlign="left">
          <div>
            <h3>CUSTOMER SERVICE</h3>
            <p>E-MAIL: customerservice@kitarakauppa.com</p>
            <p>PHONE: +358 44 123 456</p>
          </div>
        </Grid>
        <Grid container size={3} direction="column" textAlign="left">
          <div>
            <h3>OUR SHOP IN TAMPERE</h3>
            <p>
              Taitonikantie 124
              <br />
              33270 Jyväskylä
            </p>
            <p>
              OPENING HOURS:
              <br />
              Tuesday 12-17.30
              <br />
              Wednesday 12-17.30
              <br />
              Thursday 12-17.30
              <br />
              Saturday 11-15
            </p>
            <p>Other times store is open by appointment.</p>
          </div>
        </Grid>
      </Grid>
      <Grid container justifyContent="center" size={12}>
        <p>Copyright © 2024 Sanuja Ariyapperuma</p>
      </Grid>
    </>
  );
};

export default Footer;
