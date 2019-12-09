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

namespace AffdexMe
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SelectVideoSource : Window
    {
        public SelectVideoSource()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        /// <summary>
        /// Handles the Loaded event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            // Initialize Button Click Handlers
            btnCamera.Click += btnCamera_Click;
            btnVideoFile.Click += btnVideoFile_Click;
        }

        /// <summary>
        /// Handles the Click event of the btnCamera control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnCamera_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(null);
            mainWindow.Show();
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnVideoFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnVideoFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
           
            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".mp4";
            dlg.Multiselect = true;
            dlg.Filter = "MP4 Files (*.mp4)|*.mp4|MKV Files (*.mkv)|*.mkv|AVI Files (*.avi)|*.avi";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                String[] filenames = dlg.FileNames;
                //MessageBox.Show("" + filename, "YUP", MessageBoxButton.OKCancel);
                MainWindow mainWindow = new MainWindow(filenames);
                mainWindow.Show();
                this.Close();
            }
        }
    }
}
