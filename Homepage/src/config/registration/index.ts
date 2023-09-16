export interface IRegistrationConfig {
  apiUrl: string,
  newUserValidation: {
    passwordMinChars: number,
    passwordMaxChars: number
  }
}

const registration: IRegistrationConfig = {
  apiUrl: 'https://9brx0vetyi.execute-api.ap-southeast-2.amazonaws.com/register',
  newUserValidation: {
    passwordMinChars: 8,
    passwordMaxChars: 64
  }
}

export default registration;