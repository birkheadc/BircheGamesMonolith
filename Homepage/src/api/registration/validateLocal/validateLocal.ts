import config from "../../../config";
import ICreateUserError from "../../../types/user/newUser/createUserError";
import ICreateUserResponse from "../../../types/user/newUser/createUserResponse";
import INewUser from "../../../types/user/newUser/newUser";

import * as EmailValidator from 'email-validator';
import UserValidator from "../../helpers/userValidator/userValidator";
import { IApiResponse } from "../../../types/api/apiResponse";

export default function validateLocal(user: INewUser): IApiResponse {

  let validator = new UserValidator();

  if (user.password.length > 0) {
    validator = validator.withPassword(user.password, user.repeatPassword);
  }

  if (user.emailAddress.length > 0) {
    validator = validator.withEmailAddress(user.emailAddress);
  }

  const result = validator.validate();

  // If fields are empty, we assume the user hasn't started them yet so we don't validate them individually.
  // But we make sure to fail the final validation if any are empty, so that the submit button will be disabled.
  if (user.emailAddress.length < 1 || user.password.length < 1 || user.repeatPassword.length < 1) {
    result.wasSuccess = false;
  }

  return result;
}