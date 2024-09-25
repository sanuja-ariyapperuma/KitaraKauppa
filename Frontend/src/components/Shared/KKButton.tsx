import { Button } from "@mui/material";

type KKButtonProps = {
  OnClick?: () => void;
  Text?: string;
  Icon?: JSX.Element;
  Variant: "outlined" | "text" | "contained";
  IsFullWidth?: boolean;
  Type?: "button" | "submit" | "reset" | null;
  IsDisabled?: boolean | null;
};

const KKButton = (props: KKButtonProps) => {
  const { OnClick, Text, Icon, Variant, IsFullWidth, Type, IsDisabled } = props;

  const buttonStyle = {
    color: Text ? "white" : "black",
    backgroundColor: Text ? "black" : "white",
  };

  return (
    <Button
      variant={Variant}
      startIcon={Icon}
      onClick={OnClick}
      sx={buttonStyle}
      fullWidth={IsFullWidth ?? false}
      type={Type ?? "button"}
      disabled={IsDisabled ?? false}
    >
      {Text}
    </Button>
  );
};

export default KKButton;
