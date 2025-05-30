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
using System.Xml.Linq;

namespace Hostel_Management_System
{
    public partial class ROOMS : Form
    {
        private readonly string connectionString = "Data Source= desktop-0jim8li\\SQLEXPRESS;Initial Catalog =HOSTEL_management_System ; Integrated Security = True";

        public ROOMS()
        {
            InitializeComponent();
        }

        private void ROOMS_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM ROOMS";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();

                try
                {
                    con.Open();
                    da.Fill(dt);
                    con.Close();

                    // Bind the data to the DataGridView
                    dataGridView1.DataSource = dt;

                    // Optionally, hide the Id column (if not needed to be shown)
                    dataGridView1.Columns["ROOMNUMBER"].Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void RoomForm_Load(object sender, EventArgs e)
        {
            // ROOMSTATUSCB.Items.Clear();
            //  ROOMSTATUSCB.Items.Add("Available");
            // ROOMSTATUSCB.Items.Add("Occupied");
        }
        private void ClearFields()
        {
            //ROOMNUMtb.Clear();
            // ROOMSTATUSCB.SelectedIndex = -1;
            // YesRadio.Checked = false;
            //NoRadio.Checked = false;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Form1 Home = new Form1();
            Home.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO ROOMS (ROOMNUMBER,ROOMSTATUS,BOOKED) VALUES (@ROOMNUMBER,@ROOMSTATUS,@BOOKED)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ROOMNUMBER", ROOMNUMtb.Text);
                    cmd.Parameters.AddWithValue("@ROOMSTATUS", ROOMSTATUS.Text);
                    cmd.Parameters.AddWithValue("@BOOKED", BOOKED.Text);


                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("RESULT DISPLAYED");
                    // LoadData();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure we are clicking on a row (not a header or empty cell)
            if (e.RowIndex >= 0)
            {
                // Get the selected row
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Populate textboxes with the row data
                ROOMNUMtb.Text = row.Cells["ROOMNUMBER"].Value.ToString();
                ROOMSTATUS.Text = row.Cells["ROOMSTATUS"].Value.ToString();
                BOOKED.Text = row.Cells["BOOKED"].Value.ToString();
                // txtPhone.Text = row.Cells["Phone"].Value.ToString();
                // txtDepartment.Text = row.Cells["Department"].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE ROOMS SET ROOMNUMBER=@ROOMNUMBER, ROOMSTATUS=@ROOMSTATUS,BOOKED=@BOOKED WHERE ROOMNUMBER = @ROOMNUMBER";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ROOMNUMBER", ROOMNUMtb.Text);
                cmd.Parameters.AddWithValue("@ROOMSTATUS", ROOMSTATUS.Text);
                cmd.Parameters.AddWithValue("@BOOKED", BOOKED.Text);
                

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Result updated!");
                LoadData();
                ClearFields();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM ROOMS WHERE ROOMNUMBER=@ROOMNUMBER";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ROOMNUMBER", ROOMNUMtb.Text);
                cmd.Parameters.AddWithValue("@ROOMSTATUS", ROOMSTATUS.Text);
                cmd.Parameters.AddWithValue("@BOOKED", BOOKED.Text);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("ROOMS deleted!");
                LoadData();
                ClearFields();
            }
        }
    }
}
