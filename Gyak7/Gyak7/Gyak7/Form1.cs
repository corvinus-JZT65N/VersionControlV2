using Gyak7.Entities;
using Gyak7.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gyak7
{
    public partial class Form1 : Form
    {

        List<RateData> Rates = new List<RateData>();

        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = Rates.ToList();
            Feladat3();
            
        }

        private void Feladat3()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };

            var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;
            Console.WriteLine(result);
        }
    }
}
