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
using System.Data.Sql;
using System.Data.SqlClient;

namespace Library
{
    public partial class locationForm : UserControl
    {
        // SQL Connection string
        private SqlConnection con;

        public locationForm()
        {
            con = Dbcon.GetConnection();
            InitializeComponent();
            SetupDataGridView();
            LoadLocations();
        }

        public void SetupDataGridView()
        {
            dataGridViewLocations.Columns.Add("Id", "ID");
            dataGridViewLocations.Columns.Add("Section", "Section");
            dataGridViewLocations.Columns.Add("Shelf", "Shelf");
            dataGridViewLocations.Columns.Add("Floor", "Floor");

            dataGridViewLocations.Columns["Id"].Visible = false;
        }

        public void LoadLocations()
        {
            dataGridViewLocations.Rows.Clear();


            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string query = "SELECT Id, Section, Shelf, Floor FROM locations";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Add each row to the DataGridView
                            dataGridViewLocations.Rows.Add(
                                reader["Id"],           
                                reader["Section"],    
                                reader["Shelf"],        
                                reader["Floor"]        
                            );
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading locations: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSection.Text) || string.IsNullOrEmpty(txtShelf.Text) || string.IsNullOrEmpty(txtFloor.Text))
            {
                MessageBox.Show("Please fill all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string section = txtSection.Text.Trim();
            string shelf = txtShelf.Text.Trim();
            string floor = txtFloor.Text.Trim();

            string query = "INSERT INTO locations (Section, Shelf, Floor) VALUES (@Section, @Shelf, @Floor)";

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Section", section);
                    cmd.Parameters.AddWithValue("@Shelf", shelf);
                    cmd.Parameters.AddWithValue("@Floor", floor);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Location added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtSection.Clear();
                    txtShelf.Clear();
                    txtFloor.Clear();

                    LoadLocations();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding location: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
            ClearForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Ensure a row is selected and fields are filled
            if (dataGridViewLocations.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a location to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtSection.Text) || string.IsNullOrEmpty(txtShelf.Text) || string.IsNullOrEmpty(txtFloor.Text))
            {
                MessageBox.Show("Please fill all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get selected row data
            DataGridViewRow selectedRow = dataGridViewLocations.SelectedRows[0];
            int id = Convert.ToInt32(selectedRow.Cells["Id"].Value); // Get the ID of the selected row
            string section = txtSection.Text.Trim();
            string shelf = txtShelf.Text.Trim();
            string floor = txtFloor.Text.Trim();

            // Update query
            string query = "UPDATE locations SET Section = @Section, Shelf = @Shelf, Floor = @Floor WHERE Id = @Id";

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Section", section);
                    cmd.Parameters.AddWithValue("@Shelf", shelf);
                    cmd.Parameters.AddWithValue("@Floor", floor);
                    cmd.Parameters.AddWithValue("@Id", id);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Location updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Reload locations in the DataGridView
                    LoadLocations();

                    // Clear textboxes
                    txtSection.Clear();
                    txtShelf.Clear();
                    txtFloor.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating location: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void dataGridViewLocations_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridViewLocations.Rows[e.RowIndex];

                // Assign the values from the selected row to the textboxes
                txtSection.Text = selectedRow.Cells["Section"].Value?.ToString() ?? string.Empty;
                txtShelf.Text = selectedRow.Cells["Shelf"].Value?.ToString() ?? string.Empty;
                txtFloor.Text = selectedRow.Cells["Floor"].Value?.ToString() ?? string.Empty;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Ensure a row is selected
            if (dataGridViewLocations.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a location to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the ID of the selected row
            DataGridViewRow selectedRow = dataGridViewLocations.SelectedRows[0];
            int id = Convert.ToInt32(selectedRow.Cells["Id"].Value); // Get the ID of the selected row

            // Confirm the deletion
            var result = MessageBox.Show("Are you sure you want to delete this location?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                // SQL query to delete the location
                string query = "DELETE FROM locations WHERE Id = @Id";

                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Location deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Reload locations in the DataGridView
                        LoadLocations();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting location: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                }
                ClearForm();
            }
        }
        private void ClearForm()
        {
            // Clear all the textboxes
            txtSection.Text = string.Empty;
            txtShelf.Text = string.Empty;
            txtFloor.Text = string.Empty;

            // Clear the selection in the DataGridView
            if (dataGridViewLocations.SelectedRows.Count > 0)
            {
                dataGridViewLocations.ClearSelection();
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
    }
}
