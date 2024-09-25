import Grid from "@mui/material/Grid2";
import { CSSProperties } from "react";

const Orders = () => {
  const containerStyle: CSSProperties = {
    marginLeft: "400px",
    marginTop: "20px",
  };

  return (
    <Grid style={containerStyle}>
      <h1>Orders</h1>
    </Grid>
  );
};

export default Orders;
