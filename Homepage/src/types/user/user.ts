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

export function isUserDto(obj: any): obj is IUserDTO {
  return (
    'id' in obj &&
    'emailAddress' in obj &&
    'displayName' in obj &&
    'tag' in obj &&
    'creationDateTime' in obj &&
    'role' in obj &&
    'isEmailVerified' in obj &&
    'isDisplayNameChosen' in obj
  );
}