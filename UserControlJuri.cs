using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.DBCon;

namespace WindowsFormsApp1
{
    public partial class UserControlJuri : UserControl
    {
        public UserControlJuri()
        {
            InitializeComponent();
        }
        public void Fill(User user, int number)
        {
            labelName.Text = "Жюри - " + number.ToString();
            userBindingSource.DataSource = user;
        }

    }
}
