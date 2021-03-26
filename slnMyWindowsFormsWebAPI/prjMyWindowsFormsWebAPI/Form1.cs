using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net; //WebRequest
using System.IO; //Stream
using System.Web.Script.Serialization; //JavaScriptSerializer

namespace prjMyWindowsFormsWebAPI
{
    public partial class Form1 : Form
    {
        List<string> methodsList = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            methodsList.Add("GET");
            methodsList.Add("POST");
            methodsList.Add("PUT");
            methodsList.Add("DELETE");

            foreach (string item in methodsList) 
            {
                cboxMethods.Items.Add(item);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {            
            WebRequest request = WebRequest.Create(txtURL.Text);
            request.ContentType = "application/json";

            if (cboxMethods.SelectedIndex == 0) //GET
            {
                request.Method = "GET";
                request.ContentType = "application/json";
                var response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);

                var getResult = reader.ReadToEnd();
                foreach (var item in getResult)
                {
                    txtResult.Text += item;
                }

                reader.Close();
                stream.Close();
                response.Close();
            }
            else if (cboxMethods.SelectedIndex == 1) //POST
            {
                request.Method = "POST";
                request.ContentType = "application/json";

                var streamWriter = new StreamWriter(request.GetRequestStream());
                streamWriter.Write(txtInput.Text);
                streamWriter.Flush();
                streamWriter.Close();

                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                var result = reader.ReadToEnd();
                foreach (var item in result)
                {
                    txtResult.Text += item;
                }

                response.Close();
                reader.Close();
            }
            else if (cboxMethods.SelectedIndex == 2) //PUT
            {
                request.Method = "PUT";
                request.ContentType = "application/json";

                var stremWriter = new StreamWriter(request.GetRequestStream());
                stremWriter.Write(txtInput.Text);
                stremWriter.Flush();
                stremWriter.Close();

                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                txtResult.Text = reader.ReadToEnd();

                response.Close();
                reader.Close();
            }
            else if (cboxMethods.SelectedIndex == 3) 
            {
                request.Method = "DELETE";
                request.ContentType = "application/json";

                WebResponse response = request.GetResponse();
                var reader = new StreamReader(response.GetResponseStream());

                txtResult.Text = reader.ReadToEnd();

                response.Close();
                reader.Close();
            }

        }
    }
}
