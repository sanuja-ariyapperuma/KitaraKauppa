import { Autocomplete, TextField } from "@mui/material";
import { forwardRef } from "react";
import { UseFormRegister, UseFormSetValue } from "react-hook-form";

interface KKAutoCompleteProps {
  id: string;
  label: string;
  options: KKAutoCompleteOptionType[];
  setValue: UseFormSetValue<any>;
  register: UseFormRegister<any>;
  registerName: string;
}

export interface KKAutoCompleteOptionType {
  id: string;
  label: string;
}

const KKAutoComplete = forwardRef<HTMLInputElement, KKAutoCompleteProps>(
  (props, ref) => {
    const { id, label, options, setValue, register, registerName } = props;

    const {
      onChange,
      onBlur,
      name,
      ref: inputRef,
    } = register(registerName, { required: true });

    return (
      <Autocomplete
        id={id}
        disablePortal
        options={options}
        fullWidth={true}
        onChange={(_, value) => {
          setValue(registerName, value);
          onChange({ target: { value } });
        }}
        renderInput={(params) => (
          <TextField
            {...params}
            label={label}
            variant="standard"
            inputRef={ref}
            onBlur={onBlur}
          />
        )}
      />
    );
  }
);

export default KKAutoComplete;
