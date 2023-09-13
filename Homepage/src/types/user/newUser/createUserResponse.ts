import ICreateUserError from "./createUserError";

export default interface ICreateUserResponse {
  wasSuccess: boolean,
  errors: ICreateUserError[]
}