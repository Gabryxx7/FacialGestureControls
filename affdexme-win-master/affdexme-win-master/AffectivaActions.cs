using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;

namespace AffdexMe
{
    public class AffectivaFeature : IEquatable<String>
    {
        public enum FeatureType
        {
            Emoji = 0,
            Expression = 1,
            Emotion = 2,
            Appearance = 3
        }

        public FeatureType Type;
        public String Name = "" ;
        public String AffectivaName = "";
        public String ActionName = "";
        public float activationTime = 0;
        public float threshold = 0;
        public Stopwatch timer = new Stopwatch();

        public AffectivaFeature(FeatureType pType, String pName, String affName)
        {
            this.Type = pType;
            this.Name = pName;
            this.AffectivaName = affName;
            this.timer.Start();
        }

        public AffectivaFeature(FeatureType pType, String pName, String affName, String actionName) : this(pType, pName, affName)
        {
            this.ActionName = actionName;
        }

        public AffectivaFeature(FeatureType pType, String pName, String affName, String actionName, float threshold) : this(pType, pName, affName, actionName)
        {
            this.threshold = threshold;
        }

        public AffectivaFeature(FeatureType pType, String pName, String affName, String actionName, float threshold, float activationTime) : this(pType, pName, affName, actionName, threshold)
        {
            this.activationTime = activationTime;
        }

        public bool Equals(string other)
        {
            return AffectivaName.Equals(other);
        }
    }
    public class AffectivaActions
    {        
        public Dictionary<String, Func<IntPtr, bool>> actionsFunction = new Dictionary<String, Func<IntPtr, bool>>();
        public List<AffectivaFeature> featuresActions = new List<AffectivaFeature>();
        private static AffectivaActions instance = null;

       public static AffectivaActions getInstance()
        {
            if(instance == null){
                return new AffectivaActions();
            }
            return instance;
        }
        private AffectivaActions()
        {
            actionsFunction.Add("VolumeUp", (handle) =>
            {
                Console.WriteLine("VOLUME UP");
                // var handle = (new WindowInteropHelper(this)).Handle;
                HookActions.SendMessage(handle, WmCommand.WM_APPCOMMAND, handle, (IntPtr)AppCommand.APPCOMMAND_VOLUME_UP);
                return true;
            });

            actionsFunction.Add("VolumeDown", (handle) =>
            {
                Console.WriteLine("VOLUME DOWN");
                HookActions.SendMessage(handle, WmCommand.WM_APPCOMMAND, handle, (IntPtr)AppCommand.APPCOMMAND_VOLUME_DOWN);
                return true;
            });

            actionsFunction.Add("hideAllWindows", (handle) =>
            {
                Console.WriteLine("HIDE WINDOWS");
                IntPtr OutResult;
                IntPtr lHwnd = HookActions.FindWindow("Shell_TrayWnd", null);
                HookActions.SendMessageTimeout(lHwnd, WmCommand.WM_COMMAND, (IntPtr)WmCommand.MIN_ALL, IntPtr.Zero, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 2000, out OutResult);
                System.Threading.Thread.Sleep(2000);
                HookActions.SendMessageTimeout(lHwnd, WmCommand.WM_COMMAND, (IntPtr)WmCommand.MIN_ALL_UNDO, IntPtr.Zero, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 2000, out OutResult);
                return true;
            });


            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji,"scream", "Scream"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji,"flushed", "Flushed"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji,"stuckOutTongue", "StuckOutTongue"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji,"stuckOutTongueWinkingEye", "StuckOutTongueWinkingEye"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji,"wink", "Wink"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji,"smirk", "Smirk"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji,"rage", "Scream"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji, "disappointed", "Disappointed", "VolumeDown", 90));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji, "kissing", "Kissing"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji, "laughing", "Laughing"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji, "smiley", "Smiley", "VolumeUp", 70));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji, "relaxed", "Relaxed"));

            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "BrowRaise", "BrowRaise"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "BrowFurrow", "BrowFurrow"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "NoseWrinkle", "NoseWrinkle"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "UpperLipRaise", "UpperLipRaise"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "LipCornerDepressor", "LipCornerDepressor"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "ChinRaise", "ChinRaise"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "LipPucker", "LipPucker"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "LipPress", "LipPress"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "LipSuck", "LipSuck"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "MouthOpen", "MouthOpen"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "Smirk", "Smirk"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "EyeClosure", "EyeClosure", "", 50, 2000));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "Attention", "Attention"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "EyeWiden", "EyeWiden"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "CheekRaise", "CheekRaise"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "LidTighten", "LidTighten"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "Dimpler", "Dimpler"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "LipStretch", "LipStretch"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "JawDrop", "JawDrop"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "InnerBrowRaise", "InnerBrowRaise"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "Smile", "Smile"));

            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emotion, "Engagement", "Engagement"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emotion, "Valence", "Valence"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emotion, "Contempt", "Contempt"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emotion, "Surprise", "Surprise"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emotion, "Anger", "Anger"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emotion, "Sadness", "Sadness"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emotion, "Disgust", "Disgust"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emotion, "Fear", "Fear"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emotion, "Joy", "Joy"));

            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Appearance, "Glasses", "Glasses"));

            UpdateFromFile();
        }

        private void UpdateFromFile()
        {
            try
            {
                using (var reader = new StreamReader("data.csv"))
                using (var csv = new CsvHelper.CsvReader(reader))
                {
                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        foreach (AffectivaFeature feature in featuresActions)
                        {
                            if (feature.AffectivaName == csv.GetField<string>("name"))
                            {
                                feature.threshold = csv.GetField<float>("threshold");
                                feature.activationTime = csv.GetField<float>("activationTime");
                                feature.ActionName = csv.GetField<string>("action");
                                break;
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("FILE NOT FOUND: " + e.Message);
            }
        }

        public void WriteToFile()
        {
            try
            {
                using (var writer = new StreamWriter("data.csv"))
                using (var csv = new CsvHelper.CsvWriter(writer))
                {
                    csv.WriteField("name");
                    csv.WriteField("action");
                    csv.WriteField("threshold");
                    csv.WriteField("activationTime");
                    csv.NextRecord();
                    foreach (AffectivaFeature feature in featuresActions)
                    {
                        csv.WriteField(feature.AffectivaName);
                        csv.WriteField(feature.ActionName);
                        csv.WriteField(feature.threshold);
                        csv.WriteField(feature.activationTime);
                        csv.NextRecord();
                    }
                    csv.Flush();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("CANNOT WRITE FILE: " + e.Message);
            }

        }

    }
}
