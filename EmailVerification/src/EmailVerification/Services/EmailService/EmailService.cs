using EmailVerification.Repositories;

namespace EmailVerification.Services;

public class EmailService : IEmailService
{
  private readonly IUserRepository userRepository;

  public EmailService(IUserRepository userRepository)
  {
    this.userRepository = userRepository;
  }

  public Task<bool> ProcessVerificationCode(string code)
  {
    throw new NotImplementedException();
  }

  public void SendEmailForEmailAddressIfExists(string emailAddress)
  {
    throw new NotImplementedException();
  }
}