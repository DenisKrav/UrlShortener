import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import AppRoutes from "./routes/AppRoutes";
import theme from '../theme.json';
import { ConfigProvider } from "antd";
import { AuthProvider } from "./context/AuthContext";

function App() {
    const queryClient = new QueryClient();

    return (
        <QueryClientProvider client={queryClient}>
            <AuthProvider>
                <ConfigProvider theme={theme}>
                    <AppRoutes />
                </ConfigProvider>
            </AuthProvider>
        </QueryClientProvider>
    );
}

export default App;