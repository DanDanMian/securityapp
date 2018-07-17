using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

public class Email : MonoBehaviour {

    private static string SENDERADDRESS = "yesprojecttest123@gmail.com";
    private static string SENDERPASSWORD = "TestForEmail";

    public static void SendMail(List<string> recipients, List<byte[]> data)
    {
        string content = MetaData.content + " Lat:" + GPS.latitude.ToString() + " Lon:" + GPS.longtitude.ToString();
		string subject = "Sent From " + MetaData.getdisplayname();
        SendMail (recipients, subject, content, data);
    }

    public static void SendMail(List<string> recipients, string subject, string body, List<byte[]> data)
    {
        MailMessage mail = new MailMessage();
		MemoryStream ms;
        mail.From = new MailAddress(SENDERADDRESS);
        for (int i = 0; i < recipients.Count; i++)
            mail.To.Add(recipients[i]);
        mail.Subject = subject;
        mail.Body = body;

        ////////////sending video////////////////////////////////
        if (data.Count == 1)
        {
            ms = new MemoryStream(data[0]);
            mail.Attachments.Add(new Attachment(ms, "video.mp4", "video/mp4"));
            Debug.Log("Video send success!!!!!!!!!!!");
        }
        else
        {
            /////////////////sending photos//////////////////////
            for (int i = 0; i < data.Count; i++)
            {
                ms = new MemoryStream(data[i]);
                mail.Attachments.Add(new Attachment(ms, "photo" + i + ".jpg", "image/jpg"));
            }
        }

		try
		{
			ms = new MemoryStream(GoogleAPI.texture.EncodeToJPG());
			mail.Attachments.Add(new Attachment(ms, "map.jpg", "image/jpg"));
		}
		catch (Exception e)
		{
			Debug.Log(e.Message);
		}


        SmtpClient smtpServer = new SmtpClient();
        smtpServer.Host = "smtp.gmail.com";
        smtpServer.Port = 587;
        smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpServer.Credentials = new System.Net.NetworkCredential(SENDERADDRESS, SENDERPASSWORD) as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        { return true; };
        object userState = mail;
        smtpServer.SendCompleted += new SendCompletedEventHandler(SmtpClient_OnCompleted);
        smtpServer.SendAsync(mail, userState); // Need Callback Confirmation
    }

    public static void SmtpClient_OnCompleted(object sender, AsyncCompletedEventArgs e)
    {
        //Get the Original MailMessage object
        MailMessage mail= (MailMessage)e.UserState;

        //write out the subject
        string subject = mail.Subject;

        if (e.Cancelled)
        {
            Debug.Log("Send canceled for mail with subject " + subject);
        }
        if (e.Error != null)
        {
            Debug.Log("Error occurred when sending mail " + subject + " " + e.Error.ToString());
        }
        else
        {
            Debug.Log("Message sent " + subject );
        }
    }
}