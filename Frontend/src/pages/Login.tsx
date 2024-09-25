import { Alert, Box, Card, CircularProgress, Typography } from "@mui/material";
import Grid from "@mui/material/Grid2";
import KKTextBox from "../components/Shared/KKTextBox";
import KKButton from "../components/Shared/KKButton";
import DriveFileRenameOutlineIcon from "@mui/icons-material/DriveFileRenameOutline";
import LoginIcon from "@mui/icons-material/Login";
import { useForm } from "react-hook-form";
import { LoginRequest } from "../features/types/authentication";
import { useLoginMutation } from "../features/api/apiSlice";
import { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { loginSuccess } from "../features/auth/authSlice";
import { useNavigate } from "react-router-dom";

const Login = () => {
  const {
    register,
    handleSubmit,
    formState: { errors },
    watch,
  } = useForm<LoginRequest>();

  const dispatch = useDispatch();
  const navigate = useNavigate();

  const [usernamePasswordInvalid, setUsernamePasswordInvalid] = useState(false);

  const username = watch("username");
  const password = watch("password");

  useEffect(() => {
    setUsernamePasswordInvalid(false);
  }, [username, password]);

  const [login, { isLoading }] = useLoginMutation();

  const onSubmit = async (data: LoginRequest) => {
    try {
      setUsernamePasswordInvalid(false);
      const { username, password } = data;
      const result = await login({ username, password }).unwrap();

      if (result.isAuthenticated) {
        const { token, fullName, isAdmin } = result;

        dispatch(
          loginSuccess({
            token: token,
            fullName: fullName,
            isAdmin: isAdmin,
            isAuthenticated: true,
          })
        );
        isAdmin ? navigate("/admin") : navigate("/");
      } else {
        setUsernamePasswordInvalid(true);
      }
    } catch (error: any) {
      if (error.status === 401) {
        setUsernamePasswordInvalid(true);
      } else {
        console.error("An unexpected error occurred:", error);
      }
    }
  };

  return (
    <Grid container size={12} style={{ justifyContent: "center" }}>
      <form onSubmit={handleSubmit(onSubmit)}>
        <Card sx={{ padding: "20px", marginTop: "20px" }}>
          <h2>Kitara Kauppa</h2>
          <KKTextBox
            Id="username"
            Label="Username"
            Value=""
            Type="text"
            {...register("username", { required: true })}
          />
          <KKTextBox
            Id="password"
            Label="Password"
            Value=""
            Type="password"
            {...register("password", { required: true })}
          />
          <Grid
            container
            size={12}
            spacing={1}
            style={{ justifyContent: "right", marginTop: "10px" }}
          >
            <KKButton
              OnClick={() => navigate("/register")}
              Text="Register"
              Variant="text"
              Icon={<DriveFileRenameOutlineIcon />}
            />
            <KKButton
              Text="Login"
              Variant="contained"
              Icon={<LoginIcon />}
              Type={"submit"}
              IsDisabled={isLoading}
            />
          </Grid>
          <Grid container size={12} spacing={1} style={{ marginTop: "10px" }}>
            {isLoading && (
              <Box
                sx={{ display: "flex", width: "100%" }}
                justifyContent="center"
              >
                <CircularProgress sx={{ color: "black" }} />
              </Box>
            )}
            {errors.username && (
              <Alert severity="error" style={{ width: "100%" }}>
                <Typography>{"Username is required"}</Typography>
              </Alert>
            )}
            {errors.password && (
              <Alert severity="error" style={{ width: "100%" }}>
                <Typography>{"Password is required"}</Typography>
              </Alert>
            )}
            {usernamePasswordInvalid && (
              <Alert severity="error" style={{ width: "100%" }}>
                <Typography>{"Username or password is invalid"}</Typography>
              </Alert>
            )}
          </Grid>
        </Card>
      </form>
    </Grid>
  );
};

export default Login;
