import config from "../../../config";
import { ICredentials } from "../../../types/credentials/credentials";
import { ILoginResponse } from "../../../types/sessionToken/loginResponse";

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
      }
    })
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