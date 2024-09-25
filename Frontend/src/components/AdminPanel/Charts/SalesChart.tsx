import { MonitorHeart, Widgets } from "@mui/icons-material";
import {
  LineChart,
  Line,
  CartesianGrid,
  XAxis,
  YAxis,
  Tooltip,
} from "recharts";

const SalesChart = () => {
  const data = [
    { month: "January", numberOfSales: 50 },
    { month: "February", numberOfSales: 75 },
    { month: "March", numberOfSales: 30 },
    { month: "April", numberOfSales: 55 },
    { month: "May", numberOfSales: 100 },
    { month: "June", numberOfSales: 90 },
    { month: "July", numberOfSales: 85 },
    { month: "August", numberOfSales: 70 },
    { month: "September" },
    { month: "October" },
    { month: "November" },
    { month: "December" },
  ];

  return (
    <LineChart
      data={data}
      width={1500}
      height={500}
      margin={{ top: 5, right: 20, bottom: 5, left: 0 }}
      style={{ width: "100%", height: "100%" }}
    >
      <Line type="monotone" dataKey="numberOfSales" stroke="#8884d8" />
      <CartesianGrid stroke="#ccc" strokeDasharray="5 5" />
      <XAxis dataKey="month" />
      <YAxis />
      <Tooltip />
    </LineChart>
  );
};

export default SalesChart;
