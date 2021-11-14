using Gyak8.Abstractions;
using Gyak8.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gyak8
{
    public partial class Form1 : Form
    {
        List<Toy> _toys = new List<Toy>();

        private IToyFactory _factory;

        public IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }

        public Form1()
        {
            InitializeComponent();
            Factory = new CarFactory();
            mainPanel.Width = Width;
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var toy = Factory.CreateNew();
            _toys.Add(toy);
            toy.Left = -toy.Width;
            mainPanel.Controls.Add(toy);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            int maxPosition = 0;
            foreach (var item in _toys)
            {
                item.MoveToy();
                if (item.Left > maxPosition)
                {
                    maxPosition = item.Left;
                }
            }

            if (maxPosition > 1000)
            {
                var oldestBall = _toys[0];
                mainPanel.Controls.Remove(oldestBall);
                _toys.Remove(oldestBall);
            }
        }
    }
}
