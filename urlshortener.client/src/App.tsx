import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import AppRoutes from "./routes/AppRoutes";
import theme from '../theme.json';
import { ConfigProvider } from "antd";
import { AuthProvider } from "./context/AuthContext";
import { Toaster } from "sonner";

function App() {
    const queryClient = new QueryClient();

    return (
        <QueryClientProvider client={queryClient}>
            <AuthProvider>
                <ConfigProvider theme={theme}>
                    <Toaster position="top-right" richColors />
                    <AppRoutes />
                </ConfigProvider>
            </AuthProvider>
        </QueryClientProvider>
    );
}

export default App;