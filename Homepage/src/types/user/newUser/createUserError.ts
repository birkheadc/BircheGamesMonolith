export default interface ICreateUserError {
  field: string,
  statusCode: number,
  errorMessage?: string
}