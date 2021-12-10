﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldsHardestGame;

namespace Gyak11
{
    public partial class Form1 : Form
    {
        GameController gc = new GameController();
        GameArea ga;
        Button Btn = new Button();

        int populationSize = 100;
        int nbrOfSteps = 10;
        int nbrOfStepsIncrement = 10;
        int generation = 1;

        Brain winnerBrain = null;
        public Form1()
        {
            InitializeComponent();

            Btn.Text = "start";
            Btn.Top = 331;
            Btn.Left = 331;
            Btn.Width = 80;
            Btn.Height = 80;

            Btn.Visible = false;
            Btn.Enabled = false;
            button1.Visible = false;
            button1.Enabled = false;

            this.Controls.Add(Btn); //valami miatt mindenképp disabled.. Rákerestem, de ott nem volt megoldás.

            ga = gc.ActivateDisplay();
            this.Controls.Add(ga);

            /*
            gc.AddPlayer();
            gc.Start(true);
            */

            gc.GameOver += Gc_GameOver;

            for (int i = 0; i < populationSize; i++)
            {
                gc.AddPlayer(nbrOfSteps);
            }
            gc.Start();

        }

        private void Btn_Click(object sender, EventArgs e)
        {
            gc.ResetCurrentLevel();
            gc.AddPlayer(winnerBrain.Clone());
            gc.AddPlayer();
            ga.Focus();
            gc.Start(true);
        }

        private void Gc_GameOver(object sender)
        {
            generation++;
            label1.Text = string.Format(
                "{0}. generáció",
                generation);
            label1.BringToFront();

            var playerList = from p in gc.GetCurrentPlayers()
                             orderby p.GetFitness() descending
                             select p;
            var topPerformers = playerList.Take(populationSize / 2).ToList();

            gc.ResetCurrentLevel();
            foreach (var p in topPerformers)
            {
                var b = p.Brain.Clone();
                if (generation % 3 == 0)
                    gc.AddPlayer(b.ExpandBrain(nbrOfStepsIncrement));
                else
                    gc.AddPlayer(b);

                if (generation % 3 == 0)
                    gc.AddPlayer(b.Mutate().ExpandBrain(nbrOfStepsIncrement));
                else
                    gc.AddPlayer(b.Mutate());
            }

            var winners = from p in topPerformers
                          where !p.IsWinner
                          select p;

            if (winners.Count() > 0)
            {
                winnerBrain = winners.FirstOrDefault().Brain.Clone();
                gc.GameOver -= Gc_GameOver;

                Btn.Update();
                Application.DoEvents();

                Btn.Visible = true;
                Btn.Enabled = true;

                Btn.Update();
                Application.DoEvents();


                button1.Visible = true;
                button1.Enabled = true;


                Btn.Focus();
                Btn.BringToFront();
                return;
            }
                gc.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gc.ResetCurrentLevel();
            gc.AddPlayer(winnerBrain.Clone());
            gc.AddPlayer();
            ga.Focus();
            gc.Start(true);
        }
    }
}
