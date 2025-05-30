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
using System.Net;

namespace Hostel_Management_System
{
    public partial class FEES : Form
    {
        private readonly string connectionString = "Data Source= desktop-0jim8li\\SQLEXPRESS;Initial Catalog =HOSTEL_management_System ; Integrated Security = True";
        public FEES()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 Home = new Form1();
            Home.Show();
            this.Hide();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO FEES (PAYMENTID ,STUDENTUSN, STUDENTID, ROOMNUMBER, MONTH, AMOUNT) VALUES (@PAYMENTID ,@STUDENTUSN, @STUDENTID, @ROOMNUMBER, @MONTH, @AMOUNT)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PAYMENTID", PAYMENTID.Text.Trim());
                cmd.Parameters.AddWithValue("@STUDENTUSN", STUDENTUSN.SelectedItem?.ToString());
                cmd.Parameters.AddWithValue("@STUDENTID", STUDENTID.Text.Trim());
                cmd.Parameters.AddWithValue("@ROOMNUMBER", ROOMNUMCB.SelectedItem?.ToString());
                cmd.Parameters.AddWithValue("@MONTH", MONTH.Value.Date);
                cmd.Parameters.AddWithValue("@AMOUNT", Convert.ToDecimal(AMOUNT.Text.Trim()));

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                
        

                MessageBox.Show("Payment added successfully.");
                LoadFeesData();     // Refresh DataGridView
                ClearFeesFields();  // Reset form fields
            }
        }

    

            private void LoadFeesData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            { try
                {
                    string query = "SELECT * FROM FEES"; // You can filter or order if needed
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt; // Replace with your actual DataGridView name
                    dataGridView1.Columns["PAYMENTID"].Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            
            }
        }
        private void ClearFeesFields()
        {
            PAYMENTID.Clear();
            STUDENTUSN.SelectedIndex = -1;
            STUDENTID.Clear();
            ROOMNUMCB.SelectedIndex = -1;
            AMOUNT.Clear();
            MONTH.Value = DateTime.Now; // If MONTH is a DateTimePicker
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                PAYMENTID.Text = row.Cells["PAYMENTID"].Value.ToString();
                STUDENTUSN.SelectedItem = row.Cells["STUDENTUSN"].Value.ToString();
                STUDENTID.Text = row.Cells["STUDENTID"].Value.ToString();
                ROOMNUMCB.SelectedItem = row.Cells["ROOMNUMBER"].Value.ToString();

                // If MONTH is a DateTimePicker
                MONTH.Value = Convert.ToDateTime(row.Cells["MONTH"].Value);

                AMOUNT.Text = row.Cells["AMOUNT"].Value.ToString();
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE FEES SET PAYMENTID=@PAYMENTID, STUDENTUSN=@STUDENTUSN, STUDENTID=@STUDENTID,ROOMNUMBER=@ROOMNUMBER, MONTH=@MONTH,AMOUNT=@AMOUNT WHERE PAYMENTID = @PAYMENTID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PAYMENTID", PAYMENTID.Text.Trim());
                cmd.Parameters.AddWithValue("@STUDENTUSN", STUDENTUSN.SelectedItem?.ToString());
                cmd.Parameters.AddWithValue("@STUDENTID", STUDENTID.Text.Trim());
                cmd.Parameters.AddWithValue("@ROOMNUMBER", ROOMNUMCB.SelectedItem?.ToString());
                cmd.Parameters.AddWithValue("@MONTH", MONTH.Value.Date);
                cmd.Parameters.AddWithValue("@AMOUNT", Convert.ToDecimal(AMOUNT.Text.Trim()));



                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Result updated!");
                LoadFeesData();
                ClearFeesFields();
            }
        }
        private void FeesInformationForm_Load(object sender, EventArgs e)
        {
            LoadFeesData();  // This loads the data into the grid when form opens
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM FEES WHERE PAYMENTID=@PAYMENTID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PAYMENTID", PAYMENTID.Text.Trim());
                cmd.Parameters.AddWithValue("@STUDENTUSN", STUDENTUSN.SelectedItem?.ToString());
                cmd.Parameters.AddWithValue("@STUDENTID", STUDENTID.Text.Trim());
                cmd.Parameters.AddWithValue("@ROOMNUMBER", ROOMNUMCB.SelectedItem?.ToString());
                cmd.Parameters.AddWithValue("@MONTH", MONTH.Value.Date);
                cmd.Parameters.AddWithValue("@AMOUNT", Convert.ToDecimal(AMOUNT.Text.Trim()));



                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("RECORD deleted!");
                LoadFeesData();
                ClearFeesFields();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
    }

   
