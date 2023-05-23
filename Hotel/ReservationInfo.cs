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
    public partial class ReservationInfo : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\asus\Documents\hotelproject.mdf;Integrated Security=True;Connect Timeout=30");
        public void populate()
        {
            Con.Open();
            string Myquery = "select * from Reservation_tbl";
            SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
            SqlCommandBuilder cbuilder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            ResGridView.DataSource = ds.Tables[0];
            Con.Close();
        }
        public ReservationInfo()
        {
            InitializeComponent();
        }

        public void fillRoomCombo()
        {
            Con.Open();
            string roomstate = "free";
            SqlCommand cmd = new SqlCommand("select RoomId from Room_tbl where RoomFree= '"+roomstate+"'" , Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Roomid", typeof(int));
            dt.Load(rdr);
            Room.ValueMember = "Roomid";
            Room.DataSource = dt;

            Con.Close();

        }
        public void fillClientCombo()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select ClientName from Client_tbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("ClientName", typeof(string));
            dt.Load(rdr);
            Client.ValueMember = "ClientName";
            Client.DataSource = dt;

            Con.Close();

        }

        DateTime today;

        
        public void updateroomstate()
        {
            Con.Open();
            string newstate = "busy";
            string myquery = "UPDATE Room_tbl set RoomFree = '" + newstate + "'where Roomid = " + Convert.ToInt32(Room.SelectedValue.ToString()) + ";";
            SqlCommand cmd = new SqlCommand(myquery, Con);
            cmd.ExecuteNonQuery();
            // MessageBox.Show("Reservation Successfully Edited");
            Con.Close();
            fillRoomCombo();
        }

        public void updateroomondelete()
        {
            Con.Open();
            string newstate = "free";
            int roomid = Convert.ToInt32(ResGridView.SelectedRows[0].Cells[2].Value.ToString());
            string myquery = "UPDATE Room_tbl set RoomFree = '" + newstate + "'where Roomid = " + roomid + ";";
            SqlCommand cmd = new SqlCommand(myquery, Con);
            cmd.ExecuteNonQuery();
            // MessageBox.Show("Reservation Successfully Edited");
            Con.Close();
            fillRoomCombo();
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("insert into Reservation_tbl values(" + Resid.Text + ",'" + Client.SelectedValue.ToString() + "','" + Room.SelectedValue.ToString() + "','" + DateIn.Value + "','" + DateOut.Text + "')", Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Reservation Successfully Added");
            Con.Close();
            updateroomstate();
            populate();
        }

        private void edit_button_Click(object sender, EventArgs e)
        {
            if (Resid.Text == " ")
            {
                MessageBox.Show("Empty Res ID, enter the Reservation ID");
            }
            else
            {

                Con.Open();
                string myquery = "UPDATE Reservation_tbl set Client = '" + Client.SelectedValue.ToString() + "',Room='" + Room.SelectedValue.ToString() + "',DateIn='" + DateIn.Value.ToString() + "',DateOut='" + DateOut.Value.ToString() + "'where Resid = " + Resid.Text + ";";
                SqlCommand cmd = new SqlCommand(myquery, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Reservation Successfully Edited");
                Con.Close();
                populate();
            }
        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            if (Resid.Text == " ")
            {
                MessageBox.Show("Enter the reservation to be deleted");
            }
            else
            {
                Con.Open();
                string query = "delete from Reservation_tbl where ResId=" + Resid.Text + "";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Reservtion Successfully Deleted");
                Con.Close();
                updateroomondelete();
                populate();
            }

        }

        private void clientIdtbdl_TextChanged(object sender, EventArgs e)
        {

        }

        private void ReservationInfo_Load(object sender, EventArgs e)
        {            
            today = DateTime.Now;
            fillRoomCombo();
            fillClientCombo();
            populate();
            Datelbl.Text = DateTime.Today.Day.ToString() + "/"+ DateTime.Today.Month.ToString() +"/" + DateTime.Today.Year.ToString();

        }

        private void DateIn_ValueChanged(object sender, EventArgs e)
        {
            int res = DateTime.Compare(DateIn.Value, today); 
            if (res < 0)
            {
                MessageBox.Show("Wrong Date For Reservation");
            }
        }

        private void DateOut_ValueChanged(object sender, EventArgs e)
        {
            int res = DateTime.Compare(DateOut.Value, DateIn.Value);
            if (res < 0)
            {
                MessageBox.Show("Wrong Date Our, check again.");
            }
        }

        private void search_box_Click(object sender, EventArgs e)
        {
            Con.Open();
            string Myquery = "select * from Reservation_tbl where Resid='" + ResSearch.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
            SqlCommandBuilder cbuilder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            ResGridView.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void Client_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Room_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ResGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Resid.Text = ResGridView.SelectedRows[0].Cells[0].Value.ToString();
            
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            populate();
        }
    }
}
