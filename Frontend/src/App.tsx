import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import CustomerHome from "./pages/CustomerHome";
import AdminDashboard from "./pages/Admin/AdminPanel";
import Login from "./pages/Login";
import ProtectedRoute from "./components/Shared/ProtectedRoute";
import PublicRoute from "./components/Shared/PublicRoute";
import CustomerRegistration from "./pages/CustomerRegistration";

const App = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<CustomerHome />} />
        <Route
          path="/login"
          element={
            <PublicRoute>
              <Login />
            </PublicRoute>
          }
        />
        <Route
          path="/register"
          element={
            <PublicRoute>
              <CustomerRegistration />
            </PublicRoute>
          }
        />
        <Route
          path="/admin/*"
          element={
            <ProtectedRoute>
              <AdminDashboard />
            </ProtectedRoute>
          }
        />
        <Route path="*" element={<div>404 Page Not Found</div>} />
      </Routes>
    </Router>
  );
};

export default App;
