using System;
using System.Diagnostics.CodeAnalysis;
using System.Media;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CookieClicker
{
    public partial class Form1 : Form
    {
        private ulong cookie = 25;
        private ulong cookie2 = 2500;
        private ulong cookie4 = 25000;
        private ulong cookie3 = 250000;
        private ulong cookie5 = 2500000;
        
        private ulong cookiesClicked = 0;
        private ulong cookieAmount = 1;
        private const string SaveFilePath = "saveData.txt";
        SoundPlayer player = new SoundPlayer("../CookieClicker/vineboom.wav");
        public Form1()
        {
            InitializeComponent();
            LoadGameData();
            this.FormClosing += Form1_FormClosing;
            StartButtonUpdater();
        }
        


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private async void pictureBox1_Click_1(object sender, EventArgs e)
        {
            cookiesClicked += cookieAmount;
            string CookieClicked = cookiesClicked.ToString();
            label2.Text = CookieClicked;
            MouseEventArgs me = (MouseEventArgs)e;      
            Point clickPosition = me.Location;

            ShowClickEffect(clickPosition);

            var originalSize = pictureBox1.Size;
            var originalLocation = pictureBox1.Location;

            pictureBox1.Size = new Size(originalSize.Width - 10, originalSize.Height - 10);
            pictureBox1.Location = new Point(originalLocation.X + 5, originalLocation.Y + 5);

            await Task.Delay(100);

            pictureBox1.Size = originalSize;
            pictureBox1.Location = originalLocation;
        }
        private async void ShowClickEffect(Point position)
        {
            Label clickEffectLabel = new Label
            {
                Text = $"+{cookieAmount}",
                ForeColor = Color.Gold,
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.Transparent,
                AutoSize = true,
                Location = new Point(position.X - 10, position.Y - 10)
            };

            pictureBox1.Controls.Add(clickEffectLabel);

            int animationSteps = 15;
            int moveAmount = 2;


            for (int i = 0; i < animationSteps; i++)
            {
                clickEffectLabel.Top -= moveAmount;
                await Task.Delay(30);
            }

            pictureBox1.Controls.Remove(clickEffectLabel);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Grandma_Click(object sender, EventArgs e)
        {
            if (cookiesClicked >= cookie)
            {
                cookiesClicked -= cookie;
                cookie *= 2;
                cookieAmount++;
                string CookieClicked = cookiesClicked.ToString();
                label2.Text = CookieClicked;
                label4.Text = cookie.ToString();

                UpdateButtonStates();
            }
        }

        private void Sata_Click(object sender, EventArgs e)
        {

            if (cookiesClicked >= cookie2)
            {
                cookiesClicked -= cookie2;
                cookie2 *= 2;
                cookieAmount += 100;
                string CookieClicked = cookiesClicked.ToString();
                label2.Text = CookieClicked;
                label5.Text = cookie2.ToString();

                UpdateButtonStates();
            }
        }


        private bool autoClickerActive = false;

        private async void Autoclick_Click(object sender, EventArgs e)
        {
            if (cookiesClicked >= 9999 && !autoClickerActive)
            {
                cookiesClicked -= 9999;
                autoClickerActive = true;
                Autoclick.Enabled = false;
                var periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(1));

                while (autoClickerActive)
                {
                    await periodicTimer.WaitForNextTickAsync();
                    cookiesClicked += cookieAmount;
                    label2.Text = cookiesClicked.ToString();
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveGameData();
        }

        private void SaveGameData()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(SaveFilePath))
                {
                    writer.WriteLine(cookiesClicked);
                    writer.WriteLine(cookieAmount);
                    writer.WriteLine(cookie);
                    writer.WriteLine(cookie2);
                    writer.WriteLine(cookie3);
                    writer.WriteLine(cookie4);
                    writer.WriteLine(cookie5);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadGameData()
        {
            try
            {
                if (File.Exists(SaveFilePath))
                {
                    string[] data = File.ReadAllLines(SaveFilePath);

                    if (data.Length >= 7 &&
                    ulong.TryParse(data[0], out ulong savedCookies) &&
                    ulong.TryParse(data[1], out ulong savedAmount) &&
                    ulong.TryParse(data[2], out ulong savedCookie) &&
                    ulong.TryParse(data[3], out ulong savedCookie2) &&
                    ulong.TryParse(data[4], out ulong savedCookie3) &&
                    ulong.TryParse(data[5], out ulong savedCookie4) &&
                    ulong.TryParse(data[6], out ulong savedCookie5))
                    {
                        cookiesClicked = savedCookies;
                        cookieAmount = savedAmount;
                        cookie = savedCookie;
                        cookie2 = savedCookie2;
                        cookie3 = savedCookie3;
                        cookie4 = savedCookie4;
                        cookie5 = savedCookie5;

                        label2.Text = cookiesClicked.ToString();
                        label4.Text = cookie.ToString();
                        label5.Text = cookie2.ToString();
                        label8.Text = cookie3.ToString();
                        label9.Text = cookie4.ToString();
                        label10.Text = cookie5.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cookiesClicked >= cookie3)
            {
                cookiesClicked -= cookie3;
                cookie3 *= 2;
                cookieAmount += 10000;
                string CookieClicked = cookiesClicked.ToString();
                label2.Text = CookieClicked;
                label8.Text = cookie3.ToString();
                UpdateButtonStates();
            }
        }
        private async void StartButtonUpdater()
        {
            while (true)
            {
                UpdateButtonStates();
                await Task.Delay(100);
            }
        }
        private void UpdateButtonStates()
        {
            Grandma.Enabled = (cookiesClicked >= cookie);
            Sata.Enabled = (cookiesClicked >= cookie2);
            button1.Enabled = (cookiesClicked >= cookie3);
            button2.Enabled = (cookiesClicked >= cookie4);
            button3.Enabled = (cookiesClicked >= cookie5);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cookiesClicked >= 10)
            {
                BackColor = Color.DarkRed;
                panel1.BackColor = Color.Red;
                panel2.BackColor = Color.Red;
                player.Play();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (cookiesClicked >= cookie5)
            {
                cookiesClicked -= cookie5;
                cookie5 *= 2;
                cookieAmount += 100000;
                string CookieClicked = cookiesClicked.ToString();
                label2.Text = CookieClicked;
                label10.Text = cookie5.ToString();
                UpdateButtonStates();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (cookiesClicked >= cookie4)
            {
                cookiesClicked -= cookie4;
                cookie4 *= 2;
                cookieAmount += 1000;
                string CookieClicked = cookiesClicked.ToString();
                label2.Text = CookieClicked;
                label9.Text = cookie4.ToString();
                UpdateButtonStates();
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {
            
        }
    }
}
