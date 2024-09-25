export const mockFetchBaseQuery = jest.fn(() => ({
  data: {},
}));

jest.mock("@reduxjs/toolkit/query/react", () => ({
  ...jest.requireActual("@reduxjs/toolkit/query/react"),
  fetchBaseQuery: () => mockFetchBaseQuery,
}));
