using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Linq;

namespace HelloAffectiva{

    public partial class VideoForm : Form, Affdex.ImageListener, Affdex.ProcessStatusListener
    {

        private float process_last_timestamp = -1.0f; //Needed to compute the FPS
        private float process_fps = -1.0f;
        private Dictionary<String, AffectivaAction> actions = new Dictionary<String, AffectivaAction>();

        private Bitmap img { get; set; } //Last BMP img
        private Dictionary<int, Affdex.Face> faces { get; set; } //List of recognized faces
        private Affdex.Detector detector { get; set; } //The detector used to detect faces
        private ReaderWriterLock rwLock { get; set; } //Multithreading
        public VideoForm(Affdex.Detector detector)
        {
            System.Console.WriteLine("Starting Interface...");
            this.detector = detector;
            detector.setImageListener(this);
            detector.setProcessStatusListener(this);
            System.Console.WriteLine("Initializing form component...");
            InitializeComponent();
            rwLock = new ReaderWriterLock();
            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

            //I tried to create this DIctionary of string values and functions so that we can associate a function for each emotions/expressopn/emoji
            Func<double, bool> smileIncreaseVol = (value) =>
            {
                if (value > 50)
                {
                    System.Console.WriteLine("TESING3");
                    //HookActions.hideAllWindows();
                    HookActions.SendMessageW(base.Handle, WmCommand.WM_APPCOMMAND, base.Handle, (IntPtr)AppCommand.APPCOMMAND_VOLUME_UP);//this.Handle represents the system handle
                    return true;
                }

                return false;
            };
            AffectivaAction smileAction = new AffectivaAction(50, smileIncreaseVol);

            Func<double, bool> sadDecreaseVol = (value) =>
            {
                if (value > 50)
                {
                    System.Console.WriteLine("TESING3");
                    //HookActions.hideAllWindows();
                    HookActions.SendMessageW(base.Handle, WmCommand.WM_APPCOMMAND, base.Handle, (IntPtr)AppCommand.APPCOMMAND_VOLUME_DOWN);//this.Handle represents the system handle
                    return true;
                }

                return false;
            };
            AffectivaAction sadAction = new AffectivaAction(50, sadDecreaseVol);

            Func<double, bool> eyesClosedHideWIndows = (value) =>
            {
                if (value > 50)
                {
                    System.Console.WriteLine("EYES CLOSED");
                    HookActions.hideAllWindows();
                    return true;
                }

                return false;
            };
            AffectivaAction eyesCLosedAction = new AffectivaAction(50, eyesClosedHideWIndows);

            label1.Text = Affdex.Emoji.Smiley.ToString().ToLower();
            actions.Add(Affdex.Emoji.Smiley.ToString().ToLower(), smileAction);
            actions.Add(Affdex.Emoji.Disappointed.ToString().ToLower(), sadAction);
            actions.Add("eyeclosure", eyesCLosedAction);
        }
        
        public void onImageCapture(Affdex.Frame frame){
            frame.Dispose();
        }

        public void onImageResults(Dictionary<int, Affdex.Face> faces, Affdex.Frame frame)
        {
            process_fps = 1.0f / (frame.getTimestamp() - process_last_timestamp);
            process_last_timestamp = frame.getTimestamp();
            System.Console.WriteLine(" pfps: {0}", process_fps.ToString());

            byte[] pixels = frame.getBGRByteArray();
            this.img = new Bitmap(frame.getWidth(), frame.getHeight(), PixelFormat.Format24bppRgb);
            var bounds = new Rectangle(0, 0, frame.getWidth(), frame.getHeight());
            BitmapData bmpData = img.LockBits(bounds, ImageLockMode.WriteOnly, img.PixelFormat);
            IntPtr ptr = bmpData.Scan0;

            int data_x = 0;
            int ptr_x = 0;
            int row_bytes = frame.getWidth() * 3;

            // The bitmap requires bitmap data to be byte aligned.
            // http://stackoverflow.com/questions/20743134/converting-opencv-image-to-gdi-bitmap-doesnt-work-depends-on-image-size

            for (int y = 0; y < frame.getHeight(); y++)
            {
                Marshal.Copy(pixels, data_x, ptr + ptr_x, row_bytes);
                data_x += row_bytes;
                ptr_x += bmpData.Stride;
            }
            img.UnlockBits(bmpData);

            this.faces = faces;
            //rwLock.ReleaseWriterLock();

            this.Invalidate();
            frame.Dispose();
        }

        private void DrawResults(Graphics g, Dictionary<int, Affdex.Face> faces){
            System.Console.WriteLine("DrawResults");
            Pen whitePen = new Pen(Color.OrangeRed);
            Pen redPen = new Pen(Color.DarkRed);
            Pen bluePen = new Pen(Color.DarkBlue);
            Font aFont = new Font(FontFamily.GenericSerif, 8, FontStyle.Bold);
            float radius = 2;
            int spacing = 10;
            int left_margin = 30;


            if(faces.Count >= 1)
            {
                System.Console.WriteLine("Faces detected!");

            }
            //For each face;
            
            foreach (KeyValuePair<int, Affdex.Face> pair in faces){
                Affdex.Face face = pair.Value; //Get the face
                bool draw = true;
                System.Console.WriteLine("Drawing Face " + face.Id);

                foreach (Affdex.FeaturePoint fp in face.FeaturePoints){
                    //Draw a point for each of the feature points
                    if (draw)
                        g.DrawCircle(whitePen, fp.X, fp.Y, radius);
                }

                Affdex.FeaturePoint tl = minPoint(face.FeaturePoints); //Top Left feature point
                Affdex.FeaturePoint br = maxPoint(face.FeaturePoints); //Bottom Right feature point
                Affdex.FeaturePoint tr = new Affdex.FeaturePoint();
                tr.Y = tl.Y;
                tr.X = br.X;
                Affdex.FeaturePoint bl = new Affdex.FeaturePoint();
                bl.Y = br.Y;
                bl.X = tl.X;

                int padding = (int)tl.Y;

                //It was drawing from the bottom right before, but I think it's better to print infos
                //Starting from the top right, but it can now easily be changed by changing the value below here
                Affdex.FeaturePoint drawingPoint = tr; //It was br before

                if (draw) {
                    g.DrawString(String.Format("ID: {0}", pair.Key), aFont, whitePen.Brush, new PointF(drawingPoint.X, padding += spacing));
                    g.DrawString("APPEARANCE", aFont, bluePen.Brush, new PointF(drawingPoint.X, padding += (spacing * 2)));
                    g.DrawString(face.Appearance.Gender.ToString(), aFont, whitePen.Brush, new PointF(drawingPoint.X, padding += spacing));
                    g.DrawString(face.Appearance.Age.ToString(), aFont, whitePen.Brush, new PointF(drawingPoint.X, padding += spacing));
                    g.DrawString(face.Appearance.Ethnicity.ToString(), aFont, whitePen.Brush, new PointF(drawingPoint.X, padding += spacing));
                    g.DrawString("Glasses: " + face.Appearance.Glasses.ToString(), aFont, whitePen.Brush, new PointF(drawingPoint.X, padding += spacing));

                    g.DrawString("EMOJIs", aFont, bluePen.Brush, new PointF(drawingPoint.X, padding += (spacing * 2)));
                    g.DrawString("DominantEmoji: " + face.Emojis.dominantEmoji.ToString(), aFont,
                                 (face.Emojis.dominantEmoji != Affdex.Emoji.Unknown) ? whitePen.Brush : redPen.Brush,
                                 new PointF(drawingPoint.X, padding += spacing));
                }

                System.Console.WriteLine("Drawing Face " + face.Id + " Emojis");
                foreach (String emojiName in Enum.GetNames(typeof(Affdex.Emoji))){
                    PropertyInfo prop = face.Emojis.GetType().GetProperty(emojiName.ToLower());
                    if (prop != null){
                        float value = (float)prop.GetValue(face.Emojis, null);
                        if (actions.ContainsKey(emojiName.ToLower()))
                        {
                            actions[emojiName.ToLower()].executeAction(value);
                        }
                        string c = String.Format("{0}: {1:0.00}", emojiName, value);
                        if (draw){
                            g.DrawString(c, aFont, (value > 50) ? whitePen.Brush : redPen.Brush, new PointF(drawingPoint.X, padding += spacing));
                        }
                    }
                }

                System.Console.WriteLine("Drawing Face " + face.Id + " Expressions");
                if (draw)
                    g.DrawString("EXPRESSIONS", aFont, bluePen.Brush, new PointF(drawingPoint.X, padding += (spacing * 2)));
                foreach (PropertyInfo prop in typeof(Affdex.Expressions).GetProperties()){
                    float value = (float)prop.GetValue(face.Expressions, null);
                    String c = String.Format("{0}: {1:0.00}", prop.Name, value);
                    if (actions.ContainsKey(prop.Name.ToLower()))
                    {
                        actions[prop.Name.ToLower()].executeAction(value);
                    }
                    if (draw)
                    {
                        g.DrawString(c, aFont, (value > 50) ? whitePen.Brush : redPen.Brush, new PointF(drawingPoint.X, padding += spacing));
                    }
                }

                if (draw)
                    g.DrawString("EMOTIONS", aFont, bluePen.Brush, new PointF(drawingPoint.X, padding += (spacing * 2)));

                System.Console.WriteLine("Drawing Face " + face.Id + " Emotions");
                foreach (PropertyInfo prop in typeof(Affdex.Emotions).GetProperties()){
                    float value = (float)prop.GetValue(face.Emotions, null);
                    String c = String.Format("{0}: {1:0.00}", prop.Name, value);
                    if (draw)
                    {
                        g.DrawString(c, aFont, (value > 50) ? whitePen.Brush : redPen.Brush, new PointF(drawingPoint.X, padding += spacing));
                    }
                }

            }
        }

        public void onProcessingException(Affdex.AffdexException A_0){
            System.Console.WriteLine("Encountered an exception while processing {0}", A_0.ToString());
        }

        public void onProcessingFinished(){
            System.Console.WriteLine("Processing finished successfully");
        }

        Affdex.FeaturePoint minPoint(Affdex.FeaturePoint[] points){
            Affdex.FeaturePoint ret = points[0];
            foreach (Affdex.FeaturePoint point in points)
            {
                if (point.X < ret.X) ret.X = point.X;
                if (point.Y < ret.Y) ret.Y = point.Y;
            }
            return ret;
        }

        Affdex.FeaturePoint maxPoint(Affdex.FeaturePoint[] points){
            Affdex.FeaturePoint ret = points[0];
            foreach (Affdex.FeaturePoint point in points)
            {
                if (point.X > ret.X) ret.X = point.X;
                if (point.Y > ret.Y) ret.Y = point.Y;
            }
            return ret;
        }

        [HandleProcessCorruptedStateExceptions]
        protected override void OnPaint(PaintEventArgs e) {
            System.Console.WriteLine("OnPaint");
            try{
                // rwLock.AcquireReaderLock(Timeout.Infinite);
                if (img != null){
                    this.Width = img.Width;
                    this.Height = img.Height;
                    e.Graphics.DrawImage((Image)img, new Point(0, 0));
                }

                if (faces != null) DrawResults(e.Graphics, faces);


                e.Graphics.Flush();
            }
            catch (System.AccessViolationException exp){
                System.Console.WriteLine("Encountered AccessViolationException.");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }

    public static class GraphicsExtensions{
        public static void DrawCircle(this Graphics g, Pen pen, float centerX, float centerY, float radius){
            g.DrawEllipse(pen, centerX - radius, centerY - radius, radius + radius, radius + radius);
        }
    }

}
