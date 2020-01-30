export interface HttpError {
    message: any;
    url: string;
    statusCode: number;
    statusText: string;
    handled: boolean;
    isCustomError: boolean;
}
