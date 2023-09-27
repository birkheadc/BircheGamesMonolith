import { IUserDTO } from "../../types/user/user";

export default function getUserFromPayload(payload: any): IUserDTO | null {
  if (payload == null || payload.user == null) return null;
  const user: IUserDTO = {
    id: '',
    emailAddress: '',
    displayName: '',
    tag: '',
    creationDateTime: '',
    role: 0,
    isEmailVerified: false,
    isDisplayNameChosen: false
  }
  try {
    Object.entries(user).forEach(([key]) => {
      user[key] = payload.user[ key[0].toUpperCase() + key.substring(1) ];
    });
    return user;
  } catch {
    return null;
  }
}