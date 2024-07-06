using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace ditmecuocdoi
{
    public partial class IMPORT : Form
    {
        DataTable dt = new DataTable();

        DataTable dt1 = new DataTable();

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "gYPlphKzYhj9TAXdiYityrtupwO9QwNLlFr4zoSU",
            BasePath = "https://aidigitalsignage-7f781-default-rtdb.firebaseio.com/",
        };
        IFirebaseClient client;
        public IMPORT()
        {
            InitializeComponent();
        }

        private void IMPORT_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            if (client != null)
            {
                MessageBox.Show("Connect is established");
            }

            dt.Columns.Add("id");
            dt.Columns.Add("name");
            dt.Columns.Add("minage");
            dt.Columns.Add("maxage");
            dt.Columns.Add("gender");
            dt.Columns.Add("link");

            dataGridView1.DataSource = dt;


            dt1.Columns.Add("idss");
            dt1.Columns.Add("namess");
            dt1.Columns.Add("linkss");

            dataGridView2.DataSource = dt1;
        }


        private void ClearFields()
        {
            textID.Clear();
            textName.Clear();
            textMinAge.Clear();
            textMaxAge.Clear();
            comboBoxGender.SelectedIndex = -1;
            textLink.Clear();
        }

        private void ClearFields1()
        {
            textIDss.Clear();
            textNamess.Clear();
            textLinkss.Clear();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            int minAge, maxAge;
            if (int.TryParse(textMinAge.Text, out minAge) && int.TryParse(textMaxAge.Text, out maxAge))
            {
                if (minAge < maxAge)
                {
                    var data = new Data
                    {
                        ID = textID.Text,
                        Name = textName.Text,
                        MinAge = minAge,
                        MaxAge = maxAge,
                        Gender = comboBoxGender.SelectedItem.ToString(),
                        Link = textLink.Text,
                    };
                    SetResponse response = await client.SetAsync("R_ads/" + textID.Text, data);
                    Data result = response.ResultAs<Data>();

                    MessageBox.Show("Data added " + result.ID);
                }
                else
                {
                    MessageBox.Show("MinAge phải nhỏ hơn MaxAge.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập số hợp lệ cho MinAge và MaxAge.");
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            int minAge, maxAge;
            if (int.TryParse(textMinAge.Text, out minAge) && int.TryParse(textMaxAge.Text, out maxAge))
            {
                if (minAge < maxAge)
                {
                    var data = new Data
                    {
                        ID = textID.Text,
                        Name = textName.Text,
                        MinAge = minAge,  // Chuyển đổi giá trị int thành string
                        MaxAge = maxAge,  // Chuyển đổi giá trị int thành string
                        Gender = comboBoxGender.SelectedItem.ToString(),
                        Link = textLink.Text,
                    };
                    FirebaseResponse response = await client.UpdateAsync("R_ads/" + textID.Text, data);
                    Data result = response.ResultAs<Data>();

                    MessageBox.Show("Data updated at ID " + result.ID);
                }
                else
                {
                    MessageBox.Show("MinAge phải nhỏ hơn MaxAge.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập số hợp lệ cho MinAge và MaxAge.");
            }


        }

        private async void btRetrive_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.GetAsync("R_ads/" + textID.Text);
            Data result = response.ResultAs<Data>();

            textID.Text = result.ID;
            textName.Text = result.Name;

            textMinAge.Text = result.MinAge.ToString();
            textMaxAge.Text = result.MaxAge.ToString();
            comboBoxGender.SelectedItem = result.Gender;
            textLink.Text = result.Link;

            MessageBox.Show("Data Retrieved successfully");
        }

        private async void btDelete_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.DeleteAsync("R_ads/" + textID.Text);
            MessageBox.Show("Delete record of ID: " + textID.Text);


        }

        private async void btDeleteAll_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.DeleteAsync("R_ads/");
            MessageBox.Show("All Elements Deleted / R_ads node has been deleted");
        }

        private async void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                FirebaseResponse response = await client.GetAsync("R_ads/");
                if (response != null && response.Body != "null")
                {
                    Dictionary<string, Data> dataDict = response.ResultAs<Dictionary<string, Data>>();

                    if (dataDict != null)
                    {
                        List<Data> dataList = dataDict.Values.ToList();
                        dataGridView1.DataSource = dataList;
                        MessageBox.Show("Data loaded successfully");
                    }
                    else
                    {
                        MessageBox.Show("No data found in Firebase.");
                    }
                }
                else
                {
                    MessageBox.Show("No response from Firebase or data does not exist.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void textID_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button6_Click(object sender, EventArgs e)
        {
            var data1 = new Data1
            {
                IDss = textIDss.Text,
                Namess = textNamess.Text,
                Linkss = textLinkss.Text,
            };
            SetResponse response = await client.SetAsync("ads/" + textIDss.Text, data1);
            Data1 result = response.ResultAs<Data1>();

            MessageBox.Show("Data added " + result.IDss);

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.DeleteAsync("ads/" + textIDss.Text);
            MessageBox.Show("Delete record of IDss: " + textIDss.Text);
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            var data1 = new Data1
            {
                IDss = textIDss.Text,
                Namess = textNamess.Text,
                Linkss = textLinkss.Text,
            };
            FirebaseResponse response = await client.UpdateAsync("ads/" + textIDss.Text, data1);
            Data1 result = response.ResultAs<Data1>();

            MessageBox.Show("Data updated at IDss " + result.IDss);
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.GetAsync("ads/" + textIDss.Text);
            Data1 result = response.ResultAs<Data1>();

            textIDss.Text = result.IDss;
            textNamess.Text = result.Namess;
            textLinkss.Text = result.Linkss;

            MessageBox.Show("Data Retrieved successfully");
        }

        private async void button2_Click_2(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.DeleteAsync("ads/");
            MessageBox.Show("All Elements Deleted / ads node has been deleted");
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                FirebaseResponse response = await client.GetAsync("ads/");
                if (response != null && response.Body != "null")
                {
                    Dictionary<string, Data1> dataDict = response.ResultAs<Dictionary<string, Data1>>();

                    if (dataDict != null)
                    {
                        List<Data1> dataList = new List<Data1>(dataDict.Values);
                        dataGridView2.DataSource = dataList;
                        MessageBox.Show("Data loaded successfully");
                    }
                    else
                    {
                        MessageBox.Show("No data found in Firebase.");
                    }
                }
                else
                {
                    MessageBox.Show("No response from Firebase or data does not exist.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ClearFields1();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Name_Click(object sender, EventArgs e)
        {

        }
    }

}