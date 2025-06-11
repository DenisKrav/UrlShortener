import { Layout, Table, Typography, Button, Space, Tooltip, Modal, Form, Input, Descriptions, Popconfirm } from "antd";
import { EyeOutlined, DeleteOutlined, PlusOutlined } from "@ant-design/icons";
import SiteHeader from "../components/SiteHeader";
import SiteFooter from "../components/SiteFooter";
import type { ColumnsType } from "antd/es/table";
import { useAuth } from "../context/AuthContext";
import { UserRole } from "../enums/UserRole ";
import { useMemo, useState } from "react";
import { useAddShortUrl } from "../api/Queries/ShortUrl/useAddShortUrl";
import type { ShortUrlAddModel } from "../api/Models/ShortUrl/ShortUrlAddModel";
import { useGetAllShortUrls } from "../api/Queries/ShortUrl/useGetAllShortUrl";
import type { ShortUrlModel } from "../api/Models/ShortUrl/ShortUrlModel";
import { useDeleteShortUrl } from "../api/Queries/ShortUrl/useDeleteShortUrl";
import { toast } from "sonner";

const { Content } = Layout;
const { Title } = Typography;

const ShortURLsPage = () => {
    const BASE_URL = import.meta.env.VITE_ASPNETCORE_API_URL;
    const { userRole, token, userId } = useAuth();
    const isAdminOrManager = userRole === UserRole.Admin || userRole === UserRole.Manager;

    const [isAddModalVisible, setIsAddModalVisible] = useState(false);
    const [form] = Form.useForm();

    const [selectedUrl, setSelectedUrl] = useState<ShortUrlModel | null>(null);
    const [isViewModalVisible, setIsViewModalVisible] = useState(false);

    const { data: fetchedData, isLoading, refetch } = useGetAllShortUrls();
    const { mutate: addShortUrl, isPending } = useAddShortUrl(token ?? "");
    const { mutate: deleteShortUrlMutation, isPending: isDeleting } = useDeleteShortUrl(token ?? "");

    const handleAddUrl = () => {
        form.validateFields().then(values => {
            if (!userId) {
                console.error("User ID is missing.");
                toast.error("User ID is missing.");
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
                    toast.success("URL added successfully.");
                    refetch();
                },
                onError: (error: Error) => {
                    console.error(error.message);
                    toast.error(error.message);
                },
            });
        });
    };

    const handleDeleteUrl = (record: ShortUrlModel) => {
        if (!userId) {
            console.error("User ID is missing.");
            toast.error("User ID is missing.");
            return;
        }
        if (!isAdminOrManager) {
            console.error("You do not have permission to delete this URL.");
            toast.error("You do not have permission to delete this URL.");
            return;
        }
        if (record.createdByUserId !== Number(userId) && userRole !== UserRole.Admin) {
            console.error("You do not have permission to delete this URL.");
            toast.error("You do not have permission to delete this URL.");
            return;
        }

        deleteShortUrlMutation(
            {
                linkId: record.id,
                userId: Number(userId),
            },
            {
                onSuccess: () => {
                    toast.success("URL deleted successfully.");
                    refetch();
                },
                onError: (error: Error) => {
                    console.error("Failed to delete:", error.message);
                    toast.error(`Failed to delete: ${error.message}`);
                },
            }
        );
    };

    const columns: ColumnsType<ShortUrlModel> = useMemo(() => {
        const baseColumns: ColumnsType<ShortUrlModel> = [
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
                            <Button
                                shape="circle"
                                icon={<EyeOutlined />}
                                onClick={() => {
                                    setSelectedUrl(record);
                                    setIsViewModalVisible(true);
                                }}
                            />
                        </Tooltip>
                        <Popconfirm
                            title="Are you sure you want to delete this URL?"
                            onConfirm={() => handleDeleteUrl(record)}
                            okText="Yes"
                            cancelText="No"
                            okButtonProps={{ loading: isDeleting }}
                        >
                            <Tooltip title="Delete">
                                <Button shape="circle" danger loading={isDeleting} icon={<DeleteOutlined />} />
                            </Tooltip>
                        </Popconfirm>

                    </Space>
                ),
            });
        }

        return baseColumns;
    }, [isAdminOrManager]);

    const data: ShortUrlModel[] =
        fetchedData?.map(item => ({
            ...item,
            key: item.id.toString(),
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

            <Modal
                title="URL Details"
                open={isViewModalVisible}
                onCancel={() => setIsViewModalVisible(false)}
                footer={[
                    <Button key="close" onClick={() => setIsViewModalVisible(false)}>
                        Close
                    </Button>,
                ]}
            >
                {selectedUrl ? (
                    <Descriptions column={1} bordered>
                        <Descriptions.Item label="Original URL">
                            <Typography.Link href={selectedUrl.originalUrl} target="_blank">
                                {selectedUrl.originalUrl}
                            </Typography.Link>
                        </Descriptions.Item>

                        <Descriptions.Item label="Short Code">
                            <Typography.Text code>{selectedUrl.shortCode}</Typography.Text>
                        </Descriptions.Item>

                        <Descriptions.Item label="Shortened URL">
                            <Typography.Link href={`${BASE_URL}/${selectedUrl.shortCode}`} target="_blank">
                                {`${BASE_URL}/${selectedUrl.shortCode}`}
                            </Typography.Link>
                        </Descriptions.Item>

                        <Descriptions.Item label="Created at">
                            <Typography.Text code>{selectedUrl.createdAt}</Typography.Text>
                        </Descriptions.Item>

                        <Descriptions.Item label="Created by User ID">
                            <Typography.Text code>{selectedUrl.createdByUserId}</Typography.Text>
                        </Descriptions.Item>
                    </Descriptions>
                ) : (
                    <Typography.Text type="secondary">No data available.</Typography.Text>
                )}
            </Modal>
        </Layout>
    );
};

export default ShortURLsPage;
