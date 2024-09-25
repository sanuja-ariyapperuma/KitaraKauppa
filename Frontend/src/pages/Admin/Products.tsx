import { CSSProperties, useEffect, useState } from "react";
import Grid from "@mui/material/Grid2";
import KKButton from "../../components/Shared/KKButton";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import AddIcon from "@mui/icons-material/Add";

import {
  GridColDef,
  GridPaginationModel,
  GridRenderCellParams,
  GridRowsProp,
  GridSortModel,
} from "@mui/x-data-grid";
import KKDataGrid from "../../components/Shared/KKDataGrid";
import {
  useDeleteProductMutation,
  useGetAllProductsQuery,
} from "../../features/api/apiSlice";
import { ProductOrderOptions } from "../../features/types/productTypes";
import { OrderBy } from "../../features/types/commonTypes";
import { useNavigate } from "react-router-dom";

const Products = () => {
  interface Ordering {
    orderWith: ProductOrderOptions;
    orderBy: OrderBy;
  }

  const navigate = useNavigate();

  const [order, setOrder] = useState<Ordering>({
    orderWith: ProductOrderOptions.ProductTitle,
    orderBy: OrderBy.ASC,
  });

  const [paginationModel, setPaginationModel] = useState({
    pageSize: 5,
    page: 0,
  });

  const { data, error, isLoading, refetch, isSuccess } = useGetAllProductsQuery(
    {
      search: "",
      pageNo: paginationModel.page,
      pageSize: paginationModel.pageSize,
      orderWith: order.orderWith,
      orderBy: order.orderBy,
      varientType: "",
    }
  );

  useEffect(() => {
    if (isSuccess) {
      refetch();
    }
  }, [isSuccess]);

  const handleSortModelChange = (model: GridSortModel) => {
    if (model.length > 0) {
      const sortField: string = model[0].field;
      const sortDirection = model[0].sort ?? "asc";
      const orderWith = (sortField: string) => {
        switch (sortField) {
          case "col2":
            return ProductOrderOptions.ProductTitle;
          case "col4":
            return ProductOrderOptions.Price;
          default:
            return ProductOrderOptions.ProductTitle;
        }
      };
      const orderBy = (sortDirection: string) => {
        switch (sortDirection) {
          case "asc":
            return OrderBy.ASC;
          case "desc":
            return OrderBy.DESC;
          default:
            return OrderBy.ASC;
        }
      };
      setOrder({
        orderWith: orderWith(sortField),
        orderBy: orderBy(sortDirection),
      });
    }
  };

  const handlePageChange = (model: GridPaginationModel) => {
    const { pageSize, page } = model;

    setPaginationModel({
      pageSize: pageSize,
      page: page,
    });
  };

  const [deleteProduct] = useDeleteProductMutation();

  const handleDelete = async (id: number) => {
    const result = await deleteProduct(id.toString()).unwrap();
    if (result.succeeded) {
      alert("Product deleted successfully");
      refetch();
    } else {
      alert(result.message);
    }
  };

  const handleEdit = (id: number) => {
    navigate(`/admin/products/addEdit/${id}`);
  };

  const containerStyle: CSSProperties = {
    marginLeft: "350px",
    marginTop: "20px",
  };

  if (error) {
    return <div>Error loading products</div>;
  }

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (!data || !data.succeeded) {
    alert(data?.message);
    return null;
  }

  const rows: GridRowsProp = data!.value!.results.map((product) => ({
    id: product.productId,
    col2: product.productTitle,
    col3: product.brandName,
    col4: parseFloat(product.price.toString()).toFixed(2),
  }));

  const columns: GridColDef[] = [
    { field: "id", headerName: "Product Id", flex: 1, resizable: false },
    { field: "col2", headerName: "Product Title", flex: 1, resizable: false },
    {
      field: "col3",
      headerName: "Brand Name",
      flex: 1,
      resizable: false,
      sortable: false,
    },
    { field: "col4", headerName: "Price", flex: 1, resizable: false },
    {
      field: "EditDelete",
      headerName: "Edit / Delete",
      flex: 1,
      resizable: false,
      renderCell: (params: GridRenderCellParams<any, string>) => (
        <div>
          <KKButton
            OnClick={() => handleEdit(params.row.id)}
            Variant="text"
            Icon={<EditIcon />}
            Type="button"
          />
          <KKButton
            OnClick={() => handleDelete(params.row.id)}
            Variant="text"
            Icon={<DeleteIcon />}
            Type="button"
          />
        </div>
      ),
    },
  ];

  return (
    <Grid style={containerStyle} size={12} spacing={10} direction="column">
      <Grid>
        <h1>Products</h1>
      </Grid>
      <Grid>
        <KKButton
          OnClick={() => navigate("addEdit")}
          Text="Add new product"
          Variant="contained"
          Icon={<AddIcon />}
          Type="button"
        />
      </Grid>
      <Grid style={{ paddingTop: "1rem", paddingRight: "1rem" }} size={12}>
        <KKDataGrid
          rows={rows}
          columns={columns}
          paginationModel={paginationModel}
          onPaginationChange={handlePageChange}
          onSortChange={handleSortModelChange}
          rowCount={data.value?.totalRecords ?? 0}
        />
      </Grid>
    </Grid>
  );
};

export default Products;
