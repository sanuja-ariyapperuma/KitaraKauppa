import { TextField } from "@mui/material";
import { forwardRef } from "react";

type KKTextBoxProps = {
  Id: string;
  Label: string;
  Value: string;
  Type?: "text" | "password" | null;
  [x: string]: any;
};

const KKTextBox = forwardRef<HTMLInputElement, KKTextBoxProps>((props, ref) => {
  const { Id, Label, Value, Type, ...rest } = props;

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    //OnChange(event.target.value);
  };

  return (
    <TextField
      id={Id}
      label={Label}
      variant="standard"
      sx={{
        width: "100%",
      }}
      inputRef={ref}
      type={Type ?? "text"}
      {...rest}
    />
  );
});

export default KKTextBox;
