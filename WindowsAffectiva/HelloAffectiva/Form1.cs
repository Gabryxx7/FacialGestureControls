using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Affdex;
using System.Reflection;

namespace HelloAffectiva
{
    public partial class Form1 : Form, Affdex.ImageListener
    {
        public Form1(Affdex.Detector detector)
        {
            detector.setImageListener(this);

            InitializeComponent();
        }

        public void onImageCapture(Frame frame)
        {
            frame.Dispose();
        }

        public void onImageResults(Dictionary<int, Face> faces, Frame frame)
        {
            foreach (KeyValuePair<int, Affdex.Face> pair in faces)
            {
                Affdex.Face face = pair.Value;
                if(face!=null)
                {
                    foreach(PropertyInfo prop in typeof(Affdex.Emotions).GetProperties())
                    {
                        float Value = (float)prop.GetValue(face.Emotions, null);
                        string output = string.Format("{0}: {1:0.00}", prop.Name, Value);
                        System.Console.WriteLine(output);
                    }
                }
            }
            frame.Dispose();       
        }
    }
}
