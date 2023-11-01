import config from "../../../config";
import { IApiError } from "../../../types/api/apiError";
import { IApiResponse } from "../../../types/api/apiResponse";

import * as EmailValidator from 'email-validator';

export default class UserValidator {
  errors: IApiError[] = [];

  withEmailAddress(emailAddress: string): UserValidator {
    if (EmailValidator.validate(emailAddress) === false) {
      const error: IApiError = {
        field: "emailAddress",
        statusCode: 422,
        errorMessage: "Email Address format is invalid."
      }
      this.errors.push(error);
    }
    return this;
  }

  withPassword(password: string, repeatPassword?: string | undefined): UserValidator {
    if (repeatPassword != null && password !== repeatPassword) {
      const error: IApiError = {
        field: "repeatPassword",
        statusCode: 422,
        errorMessage: "Password is not the same in both fields."
      }
      this.errors.push(error);
    }
    if (password.length < config.registration.newUserValidation.passwordMinChars) {
      const error: IApiError = {
        field: "password",
        statusCode: 422,
        errorMessage: "Password is too short."
      }
      this.errors.push(error);
    }
    if (password.length > config.registration.newUserValidation.passwordMaxChars) {
      const error: IApiError = {
        field: "password",
        statusCode: 422,
        errorMessage: "Password is too long."
      }
      this.errors.push(error);
    }

    return this;
  }

  withDisplayName(displayName: string): UserValidator {
    if (displayName.length < config.registration.newUserValidation.displayNameMinChars) {
      const error: IApiError = {
        field: "displayName",
        statusCode: 422,
        errorMessage: "Display name is too short."
      }
      this.errors.push(error);
    }
    if (displayName.length > config.registration.newUserValidation.displayNameMaxChars) {
      const error: IApiError = {
        field: "displayName",
        statusCode: 422,
        errorMessage: "Display name is too long."
      }
      this.errors.push(error);
    }
    return this;
  }

  withTag(tag: string): UserValidator {
    if (tag.length !== config.registration.newUserValidation.tagChars) {
      const error: IApiError = {
        field: "tag",
        statusCode: 422,
        errorMessage: "Tag must be exactly 6 characters."
      }
      this.errors.push(error);
    }
    return this;
  }

  validate(): IApiResponse {
    return {
      wasSuccess: this.errors.length === 0,
      errors: this.errors
    }
  }
}