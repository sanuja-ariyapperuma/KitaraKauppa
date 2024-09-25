import { Paper } from "@mui/material";
import { styled } from "@mui/material/styles";
import KKButton from "../Shared/KKButton";
import { AddShoppingCart } from "@mui/icons-material";
import Grid from "@mui/material/Grid2";

const Item = styled(Paper)(() => ({
  display: "flex",
  flexDirection: "column",
  textAlign: "center",
  height: "auto",
  lineHeight: "30px",
  padding: "20px 0px 20px 0px",
  width: "300px",
  justifyContent: "space-between",
}));

type ProductCardPropsType = {
  ImageUrl: string;
  ImageAlt: string;
  Price: string;
  ProductTitle: string;
  BrandName: string;
  Id: string;
  OnAddToCart: (id: string) => void;
};

const ProductCard = (props: ProductCardPropsType) => {
  const {
    ImageUrl,
    ImageAlt,
    Price,
    ProductTitle,
    BrandName,
    Id,
    OnAddToCart,
  } = props;

  const headerStyle = {
    margin: "0",
    padding: "0px",
  };

  return (
    <Item elevation={4}>
      <Grid container justifyContent="center">
        <img
          alt={ImageAlt}
          src={ImageUrl}
          style={{ height: "200px", width: "auto" }}
        />
      </Grid>

      <p style={headerStyle}>{BrandName}</p>
      <h4 style={headerStyle}>{ProductTitle}</h4>
      <h3 style={headerStyle}>{parseFloat(Price).toFixed(2)} €</h3>
      <Grid container justifyContent="center">
        <KKButton
          Text="Add to cart"
          Icon={<AddShoppingCart />}
          OnClick={() => {
            OnAddToCart(Id);
          }}
          Variant="contained"
        ></KKButton>
      </Grid>
    </Item>
  );
};

export default ProductCard;
