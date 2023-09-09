import ICreateUserError from "./createUserError";

export default interface ICreateUserErrorResponse {
  wasSuccess: boolean,
  errors: ICreateUserError[]
}