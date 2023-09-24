namespace EmailVerification.Services;

public interface IEmailSender
{
  public Task<bool> SendEmailWithVerificationLinkToEmailAddress(string link, string emailAddress);
  public Task<string> GetVerificationEmailTemplate();
}