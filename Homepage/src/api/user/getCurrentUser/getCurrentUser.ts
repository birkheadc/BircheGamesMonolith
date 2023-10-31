import config from "../../../config";
import { ApiResponseBuilder, IApiResponse } from "../../../types/api/apiResponse";
import { IUserDTO, isUserDto } from "../../../types/user/user";

export default async function getCurrentUser(token: string): Promise<IApiResponse<IUserDTO>> {
  const builder = new ApiResponseBuilder<IUserDTO>();

  const url = config.users.apiUrl + "/me";
  if (url == null) {
    return builder.fail()
      .withGeneralError(500, "URL of users api could not be determined.")
      .build();
  }
  const controller = new AbortController();
  const timeout = setTimeout(() => {
    controller.abort();
  }, config.general.apiCallTimeout);

  try {
    const response = await fetch(url, {
      method: 'GET',
      signal: controller.signal,
      headers: {
        'Authorization': `Bearer ${token}`
      }
    });
    if (response.status !== 200) {
      return builder
        .fail()
        .withGeneralError(response.status, "Error connecting to server.")
        .build();
    }
    const data = await response.json();
    if (isUserDto(data) === false) {
      return builder
        .fail()
        .withGeneralError(415, "Data was of unexpected type.")
        .build();
    }
    return builder
      .succeed()
      .withBody(data)
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