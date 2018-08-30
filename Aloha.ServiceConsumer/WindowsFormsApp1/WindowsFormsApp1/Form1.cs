using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private string username = "aloha_user";
        private string password = "Welcome01";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var service = new ServiceReference1.AlohaServiceClient();
            service.ClientCredentials.UserName.UserName = username;
            service.ClientCredentials.UserName.Password = password;

            // This is because IIS on our Amazon web server does not have valid SSL sertificate (there is self-signed sertificate)
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };

            // example of service consuing:
            var airCompanies = service.GetAirCompanyList();

            this.textBox1.Text = string.Empty;

            foreach(var airCompany in airCompanies.Result)
            {
                this.textBox1.Text += string.Format("{0} {1}", airCompany.Id, airCompany.Name);
                this.textBox1.Text += Environment.NewLine;
            }
        }
    }
}
