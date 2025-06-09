import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import AppRoutes from "./routes/AppRoutes";
import theme from '../theme.json';
import { ConfigProvider } from "antd";

function App() {
    const queryClient = new QueryClient();

    return (
        <QueryClientProvider client={queryClient}>
            <ConfigProvider theme={theme}>
                <AppRoutes />
            </ConfigProvider>
        </QueryClientProvider>
    );
}

export default App;