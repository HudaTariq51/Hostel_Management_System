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
using System.Data.SqlClient;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Hostel_Management_System
{
    public partial class STUDENT : Form
    {
        private readonly string connectionString = "Data Source= desktop-0jim8li\\SQLEXPRESS;Initial Catalog =HOSTEL_management_System ; Integrated Security = True";
        public STUDENT()
        {
            InitializeComponent();
        }

        private void STUDENT_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void label4_Click(object sender, EventArgs e)
        {

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
                    string query = "INSERT INTO STUDENTS (STUDENTUSN, STUDENTNAME, FATHERNAME, ADDRESS, ROOMNUMBER, STUDENTSTATUS) VALUES (@STUDENTUSN, @STUDENTNAME, @FATHERNAME, @ADDRESS, @ROOMNUMBER, @STUDENTSTATUS)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Use .Text or .SelectedItem depending on your ComboBox setup
                        cmd.Parameters.AddWithValue("@STUDENTUSN", STUDENTUSN.Text.Trim());
                        cmd.Parameters.AddWithValue("@STUDENTNAME", STUDENTNAME.Text.Trim());
                        cmd.Parameters.AddWithValue("@FATHERNAME", FATHERNAME.Text.Trim());
                        cmd.Parameters.AddWithValue("@ADDRESS", ADDRESS.Text.Trim());
                        cmd.Parameters.AddWithValue("@ROOMNUMBER", ROOMNUMCB.SelectedItem?.ToString());
                        cmd.Parameters.AddWithValue("@STUDENTSTATUS", STUDENTSTATUSCB.SelectedItem?.ToString());

                        con.Open();
                        int result = cmd.ExecuteNonQuery();
                        con.Close();

                        if (result > 0)
                        {
                            MessageBox.Show("Student Added Successfully");
                            LoadData();     // Refresh DataGridView
                            ClearFields();  // Reset form fields
                        }
                        else
                        {
                            MessageBox.Show("Failed to add student.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
        private void LoadData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM STUDENTS";
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
                    dataGridView1.Columns["STUDENTUSN"].Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }

        }
        private void ClearFields()
        {
            STUDENTUSN.Clear();
            STUDENTNAME.Clear();
            FATHERNAME.Clear();
            ADDRESS.Clear();

            ROOMNUMCB.SelectedIndex = -1;  // Clears the combo box selection
            STUDENTSTATUSCB.SelectedIndex = -1;

            //txtUSN.Focus();  // Optional: sets focus to the first field
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure we're clicking on a valid row
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Populate controls with selected row data
                STUDENTUSN.Text = row.Cells["STUDENTUSN"].Value.ToString();
                STUDENTNAME.Text = row.Cells["STUDENTNAME"].Value.ToString();
                FATHERNAME.Text = row.Cells["FATHERNAME"].Value.ToString();
                ADDRESS.Text = row.Cells["ADDRESS"].Value.ToString();

                // ComboBoxes for Room Number and Student Status
                ROOMNUMCB.SelectedItem = row.Cells["ROOMNUMBER"].Value.ToString();
                STUDENTSTATUSCB.SelectedItem = row.Cells["STUDENTSTATUS"].Value.ToString();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE STUDENTS SET STUDENTUSN = @STUDENTUSN, STUDENTNAME=@STUDENTNAME, FATHERNAME=@FATHERNAME, ADDRESS= @ADDRESS, ROOMNUMBER= @ROOMNUMBER, STUDENTSTATUS=@STUDENTSTATUS WHERE STUDENTUSN = @STUDENTUSN";
                SqlCommand cmd = new SqlCommand(query, con);

                // Use .Text or .SelectedItem depending on your ComboBox setup
                cmd.Parameters.AddWithValue("@STUDENTUSN", STUDENTUSN.Text.Trim());
                cmd.Parameters.AddWithValue("@STUDENTNAME", STUDENTNAME.Text.Trim());
                cmd.Parameters.AddWithValue("@FATHERNAME", FATHERNAME.Text.Trim());
                cmd.Parameters.AddWithValue("@ADDRESS", ADDRESS.Text.Trim());
                cmd.Parameters.AddWithValue("@ROOMNUMBER", ROOMNUMCB.SelectedItem?.ToString());
                cmd.Parameters.AddWithValue("@STUDENTSTATUS", STUDENTSTATUSCB.SelectedItem?.ToString());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();


                MessageBox.Show("Student Updated Successfully");
                LoadData();     // Refresh DataGridView
                ClearFields();  // Reset form fields

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM STUDENTS WHERE STUDENTUSN = @STUDENTUSN";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@STUDENTUSN", STUDENTUSN.Text.Trim());

                try
                {
                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result > 0)
                    {
                        MessageBox.Show("Student record deleted successfully.");
                        LoadData();     // Refresh DataGridView
                        ClearFields();  // Clear form inputs
                    }
                    else
                    {
                        MessageBox.Show("No student found with the given USN.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);

                }
            }
        }
    }
}


