import { Layout, Table, Typography, Button, Space, Tooltip, Modal, Form, Input } from "antd";
import {
    EyeOutlined,
    EditOutlined,
    DeleteOutlined,
    PlusOutlined,
} from "@ant-design/icons";
import SiteHeader from "../components/SiteHeader";
import SiteFooter from "../components/SiteFooter";
import type { ColumnsType } from "antd/es/table";
import { useAuth } from "../context/AuthContext";
import { UserRole } from "../enums/UserRole ";
import { useMemo, useState } from "react";
import { useAddShortUrl } from "../api/Queries/ShortUrl/useAddShortUrl";
import type { ShortUrlAddModel } from "../api/Models/ShortUrl/ShortUrlAddModel";
import { useGetAllShortUrls } from "../api/Queries/ShortUrl/useGetAllShortUrl";

const { Content } = Layout;
const { Title } = Typography;

interface ShortUrl {
    key: string;
    shortCode: string;
    originalUrl: string;
}

const ShortURLsPage = () => {
    const BASE_URL = import.meta.env.VITE_ASPNETCORE_API_URL;
    const { userRole, token, userId } = useAuth();
    const isAdminOrManager = userRole === UserRole.Admin || userRole === UserRole.Manager;

    const [isAddModalVisible, setIsAddModalVisible] = useState(false);
    const [form] = Form.useForm();

    const { data: fetchedData, isLoading, refetch } = useGetAllShortUrls();
    const { mutate: addShortUrl, isPending } = useAddShortUrl(token ?? "");

    const handleAddUrl = () => {
        form.validateFields().then(values => {
            if (!userId) {
                console.error("User ID is missing.");
                return;
            }

            const urlData: ShortUrlAddModel = {
                originalUrl: values.originalUrl,
                createdByUserId: Number(userId),
            };

            addShortUrl(urlData, {
                onSuccess: () => {
                    form.resetFields();
                    setIsAddModalVisible(false);
                    refetch();
                },
                onError: (error: Error) => {
                    console.error(error.message);
                },
            });
        });
    };

    const columns: ColumnsType<ShortUrl> = useMemo(() => {
        const baseColumns: ColumnsType<ShortUrl> = [
            {
                title: "Original URL",
                dataIndex: "originalUrl",
                key: "originalUrl",
                render: (url: string) => (
                    <a href={url} target="_blank" rel="noopener noreferrer">
                        {url}
                    </a>
                ),
            },
            {
                title: "Short Code",
                dataIndex: "shortCode",
                key: "shortCode",
                render: (code: string) => (
                    <code style={{ backgroundColor: "#f0f0f0", padding: "2px 6px", borderRadius: "4px" }}>
                        {code}
                    </code>
                ),
            },
            {
                title: "Shortened URL",
                key: "shortenedUrl",
                render: (_, record) => {
                    const shortUrl = `${BASE_URL}/${record.shortCode}`;
                    return (
                        <a href={shortUrl} target="_blank" rel="noopener noreferrer">
                            {shortUrl}
                        </a>
                    );
                },
            },
        ];

        if (isAdminOrManager) {
            baseColumns.push({
                title: "Actions",
                key: "actions",
                render: (_, record) => (
                    <Space size="middle">
                        <Tooltip title="View">
                            <Button shape="circle" icon={<EyeOutlined />} onClick={() => console.log("View", record)} />
                        </Tooltip>
                        <Tooltip title="Edit">
                            <Button shape="circle" icon={<EditOutlined />} onClick={() => console.log("Edit", record)} />
                        </Tooltip>
                        <Tooltip title="Delete">
                            <Button
                                shape="circle"
                                danger
                                icon={<DeleteOutlined />}
                                onClick={() => {
                                    console.log("Delete", record);
                                    // TODO: реалізувати delete через mutation + refetch()
                                }}
                            />
                        </Tooltip>
                    </Space>
                ),
            });
        }

        return baseColumns;
    }, [isAdminOrManager]);

    const data: ShortUrl[] =
        fetchedData?.map(item => ({
            key: item.id.toString(),
            originalUrl: item.originalUrl,
            shortCode: item.shortCode,
        })) ?? [];

    return (
        <Layout style={{ minHeight: "100vh", display: "flex", flexDirection: "column" }}>
            <SiteHeader />

            <Content style={{ flex: 1, padding: "60px 20px", backgroundColor: "#f0f2f5" }}>
                <div
                    style={{
                        maxWidth: "1000px",
                        margin: "0 auto",
                        backgroundColor: "#fff",
                        padding: "32px",
                        borderRadius: "12px",
                        boxShadow: "0 4px 12px rgba(0,0,0,0.08)",
                    }}
                >
                    <div style={{ display: "flex", justifyContent: "space-between", alignItems: "center", marginBottom: "24px" }}>
                        <Title level={3} style={{ margin: 0 }}>
                            Shortened URLs
                        </Title>
                        {isAdminOrManager && (
                            <Button type="primary" icon={<PlusOutlined />} onClick={() => setIsAddModalVisible(true)}>
                                Add URL
                            </Button>
                        )}
                    </div>

                    <Table
                        columns={columns}
                        dataSource={data}
                        loading={isLoading}
                        pagination={false}
                        bordered
                        size="middle"
                    />
                </div>
            </Content>

            <SiteFooter />

            <Modal
                title="Add New URL"
                open={isAddModalVisible}
                onCancel={() => setIsAddModalVisible(false)}
                onOk={handleAddUrl}
                okText="Add"
                confirmLoading={isPending}
            >
                <Form form={form} layout="vertical">
                    <Form.Item
                        label="Original URL"
                        name="originalUrl"
                        rules={[
                            { required: true, message: "Please enter the original URL" },
                            { type: "url", message: "Please enter a valid URL" },
                        ]}
                    >
                        <Input placeholder="https://example.com/page" />
                    </Form.Item>
                </Form>
            </Modal>
        </Layout>
    );
};

export default ShortURLsPage;
