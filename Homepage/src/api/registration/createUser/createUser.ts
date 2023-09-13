import ICreateUserResponse from "../../../types/user/newUser/createUserResponse";
import INewUser from "../../../types/user/newUser/newUser";
import INewUserValidationConfig from "../../../types/user/newUser/newUserValidationConfig";
import validateLocal from "../validateLocal/validateLocal";

export default async function createUser(user: INewUser): Promise<ICreateUserResponse> {
  // Todo: Get this from env
  const url = 'http://localhost:5000/register';

  const validation = validateLocal(user);
  if (validation.wasSuccess === false) return validation;

  const controller = new AbortController();
  const timeout = setTimeout(() => {
    controller.abort();
  }, 8000);
  try {
    let response: Response = await fetch(
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
      try {
        let data = await response.json() as ICreateUserResponse;
        return data;
      } catch {
        return {
          wasSuccess: false,
          errors: [
            {
              field: '',
              statusCode: 500,
              errorMessage: 'Unexpected response format.'
            }
          ]
        }
      }
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