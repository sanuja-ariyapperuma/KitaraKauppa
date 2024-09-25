import Grid from "@mui/material/Grid2";
import { CSSProperties, useState } from "react";
import { useGetAllProductsQuery } from "../../features/api/apiSlice";
import {
  ProductOrderOptions,
  ProductVariant,
} from "../../features/types/productTypes";
import { useDispatch } from "react-redux";
import { setVariant } from "../../features/product/productFilterSlice";

const NavBar = () => {
  const dispatch = useDispatch();

  const navBarStyle: CSSProperties = {
    width: "300px",
    padding: "20px",
    backgroundColor: "#f0f0f0",
    height: "100vh",
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

  const handleOnClickCategory = (categoryId: number) => {
    switch (categoryId) {
      case 0:
        dispatch(setVariant(""));
        break;
      case 1:
        dispatch(setVariant("Accoustic"));
        break;
      case 2:
        dispatch(setVariant("Electric"));
        break;
      case 3:
        dispatch(setVariant("Bass"));
        break;
      case 4:
        dispatch(setVariant("Ukulele"));
        break;
      case 5:
        dispatch(setVariant("Classical"));
        break;
      case 6:
        dispatch(setVariant("Twelve String"));
        break;
      case 7:
        dispatch(setVariant("Jazz"));
        break;
      case 8:
        dispatch(setVariant("Acoustic Bass"));
        break;
      case 9:
        dispatch(setVariant("Seven String"));
        break;
      case 10:
        dispatch(setVariant("Semi Acoustic"));
        break;
      case 11:
        dispatch(setVariant("Banjo"));
        break;
      case 12:
        dispatch(setVariant("Silent"));
        break;
      case 13:
        dispatch(setVariant("Russian"));
        break;
      case 14:
        dispatch(setVariant("Contrabass"));
        break;
      default:
        dispatch(setVariant(""));
    }
  };

  return (
    <Grid style={navBarStyle} sx={{ display: { xs: "none", sm: "block" } }}>
      {/* TODO: Retrieve categories from db */}
      <ul style={ulStyle}>
        <li style={liStyle}>
          <a
            style={liAnchorStyle}
            href="#"
            onClick={() => handleOnClickCategory(0)}
          >
            ALL GUITARS
          </a>
        </li>
        <li style={liStyle}>
          <a
            style={liAnchorStyle}
            href="#"
            onClick={() => handleOnClickCategory(1)}
          >
            ACOUSTIC GUITARS
          </a>
        </li>
        <li style={liStyle}>
          <a
            style={liAnchorStyle}
            href="#"
            onClick={() => handleOnClickCategory(2)}
          >
            ELECTRIC GUITARS
          </a>
        </li>
        <li style={liStyle}>
          <a
            style={liAnchorStyle}
            href="#"
            onClick={() => handleOnClickCategory(3)}
          >
            BASSES GUITARS
          </a>
        </li>
        <li style={liStyle}>
          <a
            style={liAnchorStyle}
            href="#"
            onClick={() => handleOnClickCategory(4)}
          >
            UKULELES GUITARS
          </a>
        </li>
        <li style={liStyle}>
          <a
            style={liAnchorStyle}
            href="#"
            onClick={() => handleOnClickCategory(5)}
          >
            CLASSICAL GUITARS
          </a>
        </li>
        <li style={liStyle}>
          <a
            style={liAnchorStyle}
            href="#"
            onClick={() => handleOnClickCategory(6)}
          >
            TWELVE STRINGS GUITARS
          </a>
        </li>
        <li style={liStyle}>
          <a
            style={liAnchorStyle}
            href="#"
            onClick={() => handleOnClickCategory(7)}
          >
            JAZZ GUITARS
          </a>
        </li>
        <li style={liStyle}>
          <a
            style={liAnchorStyle}
            href="#"
            onClick={() => handleOnClickCategory(8)}
          >
            ACOUSTIC BASS GUITARS
          </a>
        </li>
        <li style={liStyle}>
          <a
            style={liAnchorStyle}
            href="#"
            onClick={() => handleOnClickCategory(9)}
          >
            SEVEN STRINGS GUITARS
          </a>
        </li>
        <li style={liStyle}>
          <a
            style={liAnchorStyle}
            href="#"
            onClick={() => handleOnClickCategory(10)}
          >
            SEMI ACOUSTIC GUITARS
          </a>
        </li>
        <li style={liStyle}>
          <a
            style={liAnchorStyle}
            href="#"
            onClick={() => handleOnClickCategory(11)}
          >
            BANJO GUITARS
          </a>
        </li>
        <li style={liStyle}>
          <a
            style={liAnchorStyle}
            href="#"
            onClick={() => handleOnClickCategory(12)}
          >
            SILENT GUITARS
          </a>
        </li>
        <li style={liStyle}>
          <a
            style={liAnchorStyle}
            href="#"
            onClick={() => handleOnClickCategory(13)}
          >
            RUSSIAN GUITARS
          </a>
        </li>
        <li style={liStyle}>
          <a
            style={liAnchorStyle}
            href="#"
            onClick={() => handleOnClickCategory(14)}
          >
            CONTRABASS GUITARS
          </a>
        </li>
      </ul>
    </Grid>
  );
};

export default NavBar;
