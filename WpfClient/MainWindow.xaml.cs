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
            IEnumerable<Employee> employees = await GetProductsAsync<Employee>(client.BaseAddress + "Employees");
            lvEmployee.ItemsSource = employees;
        }

        private async void AllproductButton_ClickDep(object sender, RoutedEventArgs e) 
        {
            IEnumerable<Department> departments = await GetProductsAsync<Department>(client.BaseAddress + "Departments");
            lbDepartmant.ItemsSource = departments;
        }

        private async void IdEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            List<Employee> employee = new List<Employee>();
            if (idproductTextBox.Text != String.Empty)
            {
                Employee product = await GetProductAsync<Employee>(client.BaseAddress + "Employees/" +
                idproductTextBox.Text);
                if (product != null)
                    employee.Add(product);
            }
            else
            {
                employee = (List<Employee>)await GetProductsAsync<Employee>(client.BaseAddress + "Employees");
            }
            lvEmployee.ItemsSource = employee;
        }

        private async void IdDepartmentButton_Click(object sender, RoutedEventArgs e)
        {
            List<Department> department = new List<Department>();
            if (idproductTextBox2.Text != String.Empty)
            {
                Department product = await GetProductAsync<Department>(client.BaseAddress + "Departments/" +
                idproductTextBox2.Text);
                if (product != null)
                    department.Add(product);
            }
            else
            {
                department = (List<Department>)await GetProductsAsync<Department>(client.BaseAddress + "Departments");
            }
            lbDepartmant.ItemsSource = department;
        }

        static async Task<IEnumerable<T>> GetProductsAsync<T>(string path)
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

        static async Task<T> GetProductAsync<T>(string path)
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
