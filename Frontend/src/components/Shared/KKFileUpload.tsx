import styled from "@emotion/styled";
import CloudUploadIcon from "@mui/icons-material/CloudUpload";
import { Button } from "@mui/material";
import { UseFormRegister } from "react-hook-form";

const VisuallyHiddenInput = styled("input")({
  clip: "rect(0 0 0 0)",
  clipPath: "inset(50%)",
  height: 1,
  overflow: "hidden",
  position: "absolute",
  bottom: 0,
  left: 0,
  whiteSpace: "nowrap",
  width: 1,
});

type KKFileUploadProps = {
  OnFileUpload: (files: FileList | null) => void;
  UploadedFileName: string;
  UploadedFileExtension: string;
  register: UseFormRegister<any>;
};

const KKFileUpload = (props: KKFileUploadProps) => {
  const { OnFileUpload, UploadedFileName, UploadedFileExtension, register } =
    props;

  return (
    <div>
      <Button
        component="label"
        role={undefined}
        variant="contained"
        tabIndex={-1}
        startIcon={<CloudUploadIcon />}
        sx={{ color: "white", backgroundColor: "black" }}
      >
        Upload files
        <VisuallyHiddenInput
          type="file"
          onChange={(event: React.ChangeEvent<HTMLInputElement>) =>
            OnFileUpload(event.target.files)
          }
        />
        <VisuallyHiddenInput
          id="imageName"
          type="text"
          value={UploadedFileName}
          {...register("imageName")}
        />
        <VisuallyHiddenInput
          id="imageExtension"
          type="text"
          value={UploadedFileExtension}
          {...register("imageExtension")}
        />
      </Button>
    </div>
  );
};

export default KKFileUpload;
