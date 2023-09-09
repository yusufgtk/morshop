using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace UseApi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            var productList = new List<ProductDTO>();
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://localhost:7000/api/product");
            var apiResponse=await response.Content.ReadAsStringAsync();
            productList = JsonConvert.DeserializeObject<List<ProductDTO>>(apiResponse);
            foreach (var product in productList)
            {
                dataGridView1.Rows.Add(product.Name,product.Description,product.CurrentPrice,product.Url);
            }

        }
    }
}
