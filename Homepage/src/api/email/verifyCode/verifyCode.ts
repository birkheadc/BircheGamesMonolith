import config from "../../../config";

export default async function verifyCode(code: string | null): Promise<{ wasSuccess: boolean, error?: string | undefined}> {
  if (code == null) return { wasSuccess: false, error: "Code not found." };
  const url = config.email.apiUrl
  if (url == null) return { wasSuccess: false, error: "Email Verification API URL not found."};

  const controller = new AbortController();
  const timeout = setTimeout(() => {
    controller.abort();
  }, config.general.apiCallTimeout);

  try {
    const response = await fetch(url + "/verify", {
      method: 'POST',
      body: JSON.stringify({ verificationCode: code}),
      headers: {
        'Content-Type': 'application/json'
      },
      signal: controller.signal
    });
    if (response.status === 200) {
      return { wasSuccess: true }
    }
    return { wasSuccess: false, error: response.statusText };
  } catch {
    return { wasSuccess: false, error: "Error connecting to server. " };
  } finally {
    clearTimeout(timeout);
  }
}