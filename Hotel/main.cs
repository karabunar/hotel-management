using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void main_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            ClientInfo ClientInfo = new ClientInfo();
            ClientInfo.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2ImageButton5_Click(object sender, EventArgs e)
        {
            Form1 Form1 = new Form1();
            Form1.Show();
            this.Hide();

        }

        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {
            Staff Staff = new Staff();
            Staff.Show();
            this.Hide();
        }

        private void guna2ImageButton4_Click(object sender, EventArgs e)
        {
            RoomInfo RoomInfo = new RoomInfo();
            RoomInfo.Show();
            this.Hide();
        }

        private void guna2ImageButton3_Click(object sender, EventArgs e)
        {
            ReservationInfo ReservationInfo = new ReservationInfo();
            ReservationInfo.Show();
            this.Hide();
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
