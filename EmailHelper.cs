using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Configuration;
using System;

namespace MDM.Common
{
    public class EmailHelper
    {
      
        /// <summary>
        /// 发送系统邮件
        /// </summary>
        public static void SendEmail(string receiverEmail, string title, string body)
        {
            if (string.IsNullOrEmpty(receiverEmail))
            {
                return;
            }
            try
            {
                string MailServerAddress = ConfigurationManager.AppSettings["MailServerAddress"];
                string MailUserName = ConfigurationManager.AppSettings["MailUserName"];
                string MailPassword = ConfigurationManager.AppSettings["MailPassword"];
                string EmailFromName = ConfigurationManager.AppSettings["EmailFromName"];

                string sendEmail = EmailFromName;//发送通知邮箱地址
                string sendEmailPassword = MailPassword;//发送通知邮箱密码
                string sendSmtp = MailServerAddress;//发送通知邮箱smtp
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                msg.To.Add(receiverEmail);
                msg.From = new System.Net.Mail.MailAddress(sendEmail, MailUserName, System.Text.Encoding.UTF8);
                msg.Subject = title;//邮件标题
                msg.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码
                msg.Body = body;//邮件内容
                msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码
                msg.IsBodyHtml = true;//是否是HTML邮件
                msg.Priority = System.Net.Mail.MailPriority.High;//邮件优先
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                client.Credentials = new System.Net.NetworkCredential(sendEmail, sendEmailPassword);
                client.Host = sendSmtp;
                client.Port = 25;
                client.Send(msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 异步发送系统邮件
        /// </summary>
        public static void SendEmailAsyn(string receiverEmail, string title, string body)
        {
            if (string.IsNullOrEmpty(receiverEmail))
            {
                return;
            }
            try
            {
              
                string MailServerAddress = ConfigurationManager.AppSettings["MailServerAddress"];
                string MailUserName = ConfigurationManager.AppSettings["MailUserName"];
                string MailPassword = ConfigurationManager.AppSettings["MailPassword"];
                string EmailFromName = ConfigurationManager.AppSettings["EmailFromName"];

                string sendEmail = EmailFromName;//发送通知邮箱地址
                string sendEmailPassword = MailPassword;//发送通知邮箱密码
                string sendSmtp = MailServerAddress;//发送通知邮箱smtp
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                msg.To.Add(receiverEmail);
                msg.From = new System.Net.Mail.MailAddress(sendEmail, MailUserName, System.Text.Encoding.UTF8);
                msg.Subject = title;//邮件标题
                msg.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码
                msg.Body = body;//邮件内容
                msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码
                msg.IsBodyHtml = true;//是否是HTML邮件
                msg.Priority = System.Net.Mail.MailPriority.High;//邮件优先
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                client.Credentials = new System.Net.NetworkCredential(sendEmail, sendEmailPassword);
                client.Host = sendSmtp;
                //client.Port = 25;
                client.Port = 587;
                client.EnableSsl = true;//加密连接
                client.SendMailAsync(msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		 /// <summary>
        /// 打招呼
        /// </summary>
		public static void SayHello(){
		 Console.WriteLine("Hello");
		}
		 /// <summary>
        /// 向某人打招呼
        /// </summary>
		public static void SayHelloTo(string name){
		 Console.WriteLine("Hello "+name);
		}
		
		 /// <summary>
        /// 唱歌方法
        /// </summary>
		public static void SingASong(){
		 Console.WriteLine("Sing");
		}
		
		//todo:增加一个方法
		
		//todo:再增加一个方法
		
	    //新分支方法
    }
}