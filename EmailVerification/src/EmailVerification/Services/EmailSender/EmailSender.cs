using System.Reflection;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using EmailVerification.Config;

namespace EmailVerification.Services;

public class EmailSender : IEmailSender
{
  private readonly IAmazonSimpleEmailService amazonSimpleEmailService;
  private readonly EmailVerificationConfig emailVerificationConfig;

  public EmailSender(IAmazonSimpleEmailService amazonSimpleEmailService, EmailVerificationConfig emailVerificationConfig)
  {
    this.amazonSimpleEmailService = amazonSimpleEmailService;
    this.emailVerificationConfig = emailVerificationConfig;
  }

  public async Task<bool> SendEmailWithVerificationLinkToEmailAddress(string link, string emailAddress)
  {
    Destination destination = new()
    {
      ToAddresses = new List<string>(){ emailAddress }
    };
    Message message = GenerateMessage(link);
    SendEmailRequest request = new()
    {
      Destination = destination,
      Message = message,
      Source = emailVerificationConfig.SenderAddress
    };
    SendEmailResponse response = await amazonSimpleEmailService.SendEmailAsync(request);
    Console.WriteLine($"Sent an email, got a response: {response.HttpStatusCode}");
    return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
  }

  private Message GenerateMessage(string link)
  {
    string html = GetTemplate();
    html = html.Replace("{{verifyUrl}}", link);

    Body body = new()
    {
      Html = new Content()
      {
        Charset = "UTF-8",
        Data = html
      }
    };

    Message message = new()
    {
      Subject = new Content()
      {
        Charset = "UTF-8",
        Data = "Please verify your BircheGames account"
      },
      Body = body
    };

    return message;
  }

  private string GetTemplate()
  {
    return EmailTemplate.Template;
  }

  public Task<string> GetVerificationEmailTemplate()
  {
    return Task.FromResult(GetTemplate());
  }
}