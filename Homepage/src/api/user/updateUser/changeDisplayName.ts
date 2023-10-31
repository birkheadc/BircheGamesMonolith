import api from "../..";
import config from "../../../config";
import { ApiResponseBuilder, IApiResponse } from "../../../types/api/apiResponse";
import { IUpdateDisplayNameRequest } from "../../../types/api/requests/updateUser/updateDisplayNameRequest";

export default async function changeDisplayName(token: string | null, request: IUpdateDisplayNameRequest): Promise<IApiResponse> {
  const builder = new ApiResponseBuilder();

  if (token == null) {
    return builder
      .fail()
      .withGeneralError(401, "Session token not found.")
      .build();
  }

  const url = config.users.apiUrl + "/me/display-name";
  if (url == null) {
    return builder
      .fail()
      .withGeneralError(500, "URL of users api could not be determined.")
      .build();
  }
  
  const { signal, timeout } = api.helpers.createAbortSignal();

  try {
    const response = await fetch(url, {
      method: 'PATCH',
      signal: signal,
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(request)
    });
    const data = await response.json();
    if (response.status !== 200) {
      return builder
        .fail()
        .withGeneralError(response.status, data.detail)
        .build();
    }
    return builder
      .succeed()
      .build();
  } catch {
    return builder
      .fail()
      .withGeneralError(503, "Could not connect to server.")
      .build();
  } finally {
    clearTimeout(timeout);
  }
}