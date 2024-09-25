import { Alert, Card } from "@mui/material";
import Grid from "@mui/material/Grid2";
import React, { useEffect, useState } from "react";
import KKTextBox from "../components/Shared/KKTextBox";
import KKButton from "../components/Shared/KKButton";
import DriveFileRenameOutlineIcon from "@mui/icons-material/DriveFileRenameOutline";
import KKAutoComplete, {
  KKAutoCompleteOptionType,
} from "../components/Shared/KKAutoComplete";
import { useForm } from "react-hook-form";
import {
  CustomerRegistrationForm,
  CustomerRegistrationRequest,
} from "../features/types/customerTypes";
import { useAddCustomerMutation } from "../features/api/apiSlice";
import { useNavigate } from "react-router-dom";
import { FetchBaseQueryError } from "@reduxjs/toolkit/query";

const CustomerRegistration = () => {
  const cityOptions: KKAutoCompleteOptionType[] = [
    { label: "Espoo", id: "0dfb4d4e-f694-4101-b0e6-e04c73c40584" },
    { label: "Helsinki", id: "1f819ac4-52a1-4c27-ad03-3ba21159e85a" },
    { label: "Oulu", id: "62046a0d-177f-4611-869b-cc14ff44ecef" },
    { label: "Vantaa", id: "99950502-4beb-45a0-ab95-5285f0e9a517" },
    { label: "Tampere", id: "ac0a350a-fdff-46d0-9839-e2057a81d62a" },
    { label: "Turku", id: "b423dcc6-00d2-4a9a-871d-e8900fd48c97" },
  ];

  const navigate = useNavigate();

  const [serverValidationErrors, setServerValidationErrors] = useState<
    string[]
  >([]);

  const {
    register,
    handleSubmit,
    formState: { errors },
    watch,
    setValue,
  } = useForm<CustomerRegistrationForm>();

  const [addCustomer, { isError, error }] = useAddCustomerMutation();

  const onSubmit = async (data: CustomerRegistrationForm) => {
    setServerValidationErrors([]);
    const saveCustomer: CustomerRegistrationRequest = {
      firstName: data.firstName,
      lastName: data.lastName,
      email: data.email,
      contactNumbers: [data.contactNumber],
      addressLine1: data.addressLine1,
      addressLine2: data.addressLine2,
      cityId: data.cityId.id,
      username: data.username,
      password: data.password,
    };

    type DynamicObject = {
      [key: string]: string[];
    };

    await addCustomer(saveCustomer)
      .unwrap()
      .then((response) => {
        alert("Customer registered successfully");
        navigate("/login");
      })
      .catch((error) => {
        console.log("api errors :", error.data.errors);
        const serverErrors = error.data.errors;
        if (serverErrors) {
          setServerValidationErrors(
            Object.values(serverErrors).map((arr) => {
              if (Array.isArray(arr)) {
                return arr[0];
              }
            })
          );
        }
      });
  };

  return (
    <Grid container size={12} flexDirection="row" justifyContent="center">
      <Card sx={{ padding: "20px", marginTop: "20px", width: "600px" }}>
        <h2>Customer Registration</h2>
        <form onSubmit={handleSubmit(onSubmit)}>
          <Grid container size={12} flexDirection="row" spacing={5}>
            <Grid container size={6}>
              <KKTextBox
                label="First Name"
                type="text"
                {...register("firstName", { required: true })}
              />
            </Grid>
            <Grid container size={6}>
              <KKTextBox
                label="Last Name"
                type="text"
                {...register("lastName", { required: true })}
              />
            </Grid>
          </Grid>
          <Grid container size={12} flexDirection="row" spacing={5}>
            <Grid container size={6}>
              <KKTextBox
                label="Email"
                type="email"
                {...register("email", { required: true })}
              />
            </Grid>
            <Grid container size={6}>
              <KKTextBox
                label="Contact Number"
                type="text"
                {...register("contactNumber", { required: true })}
              />
            </Grid>
          </Grid>
          <Grid container size={12} flexDirection="row" spacing={5}>
            <Grid container size={6}>
              <KKTextBox
                label="Address Line 1"
                type="text"
                {...register("addressLine1", { required: true })}
              />
            </Grid>
            <Grid container size={6}>
              <KKTextBox
                label="Address Line 2"
                type="text"
                {...register("addressLine2", { required: true })}
              />
            </Grid>
          </Grid>
          <Grid container size={12} flexDirection="row" spacing={5}>
            <KKAutoComplete
              id="cityId"
              options={cityOptions}
              label="City"
              register={register}
              setValue={setValue}
              registerName="cityId"
            />
          </Grid>
          <Grid container size={12} flexDirection="row" spacing={5}>
            <Grid container size={6}>
              <KKTextBox
                label="Username"
                type="text"
                {...register("username", { required: true })}
              />
            </Grid>
            <Grid container size={6}>
              <KKTextBox
                label="Password"
                type="password"
                {...register("password", { required: true })}
              />
            </Grid>
          </Grid>
          <Grid style={{ marginTop: "20px" }} size={12} textAlign="right">
            <KKButton
              Text="Register"
              Variant="text"
              Icon={<DriveFileRenameOutlineIcon />}
              Type="submit"
            />
            {isError &&
              serverValidationErrors.map((serverError) => (
                <Alert severity="error">{serverError}</Alert>
              ))}
          </Grid>
        </form>
      </Card>
    </Grid>
  );
};

export default CustomerRegistration;
