import { IUserDTO } from "../user/user";

export interface ISessionTokenPayload {
  user: IUserDTO,
  exp: number,
  iat: number,
  nbf: number,
  iss: string,
  aud: string,
}