import config from "../../../config";
import { IApiResponse } from "../../../types/api/apiResponse";
import ICreateUserResponse from "../../../types/user/newUser/createUserResponse";
import INewUser from "../../../types/user/newUser/newUser";
import validateLocal from "../validateLocal/validateLocal";

export default async function createUser(user: INewUser): Promise<IApiResponse> {

  const url = config.registration.apiUrl;
  if (url == null) return {
    wasSuccess: false,
    errors: [
      
    ]
  }
  
  const validation = validateLocal(user);
  if (validation.wasSuccess === false) return validation;

  const controller = new AbortController();
  const timeout = setTimeout(() => {
    controller.abort();
  }, config.general.apiCallTimeout);
  try {
    const response: Response = await fetch(
      url,
      {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(user),
        signal: controller.signal
      }
    );

    if (response.status !== 200) {
      let data = await response.json();
      return data;
    }

    return {
      wasSuccess: true,
      errors: []
    };
    
  } catch {
    return {
      wasSuccess: false,
      errors: [
        {
          field: 'none',
          statusCode: 500,
          errorMessage: 'Error connecting to server.'
        }
      ]
    }
  } finally {
    clearTimeout(timeout);
  }
}