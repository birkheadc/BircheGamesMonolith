const registration = {
  apiUrl: process.env.REGISTER_USER_API_URL,
  newUserValidation: {
    passwordMinChars: 8,
    passwordMaxChars: 64
  }
}

export default registration;