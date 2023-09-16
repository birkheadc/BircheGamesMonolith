export interface ILoginResponse {
  wasSuccess: boolean,
  error?: string | undefined,
  sessionToken?: string | undefined
}