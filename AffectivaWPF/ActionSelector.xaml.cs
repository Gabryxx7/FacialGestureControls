using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AffdexMe
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ActionSelector : System.Windows.Controls.UserControl
    {
        public AffectivaFeature feature;
        public ActionSelector()
        {
            InitializeComponent();
        }
        
        public ActionSelector(AffectivaFeature feature, Dictionary<String, Func<IntPtr, object, object, object>> actionsFunction) : this()
        {
            this.feature = feature;
            FeatureName.Content = feature.Name;
            ThresholdValue.Text = "" + feature.threshold;
            ActivationTime.Text = "" + feature.activationTime;
            Param1.Text = "" + feature.param1;
            Param2.Text = "" + feature.param2;
            FeatureActionBox.Items.Add("");
            foreach (String action in actionsFunction.Keys)
            {
                FeatureActionBox.Items.Add(action);
            }
            if(feature.ActionName.Trim() != "")
                FeatureActionBox.SelectedItem = feature.ActionName;

            FeatureActionBox.SelectionChanged += FeatureActionBox_SelectedChanged;
            Param1ComboBox.SelectionChanged += Param1ComboBox_SelectedChanged;
            ThresholdValue.LostFocus += ThresholdValue_LostFocus;
            ActivationTime.LostFocus += ActivationTime_LostFocus;
            Param1.LostFocus += Param1_LostFocus;
            Param2.LostFocus += Param2_LostFocus;
            FolderSelector.Click += FolderSelector_Click;
            SwitchParam1Box(feature);
            if(feature.ActionName.Trim() != "")
                FeatureActionBox.SelectedItem = feature.ActionName;
        }

        public void FolderSelector_Click(object sender, EventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Param1.Text = dialog.SelectedPath;
                }
            }
        }

        public void Param1_LostFocus(object sender, EventArgs e)
        {
            feature.param1 = Param1.Text.Trim();
        }

        public void Param2_LostFocus(object sender, EventArgs e)
        {
            feature.param2 = Param2.Text.Trim();
        }

        public void ThresholdValue_LostFocus(object sender, EventArgs e)
        {
            feature.threshold = float.Parse(ThresholdValue.Text.Trim());
        }

        public void ActivationTime_LostFocus(object sender, EventArgs e)
        {
            feature.activationTime = float.Parse(ActivationTime.Text.Trim());
        }

        public void FeatureActionBox_SelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            feature.ActionName = (String)FeatureActionBox.SelectedItem;
            SwitchParam1Box(feature);
        }

        public void Param1ComboBox_SelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            feature.param1 = (String)Param1ComboBox.SelectedItem;
        }

        public void SwitchParam1Box(AffectivaFeature feature)
        {
            if (feature.ActionName.ToLower().Contains("keyboard"))
            {
                Param1.Visibility = Visibility.Hidden;
                FolderSelector.Visibility = Visibility.Hidden;
                Param1ComboBox.Items.Add("");
                foreach (String key in Keyboard.ScanCodeShort.Keys)
                {
                    Param1ComboBox.Items.Add(key);
                }
                Param1ComboBox.Visibility = Visibility.Visible;
            }
            else if (feature.ActionName.ToLower().Contains("screenshot"))
            {
                Param1.Visibility = Visibility.Visible;
                FolderSelector.Visibility = Visibility.Visible;
                Param1ComboBox.Visibility = Visibility.Hidden;
            }
            else
            {
                Param1.Visibility = Visibility.Visible;
                FolderSelector.Visibility = Visibility.Hidden;
                Param1ComboBox.Visibility = Visibility.Hidden;
            }


            if (((string)feature.param1).Trim() != "")
                Param1ComboBox.SelectedItem = (((string)feature.param1).Trim());
        }
    }
}
