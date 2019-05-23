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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AffdexMe
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ActionSelector : UserControl
    {
        AffectivaFeature feature;
        public ActionSelector()
        {
            InitializeComponent();
        }
        
        public ActionSelector(AffectivaFeature feature, Dictionary<String, Func<IntPtr, bool>> actionsFunction) : this()
        {
            this.feature = feature;
            FeatureName.Content = feature.Name;
            ThresholdValue.Text = "" + feature.threshold;
            ActivationTime.Text = "" + feature.activationTime;
            FeatureActionBox.Items.Add("");
            foreach (String action in actionsFunction.Keys)
            {
                FeatureActionBox.Items.Add(action);
            }
            if(feature.ActionName.Trim() != "")
                FeatureActionBox.SelectedItem = feature.ActionName;

            FeatureActionBox.SelectionChanged += FeatureActionBox_SelectedChanged;
            ThresholdValue.LostFocus += ThresholdValue_LostFocus;
            ActivationTime.LostFocus += ActivationTime_LostFocus;
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
            feature.ActionName = (String) FeatureActionBox.SelectedItem;
        }
    }
}
