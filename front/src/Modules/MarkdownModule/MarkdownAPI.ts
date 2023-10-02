import axios, {AxiosResponse} from "axios";

export interface MarkdownRequest{
    text: string;
}

export interface MarkdownResponse{
    text: string;
}

export async function ProcessText(text: string): Promise<AxiosResponse<MarkdownResponse>>{
    return await axios.post<MarkdownResponse>("/Markdown");
}