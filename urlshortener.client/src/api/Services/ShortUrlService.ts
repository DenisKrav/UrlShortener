import axios from "axios";
import type { GeneralResultModel } from "../Models/GeneralResultModel/GeneralResultModel";
import type { ShortUrlAddModel } from "../Models/ShortUrl/ShortUrlAddModel";
import type { ShortUrlModel } from "../Models/ShortUrl/ShortUrlModel";
import type { DeleteShortUrlModel } from "../Models/ShortUrl/DeleteShortUrlModel";

const API_BASE_URL = import.meta.env.VITE_ASPNETCORE_API_URL;

export const getAllShortUrls = async (): Promise<ShortUrlModel[]> => {
    const response = await axios.get<GeneralResultModel<ShortUrlModel[]>>(`${API_BASE_URL}/api/ShortUrls/GetAll`);

    console.log("Response from getAllShortUrls:", response.data);

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

export const deleteShortUrl = async (payload: DeleteShortUrlModel, token: string): Promise<boolean> => {
    const response = await axios.delete<GeneralResultModel<boolean>>(
        `${API_BASE_URL}/api/ShortUrls/Delete`,
        {
            data: payload,
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`,
            },
        }
    );

    const { result, errors, hasErrors } = response.data;

    if (hasErrors || result !== true) {
        throw new Error(errors?.join(", ") || "Failed to delete the short URL");
    }

    return result;
};