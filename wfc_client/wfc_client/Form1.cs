using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;

namespace wfc_client
{
    public partial class Form1 : Form
    {
        public ServiceReference2.Service1Client sc = new ServiceReference2.Service1Client();
        private DataSet Data = new DataSet();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_read_Click(object sender, EventArgs e)
        {
            try {
                
                Data = sc.readDB();
                dataGridView1.DataSource = Data.Tables["Databases"];
            }
            catch
            {
                MessageBox.Show("연결확인");
            }
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            try {
                String[] st = new String[5];
                DataTable dtChanged = Data.Tables["Databases"].GetChanges(DataRowState.Added);
                foreach (DataRow row in dtChanged.Rows)
                {
                    if (row.RowState == DataRowState.Added)
                    {
                       for(int i = 0; i < 5; i++)
                        {
                            st[i] = row[i].ToString();
                        }
                        sc.createDB(st);
                    }

                }

            }
            catch {
                MessageBox.Show("연결확인");
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try {
                sc.updateDB(Data.Tables["Databases"].GetChanges(DataRowState.Modified));

            }
            catch {
                MessageBox.Show("연결확인");
            }
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            try {
                sc.removeDB(dataGridView1.CurrentRow.Cells[2].Value.ToString());
                Data.Tables["Databases"].Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            }
            catch {
                MessageBox.Show("연결확인");
            }
        }
    }
}
