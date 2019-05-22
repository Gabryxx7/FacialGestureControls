using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAffectiva
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            System.Console.WriteLine("Trying to process a camera feed...");
            Affdex.CameraDetector detector = new Affdex.CameraDetector(0, 30, 30, 3, Affdex.FaceDetectorMode.LARGE_FACES);

            if (detector != null) {
                VideoForm videoForm = new VideoForm(detector);
                detector.setClassifierPath("C:\\Users\\marinig\\Documents\\AffectivaSDK\\AffdexSDK\\data");
                //detector.setClassifierPath("C:\\Program Files\\Affectiva\\AffdexSDK\\data");
                detector.setDetectAllEmotions(true);
                detector.setDetectAllEmojis(true);
                detector.setDetectAllExpressions(true);
                detector.setDetectAllAppearances(true);
                detector.setDetectBrowRaise(true);
                detector.setDetectSmile(true);
                detector.setDetectGender(true);
                detector.setDetectGlasses(true);
                detector.start();
                System.Console.WriteLine("Face detector mode = " + detector.getFaceDetectorMode().ToString());

                videoForm.ShowDialog();
                detector.stop();
            }
        }
    }
}
 