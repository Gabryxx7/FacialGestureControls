using Notifications.Wpf;
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
        public object param1 = 0;
        public object param2 = "";
        public float currentValue = 0;
        public ActionSelector actionControl;
        public Stopwatch timer = new Stopwatch();

        public AffectivaFeature(FeatureType pType, String affName, String pName)
        {
            this.Type = pType;
            this.AffectivaName = affName;
            this.Name = pName;
            this.timer.Start();
        }

        public AffectivaFeature(FeatureType pType, String affName, String pName, String actionName) : this(pType, affName, pName)
        {
            this.ActionName = actionName;
        }

        public AffectivaFeature(FeatureType pType, String affName, String pName, String actionName, float threshold) : this(pType, affName, pName, actionName)
        {
            this.threshold = threshold;
        }

        public AffectivaFeature(FeatureType pType, String affName, String pName, String actionName, float threshold, float activationTime) : this(pType, affName, pName, actionName, threshold)
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
        public Dictionary<String, Func<IntPtr, object, object, object>> actionsFunction = new Dictionary<String, Func<IntPtr, object, object, object>>();
        public List<AffectivaFeature> featuresActions = new List<AffectivaFeature>();
        private static AffectivaActions instance = null;
        private NotificationManager notificationManager = new NotificationManager();

        public static AffectivaActions getInstance()
        {
            if(instance == null){
                return new AffectivaActions();
            }
            return instance;
        }
        private AffectivaActions()
        {
            actionsFunction.Add("VolumeUp", (handle, param1, param2) =>
            {
                Console.WriteLine("VOLUME UP");
                HookActions.SendMessage(handle, WmCommand.WM_APPCOMMAND, handle, (IntPtr)AppCommand.APPCOMMAND_VOLUME_UP);
                return true;
            });

            actionsFunction.Add("VolumeDown", (handle, param1, param2) =>
            {
                Console.WriteLine("VOLUME DOWN");
                HookActions.SendMessage(handle, WmCommand.WM_APPCOMMAND, handle, (IntPtr)AppCommand.APPCOMMAND_VOLUME_DOWN);
                return true;
            });

            actionsFunction.Add("hideAllWindows", (handle, param1, param2) =>
            {
                Console.WriteLine("HIDE WINDOWS");
                IntPtr OutResult;
                IntPtr lHwnd = HookActions.FindWindow("Shell_TrayWnd", null);
                HookActions.SendMessageTimeout(lHwnd, WmCommand.WM_COMMAND, (IntPtr)WmCommand.MIN_ALL, IntPtr.Zero, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 2000, out OutResult);
                System.Threading.Thread.Sleep(2000);
                HookActions.SendMessageTimeout(lHwnd, WmCommand.WM_COMMAND, (IntPtr)WmCommand.MIN_ALL_UNDO, IntPtr.Zero, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 2000, out OutResult);
                return true;
            });


            actionsFunction.Add("OpenApplication", (handle, param1, param2) =>
            {
                string param = "";
                try
                {
                    param = (string)param1;
                }
                catch (Exception e1)
                {
                    try
                    {
                        param = (string)param2;
                    }
                    catch (Exception e2) { }
                }

                if (param != "")
                {
                    Console.WriteLine("Opening {0}", param);
                    HookActions.OpenApplication(param);
                }
                return true;
            });

            actionsFunction.Add("TakeScreenshot", (handle, param1, param2) =>
            {
                //C:\Users\marinig\Desktop
                string param = "";
                try
                {
                    param = (string)param1;
                }
                catch (Exception e1)
                {
                    try
                    {
                        param = (string)param2;
                    }
                    catch (Exception e2) { }
                }
                //IF empty it will just take the screenshot in the folder where the .exe is
                Console.WriteLine("Taking Screenshot {0}", param);
                string filename = HookActions.TakeScreenshot(param);
                notificationManager.Show(new NotificationContent
                {
                    Title = "Took Screenshot",
                    Message = "Stored in " + filename,
                    Type = NotificationType.Information
                });
                return true;

            });

            actionsFunction.Add("OpenBrowser", (handle, param1, param2) =>
            {
                //https://www.youtube.com/watch?v=Sagg08DrO5U
                string param = "";
                try
                {
                    param = (string)param1;
                }
                catch (Exception e1)
                {
                    try
                    {
                        param = (string)param2;
                    }
                    catch (Exception e2) { }
                }

                if (param != "")
                {
                    Console.WriteLine("Opening Browser {0}", param);
                    HookActions.OpenBrowser(param);
                }
                return true;

            });


            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji,"scream", "Scream"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji,"flushed", "Flushed"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji,"stuckOutTongue", "StuckOutTongue"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji,"stuckOutTongueWinkingEye", "StuckOutTongueWinkingEye"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji,"wink", "Wink"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji,"smirk", "Smirk"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji,"rage", "Rage"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji, "disappointed", "Disappointed"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji, "kissing", "Kissing"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji, "laughing", "Laughing"));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji, "smiley", "Smiley"));
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
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "EyeClosure", "EyeClosure"));
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
                            if (feature.AffectivaName == csv.GetField<string>("AffectivaName"))
                            {
                                try
                                {
                                    feature.Type = (AffectivaFeature.FeatureType) Enum.Parse(typeof(AffectivaFeature.FeatureType), csv.GetField<string>("Type"));
                                }
                                catch (Exception e)
                                {
                                    feature.Type = (AffectivaFeature.FeatureType)csv.GetField<int>("Type");
                                }
                                feature.AffectivaName = csv.GetField<string>("AffectivaName");
                                feature.Name = csv.GetField<string>("Name");
                                feature.ActionName = csv.GetField<string>("ActionName");
                                feature.threshold = csv.GetField<float>("threshold");
                                feature.activationTime = csv.GetField<float>("activationTime");
                                feature.param1 = csv.GetField<string>("param1");
                                feature.param2 = csv.GetField<string>("param2");
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
                    csv.WriteField("Type");
                    csv.WriteField("AffectivaName");
                    csv.WriteField("Name");
                    csv.WriteField("ActionName");
                    csv.WriteField("threshold");
                    csv.WriteField("activationTime");
                    csv.WriteField("param1");
                    csv.WriteField("param2");
                    csv.NextRecord();
                    foreach (AffectivaFeature feature in featuresActions)
                    {
                        csv.WriteField(feature.Type);
                        csv.WriteField(feature.AffectivaName);
                        csv.WriteField(feature.Name);
                        csv.WriteField(feature.ActionName);
                        csv.WriteField(feature.threshold);
                        csv.WriteField(feature.activationTime);
                        csv.WriteField(feature.param1);
                        csv.WriteField(feature.param2);
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
