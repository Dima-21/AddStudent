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
    public partial class AddStudent : Form
    {     
        public AddStudent()
        {
            InitializeComponent();
        }



        public string SelectTeacher
        {
            get { return (string)CBTeachers.SelectedItem; }
            set { CBTeachers.SelectedItem  = value; }
        }       

        public List<Teacher> Teachers
        {
            set
            {
                CBTeachers.Items.Clear();
                CBTeachers.Items.AddRange(value.Select(x=>x.Name).ToArray());
            }
        }

        public DateTime Birthday
        {
            get
            {
                return Calendar.SelectionStart;
            }
            set
            {
                Calendar.BoldedDates = new DateTime[] { value};
                Calendar.SelectionStart = value;
            }
        }

        public String NameP
        {
            get
            {
                return TName.Text;
            }
            set
            {
                TName.Text = value;
            }
        }
        public String Surname
        {
            get
            {
                return TSurname.Text;
            }
            set
            {
                TSurname.Text = value;
            }
        }
        public String Patronymic
        {
            get
            {
                return TPatronymic.Text;
            }
            set
            {
                TPatronymic.Text = value;
            }
        }

        private void BApply_Click(object sender, EventArgs e)
        {
            if (Check())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void BCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool Check()
        {
            if (Surname == "" || NameP == "" || Patronymic == "" || Char.IsLower(Surname[0]) || Char.IsLower(NameP[0]) || Char.IsLower(Patronymic[0]))
            {
                if (Surname == "" || Char.IsLower(Surname[0]))
                    Check1.Visible = true;
                else
                    Check1.Visible = false;
                if (NameP == "" || Char.IsLower(NameP[0]))
                    Check2.Visible = true;
                else
                    Check2.Visible = false;
                if (Patronymic == "" || Char.IsLower(Patronymic[0]))
                    Check3.Visible = true;
                else
                    Check3.Visible = false;
                return false;
            }
            return true;
        }

    
    }
}
