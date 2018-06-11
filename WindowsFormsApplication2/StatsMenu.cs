using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class StatsMenu : Form
    {
        public StatsMenu()
        {
            InitializeComponent();
        }

        public event EventHandler<MarkEventArgs> MarkAdded;
        protected virtual void OnMarkAdded(int mark)
        {
            MarkAdded?.Invoke(this, new MarkEventArgs { Mark = mark });
        }
        public string TeacherName
        {

            set
            {
                LTeacher.Text = value;
            }
        }

        public Student StudentName
        {

            set
            {
                LStudent.Text = $"{value.Surname} {value.Name}";
            }
        }

        public List<int> Marks
        {
            get
            {
                return LBMarks.Items.OfType<String>().Select(x=>int.Parse(x)).ToList();
            }
            set
            {
                LBMarks.Items.Clear();
                LBMarks.Items.AddRange(value.Select(x=>x.ToString()).ToArray());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int num = int.Parse(comboBox1.Text);
            Marks.Add(num);
            OnMarkAdded(num);
        }
    }
}
