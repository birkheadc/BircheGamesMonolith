using EmailVerification.Models;
using EmailVerification.Repositories;
using EmailVerification.Config;
using Domain.Models;

namespace EmailVerification.Services;

public class EmailService : IEmailService
{
  private readonly IUserRepository userRepository;
  private readonly ITokenGenerator tokenGenerator;
  private readonly ITokenValidator tokenValidator;
  private readonly IEmailSender emailSender;

  public EmailService(IUserRepository userRepository, ITokenGenerator tokenGenerator, ITokenValidator tokenValidator, IEmailSender emailSender)
  {
    this.userRepository = userRepository;
    this.tokenGenerator = tokenGenerator;
    this.tokenValidator = tokenValidator;
    this.emailSender = emailSender;
  }

  public async Task<bool> ProcessVerificationCode(string code)
  {
    string? emailAddress = tokenValidator.ValidateToken(code);
    if (emailAddress is null) return false;
    bool wasSuccess = await UpdateUserVerifyEmailAddress(emailAddress);
    return wasSuccess;
  }

  private async Task<bool> UpdateUserVerifyEmailAddress(string emailAddress)
  {
    UserEntity? user = await userRepository.GetUserByEmailAddress(emailAddress);
    if (user is null) return false;

    user.IsEmailVerified = true;
    bool wasSuccess = await userRepository.UpdateUser(user);
    return wasSuccess;
  }

  public void ProcessGenerateRequest(GenerateVerificationEmailRequest request)
  {
    string verifyLink = GenerateVerifyLinkFromRequest(request);
    emailSender.SendEmailWithVerificationLinkToEmailAddress(verifyLink, request.EmailAddress);
  }

  private string GenerateVerifyLinkFromRequest(GenerateVerificationEmailRequest request)
  {
    return $"{request.FrontendUrl}?code={GenerateVerifyLinkCode(request.EmailAddress)}";
  }

  private string GenerateVerifyLinkCode(string emailAddress)
  {
    return tokenGenerator.GenerateTokenForEmailAddress(emailAddress);
  }
}