using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        static HttpClient client = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();

            client.BaseAddress = new Uri("http://localhost:5000");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new
            MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async void AllproductButton_ClickEmp(object sender, RoutedEventArgs e)
        {
            IEnumerable<Employee> employees = await GetEntitiesAsync<Employee>(client.BaseAddress + "Employees");
            lvEmployee.ItemsSource = employees;
        }

        private async void AllproductButton_ClickDep(object sender, RoutedEventArgs e) 
        {
            IEnumerable<Department> departments = await GetEntitiesAsync<Department>(client.BaseAddress + "Departments");
            lbDepartmant.ItemsSource = departments;
        }

        private async void IdEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            List<Employee> listEmployee = new List<Employee>();
            if (idEmployeeTextBox.Text != String.Empty)
            {
                Employee employee = await GetEssenceAsync<Employee>(client.BaseAddress + "Employees/" +
                idEmployeeTextBox.Text);
                if (employee != null)
                    listEmployee.Add(employee);
            }
            else
            {
                listEmployee = (List<Employee>)await GetEntitiesAsync<Employee>(client.BaseAddress + "Employees");
            }
            lvEmployee.ItemsSource = listEmployee;
        }

        private async void IdDepartmentButton_Click(object sender, RoutedEventArgs e)
        {
            List<Department> listDepartment = new List<Department>();
            if (idpDepartmentTextBox.Text != String.Empty)
            {
                Department department = await GetEssenceAsync<Department>(client.BaseAddress + "Departments/" +
                idpDepartmentTextBox.Text);
                if (department != null)
                    listDepartment.Add(department);
            }
            else
            {
                listDepartment = (List<Department>)await GetEntitiesAsync<Department>(client.BaseAddress + "Departments");
            }
            lbDepartmant.ItemsSource = listDepartment;
        }

        private async void AddDepartment(object sender, RoutedEventArgs e)
        {
            List<Department> listDepartments = new List<Department>();
            Department department = new Department();
            AddDepartment addDep = new AddDepartment(department);
            addDep.ShowDialog();
            if (addDep != null)
            {
                department = await PostAsync<Department>(client.BaseAddress + "Departments", addDep.D);
                if (department != null)
                    listDepartments.Add(department);
            }
            lbDepartmant.ItemsSource = listDepartments;
        }

        static async Task<T> PostAsync<T>(string path, Department a)
        {
            T allEntities = default;
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(path, a);
                if (response != null)
                {
                    allEntities = await response.Content.ReadAsAsync<T>();
                }
            }
            catch (Exception)
            {
            }
            return allEntities;
        }

        static async Task<IEnumerable<T>> GetEntitiesAsync<T>(string path)
        {
            IEnumerable<T> allEntities = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    allEntities = await response.Content.ReadAsAsync<IEnumerable<T>>();
                }
            }
            catch (Exception)
            {
            }
            return allEntities;
        }

        static async Task<T> GetEssenceAsync<T>(string path)
        {
            T idEssence = default;
            try
            {
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    idEssence = await response.Content.ReadAsAsync<T>();
                }
            }
            catch (Exception)
            {
            }
            return idEssence;
        }
    }
}
