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
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace Gyak7
{
    public partial class Form1 : Form
    {

        List<RateData> Rates = new List<RateData>();
        BindingList<string> Currencies = new BindingList<string>();
        

        public Form1()
        {
            InitializeComponent();
            AvailableCurrencies();
            RefreshData();
        }

        private void AvailableCurrencies()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetCurrenciesRequestBody();

            var response = mnbService.GetCurrencies(request);
            var result = response.GetCurrenciesResult;

            Console.WriteLine(result);

            var xml = new XmlDocument();
            xml.LoadXml(result);


            foreach (XmlElement element in xml.DocumentElement.ChildNodes[0])
            {
                string newItem = element.InnerText;
                Currencies.Add(newItem);
            }

            comboBox1.DataSource = Currencies;
        }

        private void RefreshData()
        {
            Rates.Clear();
            Feladat3(comboBox1.Text, dateTimePicker1.Value, dateTimePicker2.Value);
            Feladat5();
            Feladat6();
            dataGridView1.DataSource = Rates.ToList();
            chartRateData.DataSource = Rates;
        }

        private void Feladat6()
        {
            chartRateData.DataSource = Rates;

            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chartRateData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }

        private void Feladat5()
        {
            var xml = new XmlDocument();
            xml.LoadXml(Feladat3(comboBox1.Text, dateTimePicker1.Value, dateTimePicker2.Value));


            foreach (XmlElement element in xml.DocumentElement)
            {
                var rate = new RateData();
                Rates.Add(rate);

                rate.Date = DateTime.Parse(element.GetAttribute("date"));

                var childElement = (XmlElement)element.ChildNodes[0];
                if (childElement == null)
                    continue;
                rate.Currency = childElement.GetAttribute("curr");

                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rate.Value = value / unit;
            }
        }

        private string Feladat3(string currencyNameInput, DateTime startDateInput, DateTime EndDateInput)
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = currencyNameInput,
                startDate = startDateInput.ToString(),
                endDate = EndDateInput.ToString()
            };

            var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;

            Console.WriteLine(result);

            return result;
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
