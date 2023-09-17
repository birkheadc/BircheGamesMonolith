namespace EmailVerification.Services;

public interface IEmailService
{
  public void SendEmailForEmailAddressIfExists(string emailAddress);
  public Task<bool> ProcessVerificationCode(string code);
}