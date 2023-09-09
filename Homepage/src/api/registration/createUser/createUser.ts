import ICreateUserErrorResponse from "../../../types/user/newUser/createUserResponse";
import INewUser from "../../../types/user/newUser/newUser";
import INewUserValidationConfig from "../../../types/user/newUser/newUserValidationConfig";

export default async function createUser(user: INewUser): Promise<ICreateUserErrorResponse | void> {
  // Todo: Get this from env
  const validationConfig: INewUserValidationConfig = {
    displayNameRegex: "^[a-zA-Z0-9]{1,32}#[a-zA-Z0-9]{6}$",
    passwordMinChars: 8,
    passwordMaxChars: 64
  }

  // Todo: Get this from env
  const url = 'http://localhost:5000/register';

  const validation = validateLocal(user, validationConfig);
  if (validation.wasSuccess === false) return validation;

  const controller = new AbortController();

  try {
    let response: Response = await fetch(
      url,
      {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
      }
    );

    if (response.status !== 200) {
      try {
        let data = await response.json() as ICreateUserErrorResponse;
        return data;
      } catch {
        return {
          wasSuccess: false,
          errors: [
            {
              field: 'none',
              statusCode: 500,
              errorMessage: 'Unexpected response format.'
            }
          ]
        }
      }
    }

    return;
    
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
  }
}

function validateLocal(user: INewUser, config: INewUserValidationConfig): ICreateUserErrorResponse {
  const response: ICreateUserErrorResponse = {
    wasSuccess: true,
    errors: []
  }

  // Todo: Validate

  return response;
}