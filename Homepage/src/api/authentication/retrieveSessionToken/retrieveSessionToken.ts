import config from "../../../config";
import { ICredentials } from "../../../types/credentials/credentials";
import { ILoginResponse } from "../../../types/sessionToken/loginResponse";
import { ISessionTokenWrapper } from "../../../types/sessionToken/sessionTokenWrapper";

export default async function retrieveSessionToken(credentials: ICredentials): Promise<ILoginResponse> {
  const url = config.authentication.apiUrl;
  if (url == null) return {
    wasSuccess: false,
    error: 'Could not determine authentication api url.'
  }

  const controller = new AbortController();
  const timeout = setTimeout(() => {
    controller.abort();
  }, config.general.apiCallTimeout);

  try {
    const response = await fetch(url, {
      method: 'POST',
      headers: {
        'Authorization': credentialsToBasic(credentials)
      },
      signal: controller.signal
    });

    if (response.status === 200) {
      try {
        const data = await response.json() as ISessionTokenWrapper;
        return {
          wasSuccess: true,
          sessionToken: data.token
        };
      } catch {
        return {
          wasSuccess: false,
          error: 'Error retrieving session token. Data was in an unexpected format.'
        }
      }
    }

    if (response.status === 401) {
      return {
        wasSuccess: false,
        error: 'Credentials were incorrect. Please try again.'
      }
    }

    return {
      wasSuccess: false,
      error: response.statusText
    }

  } catch {
    return {
      wasSuccess: false,
      error: 'Could not connect to the server.'
    }
  } finally {
    clearTimeout(timeout);
  }
}

function credentialsToBasic(credentials: ICredentials): string {
  return btoa(`${credentials.emailAddress}:${credentials.password}`);
}