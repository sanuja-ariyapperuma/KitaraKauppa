export interface AuthenticationResult {
  isAuthenticated: boolean;
  fullName: string;
  isAdmin: boolean;
  token: string;
}

export interface AuthState extends AuthenticationResult {}

export interface LoginRequest {
  username: string;
  password: string;
}
