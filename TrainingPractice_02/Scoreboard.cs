using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrainingPractice_02
{
    public partial class Scoreboard : Form
    {
        private string _path;

        public Scoreboard(string filePath)
        {
            InitializeComponent();

            _path = filePath;
            BindData();
        }

        private void BindData()
        {
            var dt = new DataTable();
            var lines = File.ReadAllLines(_path);
            var labels = lines.First().Split(';');
            foreach (var label in labels)
            {
                dt.Columns.Add(new DataColumn(label));
            }

            foreach (var line in lines.Skip(1))
            {
                var dw = line.Split(';');
                var dr = dt.NewRow();

                var i = 0;
                foreach (var header in labels)
                {
                    dr[header] = dw[i++];
                }

                dt.Rows.Add(dr);
            }

            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
            }
        }
    }
}
