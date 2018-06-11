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
    public partial class MainMenu : Form
    {
        List<Student> students = new List<Student>();
        List<Teacher> teachers = new List<Teacher>();
        List<Student> FilterStudent = new List<Student>();
        BindingSource bsStudents = new BindingSource();
        BindingSource bsTeachers = new BindingSource();
        public MainMenu()
        {
            InitializeComponent();
            bsStudents.DataSource = FilterStudent;
            LBStudents.DataSource = bsStudents;
            LBStudents.DisplayMember = "FIO";

            bsTeachers.DataSource = teachers;
            CBTeachers.DataSource = bsTeachers;
            CBTeachers.DisplayMember = "Name";
           
            teachers.Add(new Teacher { Name = "Клепач В.Д."});
            teachers.Add(new Teacher { Name = "Чумак" });
            teachers.Add(new Teacher { Name = "Хилько" });
            bsTeachers.ResetBindings(false);
        }

        private void FilterStudents()
        {
            FilterStudent.Clear();
            FilterStudent.AddRange(students.Where(x => x._Teacher.Name == ((Teacher)this.CBTeachers.SelectedItem).Name));
        }
        private void listBox_DoubleClick(object sender, EventArgs e)
        {
            var selItem = LBStudents.SelectedItem as Student;

            var wnd = new StatsMenu();
            wnd.Marks = new List<int>() { 1, 2, 43, 4, 32, 2 };
            wnd.MarkAdded += Wnd_MarkAdded;
            //wnd.Teachers = teachers;
            wnd.Show();
        
        }

        private void Wnd_MarkAdded(object sender, MarkEventArgs e)
        {
            MessageBox.Show(e.Mark.ToString());
            students.Where(x => x.Name == e.StudentName).Single().Marks.Add(e.Mark);
            students.Select(x => x.Name).ToArray();
        }

        private void BAdd_Click(object sender, EventArgs e)
        {
            

            var wnd = new AddStudent();
            wnd.Teachers = teachers;
            if (wnd.ShowDialog() == DialogResult.OK)
            {
                students.Add(new Student { Name = wnd.NameP,  Surname = wnd.Surname, Patronymic = wnd.Patronymic, Birthday = wnd.Birthday});
                students.Last()._Teacher = (from i in teachers where i.Name == wnd.SelectTeacher select i).First();
            }
            wnd.Dispose();
            FilterStudents();
            bsStudents.ResetBindings(false);
        }

        private void BRemove_Click(object sender, EventArgs e)
        {
            students.Remove(LBStudents.SelectedItem as Student);
            FilterStudents();
            bsStudents.ResetBindings(false);
        }

        private void BChange_Click(object sender, EventArgs e)
        {
            if (LBStudents.SelectedItems.Count != 0)
            {
                var selItem = LBStudents.SelectedItem as Student;
                var wnd = new AddStudent();
                wnd.NameP = selItem.Name;
                wnd.Surname = selItem.Surname;
                wnd.Patronymic = selItem.Patronymic;
                wnd.Birthday = selItem.Birthday;
                wnd.Teachers = teachers;
                
                wnd.SelectTeacher = selItem._Teacher.Name;
                if (wnd.ShowDialog() == DialogResult.OK)
                    selItem._Teacher = new Teacher { Name = wnd.SelectTeacher };
                wnd.Dispose();
            }
        }

        private void CBTeachers_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            FilterStudents();
            bsStudents.ResetBindings(false);
        }
    }
}
