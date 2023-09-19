import config from "../../../config";
import { IGenerateVerificationEmailRequest } from "../../../types/email/generateVerificationEmailRequest";

export default async function requestVerificationEmail(emailAddress: string): Promise<boolean> {
  const url = config.email.apiUrl;
  const frontendUrl = config.general.frontendUrl;
  if (url == null || frontendUrl == null) return false;

  const request: IGenerateVerificationEmailRequest = { frontendUrl: frontendUrl + "/email-verification/verify", emailAddress };

  const controller = new AbortController();
  const timeout = setTimeout(() => {
    controller.abort();
  }, config.general.apiCallTimeout);

  try {
    const response = await fetch(url + '/generate', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(request),
      signal: controller.signal
    });
    return (response.status === 200);
  } catch {
    return false;
  } finally {
    clearTimeout(timeout);
  }
}