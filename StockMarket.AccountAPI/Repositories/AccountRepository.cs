using StockMarket.AccountAPI.Models;
using System.Linq;
using StockMarket.AccountAPI.DBAccess;
using StockMarket.AccountAPI.Infastructure;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace StockMarket.AccountAPI.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private StockDBContext context;
        private readonly IConfiguration configuration;
        public AccountRepository(StockDBContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }
        public void AddUser(User item)
        {
            context.Add(item);
            context.SaveChanges();
        }
        public User CreateUser(string uname,string password,string email=null,string mobile=null)
        {
            User user = new User();
            user.Username = uname;
            string hashed = encryption.Encrypt(password,uname);
            user.Password = hashed;
            user.Email = email;
            user.Mobile = mobile;
            user.Confirmed = "No";
            user.Role = "User";
            return user;
        }

        public void UpdateUser(int UId, string uname=null, string password=null, string email = null, string mobile = null)
        {
            User user = context.Users.Find(UId);
            if(uname != null)
                user.Username = uname;
            if (password != null)
                user.Password = encryption.Encrypt(password,uname);
            if (email != null)
            {
                user.Email = email;
                user.Confirmed = "No";
            }
            if (mobile != null)
                user.Mobile = mobile;
            context.Update(user);
            context.SaveChanges();
        }

        public User Validate(string uname, string pwd)
        {
            User user = context.Users.SingleOrDefault(i => i.Username == uname);
            if (encryption.Decrypt(user.Password, uname) == pwd)
                return user;
            return null;
        }

        public bool isTaken(string username)
        {
            return context.Users.Where(U => U.Username == username).Any();
        }

        public void ConfirmEmail(User user)
        {
            user.Confirmed = "Yes";
            context.Update(user);
            context.SaveChanges();
        }

        public void ConfirmationEmail(string url,string Email)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(configuration["MailCredentials:Email"]);
            mailMessage.To.Add(Email);
            mailMessage.Subject = "This is your confirmation email";
            mailMessage.Body = url;
            mailMessage.IsBodyHtml = true;

            NetworkCredential networkCredential = new NetworkCredential(configuration["MailCredentials:Email"], configuration["MailCredentials:Password"]);

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = networkCredential;
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
        }
    }
}
