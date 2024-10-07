import {
  BaseQueryFn,
  createApi,
  fetchBaseQuery,
} from "@reduxjs/toolkit/query/react";
import { AuthenticationResult, LoginRequest } from "../types/authentication";
import { KKResult, PaginatedResult } from "../types/commonTypes";
import {
  ExistingProduct,
  NewProductSaveRequest,
  ProductDefinitionDto,
  ProductDto,
  ProductQueryOptions,
} from "../types/productTypes";
import { RootState } from "../store/store";
import { logoutSuccess } from "../auth/authSlice";
import {
  CustomerRegistrationForm,
  CustomerRegistrationRequest,
} from "../types/customerTypes";

const baseQuery = fetchBaseQuery({
  baseUrl: "https://localhost:7072/api",
  //baseUrl: "https://kitarakauppa.azurewebsites.net/api",
  prepareHeaders: (headers, { getState }) => {
    const token = (getState() as RootState).auth.token;
    if (token) {
      headers.set("Authorization", `Bearer ${token}`);
    }
    return headers;
  },
});

const baseQueryWithReAuth: BaseQueryFn = async (args, api, extraOptions) => {
  let result = await baseQuery(args, api, extraOptions);

  if (result.error && result.error.status === 401) {
    api.dispatch(logoutSuccess());

    //Preventing redirection on login failure
    if (api.endpoint != "login") window.location.href = "/login";
  }

  return result;
};

export const apiSlice = createApi({
  reducerPath: "api",
  baseQuery: baseQueryWithReAuth,
  endpoints: (builder) => ({
    //Authentications
    login: builder.mutation<
      KKResult<AuthenticationResult>,
      { username: string; password: string }
    >({
      query: (loginCredentials: LoginRequest) => ({
        url: "/v1/Auth/login",
        method: "POST",
        body: loginCredentials,
      }),
    }),
    logout: builder.mutation({
      query: () => ({
        url: "/v1/Auth/logout",
        method: "POST",
      }),
    }),

    //Product
    getAllProducts: builder.query<
      KKResult<PaginatedResult<ProductDto>>,
      ProductQueryOptions
    >({
      query: (searchQuery) => {
        const queryParams: Record<string, string> = {
          pageNo: searchQuery.pageNo.toString(),
          pageSize: searchQuery.pageSize.toString(),
          orderWith: searchQuery.orderWith,
          orderBy: searchQuery.orderBy.toString(),
          varientType: searchQuery.varientType?.toString() ?? "",
          search: searchQuery.search ?? "",
        };
        return "/v1/Products?" + new URLSearchParams(queryParams).toString();
      },
    }),
    getProductById: builder.query<KKResult<ExistingProduct>, string>({
      query: (productId) => `/v1/Products/${productId}`,
    }),
    deleteProduct: builder.mutation<KKResult<boolean>, string>({
      query: (productId) => ({
        url: `/v1/Products/${productId}`,
        method: "DELETE",
      }),
    }),
    addProduct: builder.mutation<KKResult<boolean>, NewProductSaveRequest>({
      query: (product) => ({
        url: "/v1/Products",
        method: "POST",
        body: product,
      }),
    }),

    //Definition data
    getDefinitions: builder.query<KKResult<ProductDefinitionDto>, void>({
      query: () => "/v1/Products/definition",
    }),

    //Customer
    addCustomer: builder.mutation<
      KKResult<boolean>,
      CustomerRegistrationRequest
    >({
      query: (customer) => ({
        url: "/v1/Users/profile",
        method: "POST",
        body: customer,
      }),
    }),
  }),
});

export const {
  useGetAllProductsQuery,
  useLoginMutation,
  useLogoutMutation,
  useDeleteProductMutation,
  useGetDefinitionsQuery,
  useAddProductMutation,
  useGetProductByIdQuery,
  useAddCustomerMutation,
} = apiSlice;
