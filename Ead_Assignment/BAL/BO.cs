using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public static class BO
    {
        public static UserPoko getUserPoko(int userId)
        {
            return DO.getUserPokoById(userId);
        }

        public static UserPoko getUserPokoByEmail(String email)
        {
            return DO.getUserPokoByEmail(email);
        }

        public static int validateAdmin(AdminPoko admin)
        {
            return DO.validateAdmin(admin);
        }

        public static int Save(UserPoko user)
        {
            return DO.Save(user);
        }

        public static UserPoko validateUser(String login, String password, String email, int uid, int flag)
        {
            return DO.validateUser(login, password, email, uid, flag);
        }

        public static int validateUserWithEmail(String email)
        {
            int count = DO.validateUserWithEmail(email);
            return count;
        }

        public static DataTable getAllUsersPoko()
        {
            DataTable table = DO.getAllUsersPoko();
            return table;
        }

        public static bool sendEmail(String toEmailAddress, String subject, String body)
        {
            try
            {

                var fromAddress = new MailAddress("EAD.SEMorning@gmail.com");
                var fromPassword = "SEMorining2017";


                var toAddress = new MailAddress(toEmailAddress);

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })

                    smtp.Send(message);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public static int RandomCodeGenerator()
        {
            int randomValue;
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                byte[] val = new byte[4];
                crypto.GetBytes(val);
                randomValue = BitConverter.ToInt16(val, 0);
                randomValue = System.Math.Abs(randomValue);
            }
            return randomValue;
        }
    }
}
