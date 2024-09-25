import { KKAutoCompleteOptionType } from "../../components/Shared/KKAutoComplete";

export interface CustomerRegistrationForm {
  firstName: string;
  lastName: string;
  email: string;
  contactNumber: string;
  addressLine1: string;
  addressLine2: string;
  cityId: KKAutoCompleteOptionType;
  username: string;
  password: string;
}

export interface CustomerRegistrationRequest {
  firstName: string;
  lastName: string;
  email: string;
  contactNumbers: string[];
  addressLine1: string;
  addressLine2: string;
  cityId: string;
  username: string;
  password: string;
}
