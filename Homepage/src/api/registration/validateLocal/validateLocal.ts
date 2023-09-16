import config from "../../../config";
import ICreateUserError from "../../../types/user/newUser/createUserError";
import ICreateUserResponse from "../../../types/user/newUser/createUserResponse";
import INewUser from "../../../types/user/newUser/newUser";

import * as EmailValidator from 'email-validator';

export default function validateLocal(user: INewUser): ICreateUserResponse {

  const validationConfig = config.registration.newUserValidation;

  let wasSuccess = true;
  const errors: ICreateUserError[] = [];

  if (user.password.length > 0 && user.password.length < validationConfig.passwordMinChars) {
    wasSuccess = false;
    const error: ICreateUserError = {
      field: "password",
      statusCode: 422,
      errorMessage: "Password is too short."
    }
    errors.push(error);
  }

  if (user.password.length > validationConfig.passwordMaxChars) {
    wasSuccess = false;
    const error: ICreateUserError = {
      field: "password",
      statusCode: 422,
      errorMessage: "Password is too long."
    }
    errors.push(error);
  }

  if (user.password !== user.repeatPassword) {
    wasSuccess = false;
    const error: ICreateUserError = {
      field: "repeatPassword",
      statusCode: 422,
      errorMessage: "Password is not the same in both fields."
    }
    errors.push(error);
  }

  if (user.emailAddress.length > 0 && EmailValidator.validate(user.emailAddress) === false) {
    wasSuccess = false;
    const error: ICreateUserError = {
      field: "emailAddress",
      statusCode: 422,
      errorMessage: "Email Address format is invalid."
    }
    errors.push(error);
  }

  if (user.emailAddress.length < 1 || user.password.length < 1 || user.repeatPassword.length < 1) {
    wasSuccess = false;
  }

  return {
    wasSuccess,
    errors
  };
}