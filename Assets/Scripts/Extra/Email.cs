 using System;
 using System.IO;
 using System.Net;
 using System.Net.Mail;
 using System.Collections;
 using System.Net.Security;
 using System.Security.Cryptography.X509Certificates;
 using UnityEngine;
 
 public class Email : MonoBehaviour {

    private GameData gameData;

    private void Awake() {
        gameData = GameData.FindObjectOfType<GameData>();
    }
    
    public void SendEmail(string fileName) {

        // string filePath = Application.dataPath + "/Resources/ChatLogs/chat20200425.txt";
        string filePath = Application.dataPath + "/Resources/ChatLogs/" + fileName;
        string text = File.ReadAllText(filePath);
        
        MailMessage mail = new MailMessage();

        mail.From = new MailAddress("allianceteam585@gmail.com");
        mail.To.Add("manuelcabrejos97@gmail.com");
        mail.Subject = fileName;
        mail.Body = text + "\n" + gameData.GetFullLog();

        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new System.Net.NetworkCredential("allianceteam585@gmail.com", "changelater585") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback = 
            delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) 
                { return true; };
        smtpServer.Send(mail);
        Debug.Log("success");
    
    }
 }
