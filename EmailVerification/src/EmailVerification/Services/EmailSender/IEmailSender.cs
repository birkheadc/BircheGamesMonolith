namespace EmailVerification.Services;

public interface IEmailSender
{
  public Task<bool> SendEmailWithVerificationLinkToEmailAddress(string link, string emailAddress);
}