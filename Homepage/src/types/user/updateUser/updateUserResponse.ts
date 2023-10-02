import { IUpdateUserError } from "./updateUserError";

export interface IUpdateUserResponse {
  wasSuccess: boolean,
  errors: IUpdateUserError[]
}