using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel
{
    public partial class Staff : Form
        
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\asus\Documents\hotelproject.mdf;Integrated Security=True;Connect Timeout=30");

        public void populate()
        {
            con.Open();
            string Myquery = "select * from Staff_tbl";
            SqlDataAdapter da = new SqlDataAdapter(Myquery, con);
            SqlCommandBuilder cbuilder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            StaffGridView.DataSource = ds.Tables[0];
            con.Close();
        }
        public Staff()
        {
            InitializeComponent();
        }

        private void Staff_id_box_TextChanged(object sender, EventArgs e)
        {

        }

        private void staff_name_box_TextChanged(object sender, EventArgs e)
        {

        }

        private void phone_text_box_TextChanged(object sender, EventArgs e)
        {

        }

        private void password_text_box_TextChanged(object sender, EventArgs e)
        {

        }

        private void gender_box_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void add_button_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into Staff_tbl values(" + Staff_id_box.Text + " , '" + staff_name_box.Text + "' , '" + phone_text_box.Text + "' , '" + gender_box.SelectedItem.ToString() + "' , '" + password_text_box.Text + "')", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Staff Succesfully Added");
            con.Close();
            populate();
        }

        private void edit_button_Click(object sender, EventArgs e)
        {

            if (staff_name_box != null && phone_text_box != null && gender_box != null && password_text_box != null && Staff_id_box != null)
            {
                con.Open();
                string query = "UPDATE Staff_tbl SET StaffName = '" + staff_name_box.Text + "', staffphone = '" + phone_text_box.Text + "', gender = '" + (gender_box.SelectedItem != null ? gender_box.SelectedItem.ToString() : "") + "', password = '" + password_text_box.Text + "' WHERE StaffId = '" + Staff_id_box.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Staff Successfully Edited");
                con.Close();
                populate();
            }
            else
            {
                MessageBox.Show("One or more controls are null");
            }
        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "DELETE from Staff_tbl where StaffId=" + Staff_id_box.Text + "";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Staff Succesfully Deleted");
            con.Close();
            populate();
        }

        private void StaffGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Staff_id_box.Text = StaffGridView.SelectedRows[0].Cells[0].Value.ToString();
            staff_name_box.Text = StaffGridView.SelectedRows[0].Cells[1].Value.ToString();
            phone_text_box.Text = StaffGridView.SelectedRows[0].Cells[2].Value.ToString();
            password_text_box.Text = StaffGridView.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void staff_search_box_TextChanged(object sender, EventArgs e)
        {

        }

        private void search_box_Click(object sender, EventArgs e)
        {
            con.Open();
            string Myquery = "select * from Staff_tbl where StaffName= '" + staff_search_box.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(Myquery, con);
            SqlCommandBuilder cbuilder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            StaffGridView.DataSource = ds.Tables[0];
            con.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            main main = new main();
            main.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Datelbl.Text = DateTime.Now.ToLongTimeString();
        }

        private void Staff_Load(object sender, EventArgs e)
        {
            //Datelbl.Text = DateTime.Now.ToLongTimeString();
            //timer1.Start();
            Datelbl.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
            populate();
        }
    }
}
