using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BusinessLogic;

namespace FantasyFootball
{
    /// <summary>
    /// Interaction logic for DatabaseTools.xaml
    /// </summary>
    public partial class DatabaseTools : Window
    {
        public DatabaseTools()
        {
            InitializeComponent();
        }

        private void btnCreateDatabase_Click(object sender, RoutedEventArgs e)
        {
            BackendData.Create_Database();
        }
    }
}
