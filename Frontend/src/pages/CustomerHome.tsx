import Grid from "@mui/material/Grid2";
import Banner from "../components/CustomerHome/Banner";
import NavBar from "../components/CustomerHome/NavBar";
import ProductList from "../components/CustomerHome/ProductList";
import KKPagination from "../components/Shared/KKPagination";
import Footer from "../components/CustomerHome/Footer";
import KKTopBar from "../components/Shared/KKTopBar";

const CustomerHome = () => {
  return (
    <Grid container rowSpacing={1}>
      <KKTopBar />
      <Banner />
      <Grid container size={12}>
        <Grid>
          <NavBar />
        </Grid>
        <ProductList />
      </Grid>
      <Grid container size={12}>
        <Footer />
      </Grid>
    </Grid>
  );
};

export default CustomerHome;
