import { TablePagination } from "@mui/material";

type KKPaginationPropsType = {
  Page: number;
  RowsPerPage: number;
  TotalRecords: number;
  OnPageSizeChange: (pageSize: number) => void;
  OnPageChange: (page: number) => void;
};

const KKPagination = (props: KKPaginationPropsType) => {
  const { Page, RowsPerPage, OnPageSizeChange, OnPageChange, TotalRecords } =
    props;

  const handleChangePage = (
    event: React.MouseEvent<HTMLButtonElement> | null,
    newPage: number
  ) => {
    OnPageChange(newPage + 1);
  };

  const handleChangeRowsPerPage = (
    event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    OnPageSizeChange(parseInt(event.target.value, 10));
  };

  return (
    <TablePagination
      component="div"
      count={TotalRecords}
      page={Page - 1}
      onPageChange={handleChangePage}
      rowsPerPage={RowsPerPage}
      onRowsPerPageChange={handleChangeRowsPerPage}
      labelRowsPerPage="Items per page"
      rowsPerPageOptions={[5, 10, 25, 50, 100]}
    />
  );
};

export default KKPagination;
