import {
  DataGrid,
  GridCallbackDetails,
  GridColDef,
  GridEventListener,
  GridPaginationModel,
  GridRowsProp,
  GridSortModel,
} from "@mui/x-data-grid";

export interface KKDataGridProps {
  rows: GridRowsProp;
  columns: GridColDef[];
  paginationModel: GridPaginationModel;
  onPaginationChange: (
    model: GridPaginationModel,
    details: GridCallbackDetails
  ) => void;
  onSortChange: (model: GridSortModel, details: GridCallbackDetails) => void;
  rowCount: number;
}

const KKDataGrid = (props: KKDataGridProps) => {
  const {
    rows,
    columns,
    paginationModel,
    onPaginationChange,
    onSortChange,
    rowCount,
  } = props;

  return (
    <DataGrid
      rows={rows}
      columns={columns}
      rowCount={rowCount}
      onSortModelChange={onSortChange}
      paginationModel={paginationModel}
      onPaginationModelChange={onPaginationChange}
      paginationMode="server"
      sortingMode="server"
      disableAutosize
      disableColumnMenu
      columnVisibilityModel={{
        id: false,
      }}
    />
  );
};

export default KKDataGrid;
