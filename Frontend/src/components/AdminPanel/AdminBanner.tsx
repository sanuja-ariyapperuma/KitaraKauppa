import Grid from "@mui/material/Grid2";
import { CSSProperties } from "react";

const Banner = () => {
  //Define header styles
  const headerStyle: CSSProperties = {
    color: "#fff",
    backgroundColor: "#000",
    textAlign: "center",
    padding: "20px 0",
    width: "100%",
  };

  const h1Style: CSSProperties = {
    fontSize: "2.5rem",
    margin: "0",
  };

  const h2Style: CSSProperties = {
    fontSize: "1.25rem",
    margin: "0",
  };

  return (
    <Grid
      container
      size={12}
      sx={{
        display: {
          xs: "none",
          sm: "block",
        },
      }}
    >
      <div style={headerStyle}>
        <h1 style={h1Style}>KITARA KAUPPA</h1>
        <h2 style={h2Style}>Admin Panel</h2>
      </div>
    </Grid>
  );
};

export default Banner;
