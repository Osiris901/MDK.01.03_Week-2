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
    public partial class SeedStartForm : Form
    {
        public int Seed => (int) numericUpDown1.Value;

        public SeedStartForm()
        {
            InitializeComponent();
        }
    }
}
