using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private BindingSource bindingSource1 = new BindingSource();
        public Form1()
        {
            InitializeComponent();

            tabela = new DataTable();
            tabela.Columns.Add("ID", typeof(int));
            tabela.Columns.Add("Imie", typeof(string));
            tabela.Columns.Add("Nazwisko", typeof(string));
            tabela.Columns.Add("Wiek", typeof(int));
            tabela.Columns.Add("Stanowisko", typeof(string));

            tabela.Rows.Add(1, "Jan", "Nowak", 41, "Szef");
            tabela.Rows.Add(2, "Joanna", "Kowalska", 33, "Sekretarka");

            bindingSource1.DataSource = tabela;

            DataGridView dataGridView1 = new DataGridView();
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Controls.Add(dataGridView1);

            dataGridView1.DataSource = bindingSource1;
        }

        private DataTable tabela;
        private int ostatnieID = 2; 

        private void button2_Click(object sender, EventArgs e)
        {
            DataGridView dgv = Controls.Find("dataGridView1", true).FirstOrDefault() as DataGridView;
            if (dgv?.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgv.SelectedRows)
                {
                    if (!row.IsNewRow)
                        dgv.Rows.Remove(row);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            if (form2.ShowDialog() == DialogResult.OK)
            {
                ostatnieID++;
                tabela.Rows.Add(ostatnieID, form2.Imie, form2.Nazwisko, form2.Wiek, form2.Stanowisko);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "CSV|*.csv";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFile.FileName))
                {
                    sw.WriteLine("ID,Imie,Nazwisko,Wiek,Stanowisko");
                    foreach (DataRow row in tabela.Rows)
                    {
                        sw.WriteLine(string.Join(",", row.ItemArray));
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "CSV|*.csv";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                tabela.Clear();
                var lines = File.ReadAllLines(openFile.FileName);
                for (int i = 1; i < lines.Length; i++)
                {
                    var values = lines[i].Split(',');
                    tabela.Rows.Add(int.Parse(values[0]), values[1], values[2], int.Parse(values[3]), values[4]);
                    if (int.Parse(values[0]) > ostatnieID)
                        ostatnieID = int.Parse(values[0]);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnZapiszJSON_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Pliki JSON (*.json)|*.json";
            saveDialog.Title = "Zapisz dane do pliku JSON";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                List<Osoba> osoby = new List<Osoba>();

                foreach (DataRow row in tabela.Rows)
                {
                    osoby.Add(new Osoba(
                        Convert.ToInt32(row["ID"]),
                        row["Imie"].ToString(),
                        row["Nazwisko"].ToString(),
                        Convert.ToInt32(row["Wiek"]),
                        row["Stanowisko"].ToString()
                    ));
                }

                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(osoby, options);
                File.WriteAllText(saveDialog.FileName, json);

                MessageBox.Show("Dane zapisano do pliku JSON.");
            }
        }
    }

}
