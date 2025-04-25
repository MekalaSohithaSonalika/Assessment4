using System;
using System.Drawing;
using System.Windows.Forms;

namespace AlarmClockWinForms2
{
    public partial class Form1 : Form
    {
        private DateTime alarmTime;
        private bool isAlarmSet = false;
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!isAlarmSet)
            {
                if (DateTime.TryParseExact(txtAlarmTime.Text, "HH:mm:ss", 
                    null, System.Globalization.DateTimeStyles.None, out alarmTime))
                {
                    alarmTime = DateTime.Today.Add(alarmTime.TimeOfDay);
                    
                    if (alarmTime < DateTime.Now)
                    {
                        alarmTime = alarmTime.AddDays(1);
                    }

                    isAlarmSet = true;
                    btnStart.Text = "Stop";
                    timer1.Start();
                }
                else
                {
                    MessageBox.Show("Please enter time in valid HH:MM:SS format", "Invalid Time");
                }
            }
            else
            {
                isAlarmSet = false;
                btnStart.Text = "Start";
                timer1.Stop();
                this.BackColor = SystemColors.Control;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            
            if (DateTime.Now >= alarmTime)
            {
                timer1.Stop();
                isAlarmSet = false;
                btnStart.Text = "Start";
                MessageBox.Show("ALARM! Time's up!", "Alarm");
                this.BackColor = SystemColors.Control;
            }
        }
    }
}
