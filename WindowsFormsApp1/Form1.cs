using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private BindingSource bindingSource1 = new BindingSource();
        public Form1()
        {
            InitializeComponent();
            // Utwórz dane do siatki
            var dataTable = new System.Data.DataTable();
            dataTable.Columns.Add("ID", typeof(int));
            dataTable.Columns.Add("Imie", typeof(string));
            dataTable.Columns.Add("Nazwisko", typeof(string));
            dataTable.Columns.Add("Wiek", typeof(int));
            dataTable.Columns.Add("Stanowisko", typeof(string));
            // Dodaj przykładowe dane
            dataTable.Rows.Add(1, "Jan", "Nowak", 41, "Szef");
            dataTable.Rows.Add(2, "Joanna", "Kowalska", 33, "Sekretarka");
            // Połącz dane z BindingSource
            bindingSource1.DataSource = dataTable;
            // Inicjalizacja DataGridView
            DataGridView dataGridView1 = new DataGridView();
            dataGridView1.Dock = DockStyle.Fill;
            // Przypisz DataGridView do BindingSource
            dataGridView1.DataSource = bindingSource1;
            // Dodaj DataGridView do formularza
            Controls.Add(dataGridView1);
        }


        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }

}
