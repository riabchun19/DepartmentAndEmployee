using System.Windows;

namespace WpfClient
{
    /// <summary>
    /// Логика взаимодействия для AddDepartment.xaml
    /// </summary>
    public partial class AddDepartment : Window
    {
        public Department D { get; set; }
        public AddDepartment(Department d)
        {
            InitializeComponent();
            D = d;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            D.NameDep = AddDepar.Text;
            this.Close();
        }
    }
}
