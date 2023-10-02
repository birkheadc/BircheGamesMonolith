import { IUpdateUserRequest } from "../../../types/user/updateUser/updateUserRequest";
import { IUpdateUserResponse } from "../../../types/user/updateUser/updateUserResponse";

export default async function updateUser(request: IUpdateUserRequest): Promise<IUpdateUserResponse> {
  const response: IUpdateUserResponse = {
    wasSuccess: false,
    errors: []
  };
  return response;
}