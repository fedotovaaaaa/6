using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using WindowsFormsApp1.DBCon;

namespace WindowsFormsApp1
{
    public partial class FormAddActivity : Form
    {
        public FormAddActivity()
        {
            InitializeComponent();
        }

        private List<int> Id_Juri = new List<int>(); 

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonAddJuri_Click(object sender, EventArgs e)
        {
            int id = (int)juricomboBox.SelectedValue;
            if (!Id_Juri.Contains(id))
            {
                Id_Juri.Add(id);
                MessageBox.Show($"Пользователь с ID - {juricomboBox.SelectedValue} добавлен!");
                return;
            }
        }

        private void buttonAddActivity_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(titletextBox.Text))
            {
                MessageBox.Show("Заполните поле Название!");
                return;
            }
            try
            {
                Convert.ToInt32(daytextBox.Text);
            }
            catch 
            {
                MessageBox.Show("В поле день должно стоять целочисленное значение!");
                return;
            }
            if(Id_Juri.Count <= 0)
            {
                MessageBox.Show("Добавьте хотя-бы одного члена жюри!");
                return;
            }

            Activity activity = new Activity();
            activity.Title = titletextBox.Text;
            activity.EventPlanID = (int)eventPlanIDcomboBox.SelectedValue;
            activity.Day = Convert.ToInt32(daytextBox.Text);
            activity.StartedAt = dateTimePicker1.Value.TimeOfDay;
            activity.ModeratorID = (int)ModeratocomboBox.SelectedValue;
            activity.GroupsJury = JsonSerializer.Serialize(Id_Juri);

            DBConst.model.Activity.Add(activity);
            try
            {
                DBConst.model.SaveChanges();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
                return;
            }
            Close();
        }

        private void FormAddActivity_Load(object sender, EventArgs e)
        {
            eventBindingSource.DataSource = DBConst.model.Event.ToList();

            userBindingSource.DataSource = DBConst.model.User.Where( x => x.RoleID == 1).ToList();
            userBindingSource2.DataSource = DBConst.model.User.Where(x => x.RoleID == 2).ToList();

        }

        private void titletextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
