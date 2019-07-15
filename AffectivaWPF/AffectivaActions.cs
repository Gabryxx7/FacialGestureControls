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
        public float activationTime = 1;
        public float threshold = 50;
        public object param1 = "";
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
            actionsFunction.Add("BrightnessDown", (handle, param1, param2) =>
            {
                Console.WriteLine("BRIGHTNESS DOWN");
                HookActions.BrightnessDown(handle);
                return true;
            });

            actionsFunction.Add("BrightnessUp", (handle, param1, param2) =>
            {
                Console.WriteLine("BRIGHTNESS UP");
                HookActions.BrightnessUp(handle);
                return true;
            });

            actionsFunction.Add("VolumeUp", (handle, param1, param2) =>
            {
                Console.WriteLine("VOLUME UP");
                HookActions.IncreaseSystemVolume(handle);
                return true;
            });

            actionsFunction.Add("VolumeDown", (handle, param1, param2) =>
            {
                Console.WriteLine("VOLUME DOWN");
                HookActions.DecreaseSystemVolume(handle);
                return true;
            });


            actionsFunction.Add("VolumeMute", (handle, param1, param2) =>
            {
                Console.WriteLine("VOLUME MUTE");
                HookActions.MuteSystemVolume(handle);
                return true;
            });

            actionsFunction.Add("MediaPlay", (handle, param1, param2) =>
            {
                Console.WriteLine("MEDIA PLAY");
                HookActions.SystemMediaPlay(handle);
                return true;
            });

            actionsFunction.Add("MediaPause", (handle, param1, param2) =>
            {
                Console.WriteLine("MEDIA PAUSE");
                HookActions.SystemMediaPause(handle);
                return true;
            });

            actionsFunction.Add("MediaStop", (handle, param1, param2) =>
            {
                Console.WriteLine("MEDIA STOP");
                HookActions.SystemMediaStop(handle);
                return true;
            });

            actionsFunction.Add("MediaPlayPause", (handle, param1, param2) =>
            {
                Console.WriteLine("MEDIA PLAY/PAUSE");
                HookActions.SystemMediaPlayPause(handle);
                return true;
            });


            actionsFunction.Add("KeyboardPress", (handle, param1, param2) =>
            {

                string keyCode = "";
                try
                {
                    keyCode = (string)param1;
                }
                catch (Exception e1)
                {
                    try
                    {
                        keyCode = (string)param2;
                    }
                    catch (Exception e2) { }
                }

                Console.WriteLine("Keyboard Press " + keyCode);
                HookActions.KeyboardPress(handle, keyCode);
                return true;
            });


            actionsFunction.Add("hideAllWindows", (handle, param1, param2) =>
            {
                Console.WriteLine("HIDE WINDOWS");
                HookActions.hideAllWindows();
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

                Console.WriteLine("Opening {0}", param);
                HookActions.OpenApplication(param);
                return true;
            });

            actionsFunction.Add("TakeScreenshot", (handle, param1, param2) =>
            {
                //C:\Users\marinig\Desktop
                string folderPath = "";
                try
                {
                    folderPath = (string)param1;
                }
                catch (Exception e1)
                {
                    try
                    {
                        folderPath = (string)param2;
                    }
                    catch (Exception e2) { }
                }
                //IF empty it will just take the screenshot in the folder where the .exe is
                Console.WriteLine("Taking Screenshot {0}", folderPath);
                string filename = @"ScreenShot-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".jpg";
                if (HookActions.TakeScreenshot(folderPath, filename))
                {
                    notificationManager.Show(new NotificationContent
                    {
                        Title = "Screenshot Saved",
                        Message = "Stored in " + folderPath+filename,
                        Type = NotificationType.Information
                    });
                }
                else
                {
                    HookActions.TakeScreenshot("", filename);
                    notificationManager.Show(new NotificationContent
                    {
                        Title = "Screenshot Error",
                        Message = "Could not find the folder:" + folderPath +"\nScreenshot saved to program folder ",
                        Type = NotificationType.Error
                    });
                }
                return true;

            });

            actionsFunction.Add("OpenBrowser", (handle, param1, param2) =>
            {
                //https://www.youtube.com/watch?v=Sagg08DrO5U
                string url = "";
                try
                {
                    url = (string)param1;
                }
                catch (Exception e1)
                {
                    try
                    {
                        url = (string)param2;
                    }
                    catch (Exception e2) { }
                }

                Console.WriteLine("Opening Browser {0}", url);
                if (HookActions.OpenBrowser(url))
                {
                    notificationManager.Show(new NotificationContent
                    {
                        Title = "Opening Browser",
                        Message = "Opening url " + url,
                        Type = NotificationType.Information
                    });
                }
                else
                {
                    notificationManager.Show(new NotificationContent
                    {
                        Title = "Error opening Browser",
                        Type = NotificationType.Error
                    });
                }
                return false;
            });


            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji,"scream", "Scream", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji,"flushed", "Flushed", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji,"stuckOutTongue", "StuckOutTongue", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji,"stuckOutTongueWinkingEye", "StuckOutTongueWinkingEye", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji,"wink", "Wink", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji,"smirk", "Smirk", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji,"rage", "Rage", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji, "disappointed", "Disappointed", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji, "kissing", "Kissing", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji, "laughing", "Laughing", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji, "smiley", "Smiley", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emoji, "relaxed", "Relaxed", "", 50, 1));

            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "BrowRaise", "BrowRaise", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "BrowFurrow", "BrowFurrow", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "NoseWrinkle", "NoseWrinkle", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "UpperLipRaise", "UpperLipRaise", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "LipCornerDepressor", "LipCornerDepressor", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "ChinRaise", "ChinRaise", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "LipPucker", "LipPucker", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "LipPress", "LipPress", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "LipSuck", "LipSuck", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "MouthOpen", "MouthOpen", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "Smirk", "Smirk", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "EyeClosure", "EyeClosure", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "Attention", "Attention", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "EyeWiden", "EyeWiden", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "CheekRaise", "CheekRaise", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "LidTighten", "LidTighten", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "Dimpler", "Dimpler", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "LipStretch", "LipStretch", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "JawDrop", "JawDrop", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "InnerBrowRaise", "InnerBrowRaise", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Expression, "Smile", "Smile", "", 50, 1));

            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emotion, "Engagement", "Engagement", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emotion, "Valence", "Valence", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emotion, "Contempt", "Contempt", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emotion, "Surprise", "Surprise", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emotion, "Anger", "Anger", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emotion, "Sadness", "Sadness", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emotion, "Disgust", "Disgust", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emotion, "Fear", "Fear", "", 50, 1));
            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Emotion, "Joy", "Joy", "", 50, 1));

            featuresActions.Add(new AffectivaFeature(AffectivaFeature.FeatureType.Appearance, "Glasses", "Glasses", "", 50, 1));

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
