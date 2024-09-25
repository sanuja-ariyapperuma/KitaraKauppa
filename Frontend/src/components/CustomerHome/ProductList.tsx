import Grid from "@mui/material/Grid2";
import ProductCard from "./ProductCard";
import KKPagination from "../Shared/KKPagination";
import { useDispatch, useSelector } from "react-redux";
import { useEffect, useState } from "react";
import { useGetAllProductsQuery } from "../../features/api/apiSlice";
import {
  ProductOrderOptions,
  ProductVariant,
} from "../../features/types/productTypes";
import { OrderBy } from "../../features/types/commonTypes";
import { RootState } from "../../features/store/store";
import { useToken } from "../../features/utilities/checkAuth";
import Cart from "../cart/Cart";
import { addToCart, CartItem } from "../../features/cart/cartSlice";

const ProductList = () => {
  const [pageNo, setPageNo] = useState(0);
  const [pageSize, setPageSize] = useState(10);
  const dispatch = useDispatch();

  const selectedCategory = useSelector(
    (state: RootState) => state.product.variant
  );

  const searchFiltration = useSelector(
    (state: RootState) => state.product.search
  );

  const token = useToken();

  const { data, error, isLoading, refetch } = useGetAllProductsQuery({
    search: searchFiltration,
    pageNo: pageNo,
    pageSize: pageSize,
    orderWith: ProductOrderOptions.ProductTitle,
    orderBy: OrderBy.ASC,
    varientType: selectedCategory,
  });

  useEffect(() => {
    if (selectedCategory || searchFiltration) {
      refetch();
    }
  }, [selectedCategory, searchFiltration, refetch]);

  const handlePageSizeChange = (newPageSize: number) =>
    setPageSize(newPageSize);
  const handlePageChange = (newPage: number) => {
    setPageNo(newPage - 1);
  };

  const handleAddToCart = (productId: string) => {
    if (!token) {
      alert("Please login to add to cart");
      return;
    }

    const selectedItem = data?.value?.results?.find(
      (p) => p.productId === productId
    )!;

    const cartItem: CartItem = {
      id: selectedItem.productId,
      name: selectedItem.productTitle,
      unitPrice: selectedItem.price,
      quantity: 1,
      subTotal: selectedItem.price,
    };

    dispatch(addToCart(cartItem));

    alert(`${cartItem.name}, added to cart`);
  };

  return (
    <div className="content">
      <Cart />
      <h2>
        {selectedCategory.trim() == "" ? "All" : selectedCategory} Guitars
      </h2>
      <Grid
        container
        size={12}
        spacing={5}
        sx={{ marginLeft: { xs: "10px", sm: "0" } }}
        justifyContent="center"
      >
        {error ? (
          <>No guitars found</>
        ) : isLoading ? (
          <>Loading...</>
        ) : data ? (
          data.succeeded &&
          data.value?.results?.map((product) => (
            <ProductCard
              key={product.productId}
              ImageAlt={product.imageAlt}
              ImageUrl={product.imageUrl}
              Price={product.price.toString()}
              ProductTitle={product.productTitle}
              Id={product.productId}
              BrandName={product.brandName}
              OnAddToCart={handleAddToCart}
            />
          ))
        ) : (
          <>No guitars found</>
        )}
      </Grid>
      <Grid container justifyContent="center">
        {error ? null : isLoading ? (
          <>Loading...</>
        ) : data ? (
          data.succeeded && (
            <KKPagination
              Page={data.value!.pageNo + 1}
              RowsPerPage={data.value?.pageSize ?? 10}
              TotalRecords={data.value?.totalRecords ?? 0}
              OnPageSizeChange={handlePageSizeChange}
              OnPageChange={handlePageChange}
            />
          )
        ) : null}
      </Grid>
    </div>
  );
};

export default ProductList;
