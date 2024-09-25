import Grid from "@mui/material/Grid2";
import AdminNavBar from "../../components/AdminPanel/AdminNavBar";
import { Outlet, Route, Routes } from "react-router-dom";
import Dashboard from "./Dashboard";
import Products from "./Products";
import Users from "./Users";
import Orders from "./Orders";
import AddEditProducts from "./AddEditProducts";

const AdminPanel = () => {
  return (
    <Grid container rowSpacing={1}>
      <AdminNavBar />
      <Routes>
        <Route path="/" element={<Dashboard />} />
        <Route path="products">
          <Route index element={<Products />} />
          <Route path="addEdit/:productId?" element={<AddEditProducts />} />
        </Route>
        <Route path="users" element={<Users />} />
        <Route path="orders" element={<Orders />} />
        <Route path="*" element={<h1>Not Found</h1>} />
      </Routes>
      <Outlet />
    </Grid>
  );
};

export default AdminPanel;
