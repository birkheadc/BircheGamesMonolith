const registration = {
  apiUrl: process.env.REGISTER_USER_API_URL,
  newUserValidation: {
    passwordMinChars: 8,
    passwordMaxChars: 32,
    displayNameMinChars: 1,
    displayNameMaxChars: 16,
    tagChars: 6
  }
}

export default registration;