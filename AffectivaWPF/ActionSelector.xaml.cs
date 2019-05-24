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
            ThresholdValue.LostFocus += ThresholdValue_LostFocus;
            ActivationTime.LostFocus += ActivationTime_LostFocus;
            Param1.LostFocus += Param1_LostFocus;
            Param2.LostFocus += Param2_LostFocus;
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
            feature.ActionName = (String) FeatureActionBox.SelectedItem;
        }
    }
}
