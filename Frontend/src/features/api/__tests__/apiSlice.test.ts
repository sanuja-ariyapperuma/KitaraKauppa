import { setupApiStore } from "../../utilities/testSetupApiStore";
import { apiSlice } from "../apiSlice";
import {
  ExistingProduct,
  NewProductSaveRequest,
  ProductDto,
  ProductOrderOptions,
  ProductOrientation,
  ProductQueryOptions,
  ProductVariant,
} from "../../types/productTypes";
import { KKResult, OrderBy, PaginatedResult } from "../../types/commonTypes";
import { AuthenticationResult } from "../../types/authentication";
import { CustomerRegistrationRequest } from "../../types/customerTypes";
import { RootState } from "../../store/store";

beforeEach(() => {
  // Reset any mock implementations before each test
  jest.resetAllMocks();
});

const storeRef = setupApiStore(apiSlice);

describe("Products", () => {
  test("get all products successfully", async () => {
    const mockResponse: KKResult<PaginatedResult<ProductDto>> = {
      succeeded: true,
      value: {
        pageNo: 1,
        pageSize: 10,
        totalRecords: 100,
        totalPages: 10,
        results: [
          {
            productId: "someid",
            productTitle: "sometitle",
            brandName: "somebrand",
            imageAlt: "somealt",
            imageUrl: "someurl",
            price: 100,
          },
        ],
      },
      message: "",
    };

    // Mocking the global fetch method with a valid response structure
    global.fetch = jest.fn(() =>
      Promise.resolve({
        ok: true,
        status: 200,
        json: () => Promise.resolve(mockResponse),
        text: () => Promise.resolve(JSON.stringify(mockResponse)), // Mock text() method to return stringified JSON
        clone: () => ({
          json: () => Promise.resolve(mockResponse),
          text: () => Promise.resolve(JSON.stringify(mockResponse)), // Mock clone() with text() method
        }),
      } as Response)
    );

    const queryParams: ProductQueryOptions = {
      pageNo: 1,
      pageSize: 10,
      orderWith: ProductOrderOptions.ProductTitle,
      orderBy: OrderBy.ASC,
      varientType: "",
      search: "",
    };

    // Dispatch the RTK Query action
    const result = await storeRef.store.dispatch(
      (apiSlice as any).endpoints.getAllProducts.initiate(queryParams)
    );

    // Ensure fetch was called once
    expect(global.fetch).toHaveBeenCalledTimes(1);

    // Ensure the response is correctly returned
    expect(result.data).toEqual(mockResponse);
  });

  test("get single product", async () => {
    const mockResponse: KKResult<ExistingProduct> = {
      succeeded: true,
      value: {
        id: "c18ec26d-e90c-4e23-9d28-1d53dc33d618",
        title: "some title",
        description: "some description",
        brandId: "someid",
        unitPrice: 100,
        varientType: ProductVariant.accoustic,
        productOrientation: ProductOrientation.bothOptions,
        productColors: [""],
      },
      message: "",
    };
    global.fetch = jest.fn(() =>
      Promise.resolve({
        ok: true,
        status: 200,
        json: () => Promise.resolve(mockResponse),
        text: () => Promise.resolve(JSON.stringify(mockResponse)),
        clone: () => ({
          json: () => Promise.resolve(mockResponse),
          text: () => Promise.resolve(JSON.stringify(mockResponse)),
        }),
      } as Response)
    );

    const productId = "c18ec26d-e90c-4e23-9d28-1d53dc33d618";

    const result = await storeRef.store.dispatch(
      (apiSlice as any).endpoints.getProductById.initiate(productId)
    );
    expect(global.fetch).toHaveBeenCalledTimes(1);
    expect(result.data).toEqual(mockResponse);
  });

  test("delete product", async () => {
    const mockResponse: KKResult<boolean> = {
      succeeded: true,
      value: true,
      message: "",
    };
    global.fetch = jest.fn(() =>
      Promise.resolve({
        ok: true,
        status: 200,
        json: () => Promise.resolve(mockResponse),
        text: () => Promise.resolve(JSON.stringify(mockResponse)),
        clone: () => ({
          json: () => Promise.resolve(mockResponse),
          text: () => Promise.resolve(JSON.stringify(mockResponse)),
        }),
      } as Response)
    );

    const productId = "c18ec26d-e90c-4e23-9d28-1d53dc33d618";

    const result = await storeRef.store.dispatch(
      (apiSlice as any).endpoints.deleteProduct.initiate(productId)
    );
    expect(global.fetch).toHaveBeenCalledTimes(1);
    expect(result.data).toEqual(mockResponse);
  });

  test("add product", async () => {
    const mockResponse: KKResult<ExistingProduct> = {
      succeeded: true,
      value: {
        id: "c18ec26d-e90c-4e23-9d28-1d53dc33d618",
        title: "some title",
        description: "some description",
        brandId: "someid",
        unitPrice: 100,
        varientType: ProductVariant.accoustic,
        productOrientation: ProductOrientation.bothOptions,
        productColors: [""],
      },
      message: "",
    };
    global.fetch = jest.fn(() =>
      Promise.resolve({
        ok: true,
        status: 200,
        json: () => Promise.resolve(mockResponse),
        text: () => Promise.resolve(JSON.stringify(mockResponse)),
        clone: () => ({
          json: () => Promise.resolve(mockResponse),
          text: () => Promise.resolve(JSON.stringify(mockResponse)),
        }),
      } as Response)
    );

    const newProduct: NewProductSaveRequest = {
      title: "some title",
      description: "some description",
      brandId: "c18ec26d-e90c-4e23-9d28-1d53dc33d618",
      unitPrice: 1000,
      varientType: ProductVariant.accoustic,
      productOrientation: ProductOrientation.bothOptions,
      productColors: [],
    };

    const result = await storeRef.store.dispatch(
      (apiSlice as any).endpoints.addProduct.initiate(newProduct)
    );
    expect(global.fetch).toHaveBeenCalledTimes(1);
    expect(result.data).toEqual(mockResponse);
  });
});

describe("Authenticate", () => {
  test("login", async () => {
    const mockResponse: AuthenticationResult = {
      isAuthenticated: true,
      fullName: "somename",
      isAdmin: true,
      token: "jwt",
    };

    global.fetch = jest.fn(() =>
      Promise.resolve({
        ok: true,
        status: 200,
        json: () => Promise.resolve(mockResponse),
        text: () => Promise.resolve(JSON.stringify(mockResponse)),
        clone: () => ({
          json: () => Promise.resolve(mockResponse),
          text: () => Promise.resolve(JSON.stringify(mockResponse)),
        }),
      } as Response)
    );

    // Dispatch the RTK Query action
    const result = await storeRef.store.dispatch(
      (apiSlice as any).endpoints.login.initiate({
        username: "username",
        password: "password",
      })
    );

    expect(global.fetch).toHaveBeenCalledTimes(1);
    expect(result.data).toEqual(mockResponse);
  });
  test("logout", async () => {
    global.fetch = jest.fn(() =>
      Promise.resolve({
        ok: true,
        status: 200,
        json: () => Promise.resolve(undefined),
        text: () => Promise.resolve(JSON.stringify(undefined)),
        clone: () => ({
          json: () => Promise.resolve(undefined),
          text: () => Promise.resolve(JSON.stringify(undefined)),
        }),
      } as Response)
    );

    // Dispatch the RTK Query action
    const result = await storeRef.store.dispatch(
      (apiSlice as any).endpoints.logout.initiate()
    );

    expect(global.fetch).toHaveBeenCalledTimes(1);
  });
});

describe("Customer", () => {
  test("add new customer", async () => {
    const mockResponse: KKResult<boolean> = {
      succeeded: true,
      message: "",
      value: true,
    };

    global.fetch = jest.fn(() =>
      Promise.resolve({
        ok: true,
        status: 200,
        json: () => Promise.resolve(mockResponse),
        text: () => Promise.resolve(JSON.stringify(mockResponse)),
        clone: () => ({
          json: () => Promise.resolve(mockResponse),
          text: () => Promise.resolve(JSON.stringify(mockResponse)),
        }),
      } as Response)
    );

    const customer: CustomerRegistrationRequest = {
      firstName: "john",
      lastName: "cena",
      email: "a.b@c.com",
      contactNumbers: [],
      addressLine1: "address1",
      addressLine2: "address2",
      cityId: "c18ec26d-e90c-4e23-9d28-1d53dc33d618",
      username: "john",
      password: "cena",
    };

    // Dispatch the RTK Query action
    const result = await storeRef.store.dispatch(
      (apiSlice as any).endpoints.addCustomer.initiate(customer)
    );

    expect(global.fetch).toHaveBeenCalledTimes(1);
    expect(result.data).toEqual(mockResponse);
  });
});
