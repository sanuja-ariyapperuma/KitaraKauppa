import { CSSProperties } from "react";
import Grid from "@mui/material/Grid2";
import { Card, Typography } from "@mui/material";
import SalesChart from "../../components/AdminPanel/Charts/SalesChart";

const Dashboard = () => {
  const containerStyle: CSSProperties = {
    marginLeft: "400px",
    marginTop: "20px",
  };

  return (
    <Grid container style={containerStyle} size={12}>
      <h2>Dashboard</h2>
      <Grid container size={12} direction="row">
        <Card
          style={{
            width: "20%",
            margin: "10px",
            borderRadius: "1rem",
            padding: "1rem",
          }}
        >
          <h3>New Orders</h3>
          <Typography color="red" variant="h4" align="right">
            10
          </Typography>
        </Card>
        <Card
          style={{
            width: "20%",
            margin: "10px",
            borderRadius: "1rem",
            padding: "1rem",
          }}
        >
          <h3>Pending Shipments</h3>
          <Typography color="red" variant="h4" align="right">
            5
          </Typography>
        </Card>
        <Card
          style={{
            width: "20%",
            margin: "10px",
            borderRadius: "1rem",
            padding: "1rem",
          }}
        >
          <h3>Number of Orders / month</h3>
          <Typography color="red" variant="h4" align="right">
            55
          </Typography>
        </Card>
        <Card
          style={{
            width: "20%",
            margin: "10px",
            borderRadius: "1rem",
            padding: "1rem",
          }}
        >
          <h3>Returned Orders</h3>
          <Typography color="red" variant="h4" align="right">
            1
          </Typography>
        </Card>
      </Grid>
      <Grid container size={12}>
        <Card style={{ padding: "2rem", borderRadius: "1rem" }}>
          <h3>Sales / month</h3>
          <SalesChart />
        </Card>
      </Grid>
    </Grid>
  );
};

export default Dashboard;
