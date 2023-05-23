using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hotel
{
    public partial class RoomInfo : Form
    {
        public RoomInfo()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\asus\Documents\hotelproject.mdf;Integrated Security=True;Connect Timeout=30");
        public void populate()
        {
            Con.Open();
            string Myquery = "select * from Room_tbl";
            SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
            SqlCommandBuilder cbuilder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            RoomGridView.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void Datelbl_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            main main = new main();
            main.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Datelbl.Text = DateTime.Now.ToLongTimeString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void RoomGridView_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            Roomnumtbl.Text = RoomGridView.SelectedRows[0].Cells[0].Value.ToString();
            RoomPhonetbl.Text = RoomGridView.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void RoomInfo_Load(object sender, EventArgs e)
        {
            //Datelbl.Text = DateTime.Now.ToLongTimeString();
            //timer1.Start();
            Datelbl.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
            populate();
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            string isfree;
            if (Yesradio.Checked == true)
                isfree = "free";
            else
                isfree = "busy";
            Con.Open();
            SqlCommand cmd = new SqlCommand("insert into Room_tbl values (" + Roomnumtbl.Text + ",'" + RoomPhonetbl.Text + "','" + isfree + "')", Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Room Successfully Added");
            Con.Close();
            populate();
        }

        private void edit_button_Click(object sender, EventArgs e)
        {
            string isfree;
            if (Yesradio.Checked == true)
                isfree = "free";
            else
                isfree = "busy";
            Con.Open();
            string myquery = "UPDATE Room_tbl SET RoomPhone = '" + RoomPhonetbl.Text + "', RoomFree = '" + isfree + "' WHERE RoomId = '" + Roomnumtbl.Text + "';";
            SqlCommand cmd = new SqlCommand(myquery, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Room Successfully Edited");
            Con.Close();
            populate();
        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query = "delete from Room_tbl where RoomId = " + Roomnumtbl.Text + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Room Successfully Deleted");
            Con.Close();
            populate();
        }

        private void search_box_Click(object sender, EventArgs e)
        {
            Con.Open();
            string Myquery = "select * from Room_tbl where RoomId = '" + RoomSearch.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
            SqlCommandBuilder cbuilder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            RoomGridView.DataSource = ds.Tables[0];
            Con.Close();
        }
    }
}
