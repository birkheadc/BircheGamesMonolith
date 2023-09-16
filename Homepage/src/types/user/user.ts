import { IUserRole } from "./role";

export interface IUserDTO {
  id: string,
  emailAddress: string,
  displayName: string,
  tag: string,
  creationDateTime: string,
  role: IUserRole
  isEmailVerified: boolean,
  isDisplayNameChosen: boolean
}