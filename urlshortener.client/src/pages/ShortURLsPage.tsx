import { Layout } from "antd";
import SiteHeader from "../components/SiteHeader";
import SiteFooter from "../components/SiteFooter";

const { Content } = Layout;

const ShortURLsPage = () => {
    return (
        <Layout style={{ minHeight: "100vh", display: "flex", flexDirection: "column" }}>
            <SiteHeader />

            <Content style={{ flex: 1, display: "flex", justifyContent: "center", alignItems: "center", backgroundColor: "#f5f5f5" }}>
                <div style={{ maxWidth: "400px", width: "100%", padding: "20px", backgroundColor: "#fff", borderRadius: "8px", textAlign: "center" }}>
                    Short URLs Page
                </div>
            </Content>

            <SiteFooter />
        </Layout>
    );
};

export default ShortURLsPage;
