import axios from "axios";
import type { GeneralResultModel } from "../Models/GeneralResultModel/GeneralResultModel";
import type { ShortUrlAddModel } from "../Models/ShortUrl/ShortUrlAddModel";
import type { ShortUrlModel } from "../Models/ShortUrl/ShortUrlModel";

const API_BASE_URL = import.meta.env.VITE_ASPNETCORE_API_URL;

export const getAllShortUrls = async (): Promise<ShortUrlModel[]> => {
    const response = await axios.get<GeneralResultModel<ShortUrlModel[]>>(`${API_BASE_URL}/api/ShortUrls/GetAll`);

    const { result, errors, hasErrors } = response.data;

    if (hasErrors || !result) {
        throw new Error(errors?.join(", ") || "Unknown server error");
    }

    return result;
};

export const addShortUrl = async (urlData: ShortUrlAddModel, token: string): Promise<ShortUrlModel> => {
    const response = await axios.post<GeneralResultModel<ShortUrlModel>>(
        `${API_BASE_URL}/api/ShortUrls/AddUrl`,
        urlData,
        {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`,
            },
        }
    );

    const { result, errors, hasErrors } = response.data;

    if (hasErrors || !result) {
        throw new Error(errors?.join(", ") || "Unknown server error");
    }

    return result;
};

