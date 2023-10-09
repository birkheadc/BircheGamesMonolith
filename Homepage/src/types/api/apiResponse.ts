import { IApiError } from "./apiError";

export interface IApiResponse<T = never> {
  wasSuccess: boolean,
  body?: T,
  errors: IApiError[]
}

export class ApiResponseBuilder<T> {
  apiResponse: IApiResponse<T> = {
    wasSuccess: false,
    body: undefined,
    errors: []
  }

  build(): IApiResponse<T> {
    return this.apiResponse;
  }

  succeed(): ApiResponseBuilder<T> {
    this.apiResponse.wasSuccess = true;
    return this;
  }

  fail(): ApiResponseBuilder<T> {
    this.apiResponse.wasSuccess = false;
    return this;
  }

  withBody(body: T): ApiResponseBuilder<T> {
    this.apiResponse.body = body;
    return this;
  }

  withGeneralError(statusCode: number, errorMessage?: string): ApiResponseBuilder<T> {
    const error: IApiError = {
      statusCode,
      errorMessage
    }
    this.apiResponse.errors.push(error);
    return this;
  }

  withFieldError(field: string, statusCode: number, errorMessage?: string): ApiResponseBuilder<T> {
    const error: IApiError = {
      field,
      statusCode,
      errorMessage
    }
    this.apiResponse.errors.push(error);
    return this;
  }
}