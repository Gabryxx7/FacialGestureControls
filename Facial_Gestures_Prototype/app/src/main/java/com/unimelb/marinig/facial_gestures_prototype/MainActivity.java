package com.unimelb.marinig.facial_gestures_prototype;

import android.Manifest;
import android.animation.Animator;
import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.AlertDialog;
import android.app.Dialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.pm.PackageManager;
import android.os.CountDownTimer;
import android.os.Handler;
import android.support.design.widget.Snackbar;
import android.support.v4.app.ActivityCompat;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.animation.AlphaAnimation;
import android.view.animation.Animation;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.airbnb.lottie.LottieAnimationView;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.GoogleApiAvailability;
import com.google.android.gms.vision.CameraSource;
import com.google.android.gms.vision.MultiProcessor;
import com.google.android.gms.vision.Tracker;
import com.google.android.gms.vision.face.Face;
import com.google.android.gms.vision.face.FaceDetector;
import com.unimelb.marinig.facial_gestures_prototype.camera.CameraSourcePreview;
import com.unimelb.marinig.facial_gestures_prototype.camera.GraphicOverlay;

import java.io.IOException;

import io.netopen.hotbitmapgg.library.view.RingProgressBar;

@SuppressLint("MissingPermission")
public class MainActivity extends AppCompatActivity {
    private static final String TAG = "FaceTracker";

    private CameraSource mCameraSource = null;

    private CameraSourcePreview mPreview;
    private GraphicOverlay mGraphicOverlay;

    private static final int RC_HANDLE_GMS = 9001;
    // permission request codes need to be < 256
    private static final int RC_HANDLE_CAMERA_PERM = 2;
    private TextView mFinalMessage;
    RingProgressBar mRingProgressBar;
    TextView mCounterText;
    LinearLayout mCameraContainer;
    final private int animTimeMillis = 1000;
    final private int animStartTime = 500;
    boolean animStarted = false;
    private Button mRestartBtn;
    private boolean mCounterStarted = false;
    private boolean mLeftEyeOpen = false;
    private LottieAnimationView checkMarkAnim;
    private LottieAnimationView emojiAnim;
    private LottieAnimationView shakeAnim;

    Animation out;
    Animation outStart;
    Animation in;
    CountDownTimer timer;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        checkMarkAnim = findViewById(R.id.check_mark_anim);
        emojiAnim = findViewById(R.id.emoji_wink_anim);
        shakeAnim = findViewById(R.id.shake_anim);
        mCameraContainer = (LinearLayout) findViewById(R.id.cameraContainer);
        mPreview = (CameraSourcePreview) findViewById(R.id.preview);
        mGraphicOverlay = (GraphicOverlay) findViewById(R.id.faceOverlay);
        mRingProgressBar = (RingProgressBar) findViewById(R.id.progress_bar);
        mCounterText = (TextView) findViewById(R.id.counterText);
        mFinalMessage = (TextView) findViewById(R.id.finalText);

        mRestartBtn = (Button) findViewById(R.id.restartButton);
        mRestartBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                restartCounter(3, true, mRingProgressBar, mCounterText);
            }
        });

        int rc = ActivityCompat.checkSelfPermission(getApplicationContext(), Manifest.permission.CAMERA);
        if (rc == PackageManager.PERMISSION_GRANTED) {
            createCameraSource();
        } else {
            requestCameraPermission();
        }

        //smileUnlock();
        winkUnlock();
        //shakeUnlock();
    }

    public void shakeUnlock(){
        mFinalMessage.setText("Shake your phone!");
        mFinalMessage.setVisibility(View.VISIBLE);
        mRestartBtn.setVisibility(View.GONE);
        shakeAnim.setVisibility(View.VISIBLE);
        emojiAnim.setVisibility(View.GONE);
        checkMarkAnim.setVisibility(View.GONE);
        mCounterText.setVisibility(View.GONE);
        mRingProgressBar.setVisibility(View.GONE);

        in = new AlphaAnimation(0.0f, 1.0f);
        in.setDuration(animTimeMillis);

        outStart = new AlphaAnimation(1.0f, 0.0f);
        outStart.setDuration(animTimeMillis);
        outStart.setAnimationListener(new Animation.AnimationListener() {
            @Override
            public void onAnimationStart(Animation animation) {

            }

            @Override
            public void onAnimationEnd(Animation animation) {
                mFinalMessage.setVisibility(View.VISIBLE);
                mFinalMessage.startAnimation(in);
                mFinalMessage.setText("Facial gestures enabled!");
                checkMarkAnim.setVisibility(View.VISIBLE);
                checkMarkAnim.startAnimation(in);
                checkMarkAnim.playAnimation();
                shakeAnim.setVisibility(View.GONE);
            }

            @Override
            public void onAnimationRepeat(Animation animation) {

            }
        });

        new Handler().postDelayed(new Runnable() {
            public void run() {
                shakeAnim.playAnimation();
                shakeAnim.addAnimatorListener(new Animator.AnimatorListener() {
                    @Override
                    public void onAnimationStart(Animator animation) {

                    }

                    @Override
                    public void onAnimationEnd(Animator animation) {
                        new Handler().postDelayed(new Runnable() {
                            public void run() {
                                shakeAnim.startAnimation(outStart);
                            }
                        }, 1000);
                    }

                    @Override
                    public void onAnimationCancel(Animator animation) {

                    }

                    @Override
                    public void onAnimationRepeat(Animator animation) {

                    }
                });

            }
        }, 1000);

    }

    public void smileUnlock(){
        restartCounter(3, true, mRingProgressBar, mCounterText);
        mFinalMessage.setText("Smile!");
        mFinalMessage.setVisibility(View.VISIBLE);
        mRestartBtn.setVisibility(View.GONE);
        emojiAnim.setVisibility(View.GONE);
        shakeAnim.setVisibility(View.GONE);
        mCounterText.setVisibility(View.VISIBLE);
        mRingProgressBar.setVisibility(View.VISIBLE);
    }


    public void winkUnlock(){
        mFinalMessage.setText("Wink Twice!");
        mFinalMessage.setVisibility(View.VISIBLE);
        mRestartBtn.setVisibility(View.GONE);
        emojiAnim.setVisibility(View.VISIBLE);
        shakeAnim.setVisibility(View.GONE);
        checkMarkAnim.setVisibility(View.GONE);
        mCounterText.setVisibility(View.GONE);
        mRingProgressBar.setVisibility(View.GONE);

        in = new AlphaAnimation(0.0f, 1.0f);
        in.setDuration(animTimeMillis);

        outStart = new AlphaAnimation(1.0f, 0.0f);
        outStart.setDuration(animTimeMillis);
        outStart.setAnimationListener(new Animation.AnimationListener() {
            @Override
            public void onAnimationStart(Animation animation) {

            }

            @Override
            public void onAnimationEnd(Animation animation) {
                mFinalMessage.setVisibility(View.VISIBLE);
                mFinalMessage.startAnimation(in);
                mFinalMessage.setText("Facial gestures enabled!");
                checkMarkAnim.setVisibility(View.VISIBLE);
                checkMarkAnim.startAnimation(in);
                checkMarkAnim.playAnimation();
                emojiAnim.setVisibility(View.GONE);
            }

            @Override
            public void onAnimationRepeat(Animation animation) {

            }
        });

        new Handler().postDelayed(new Runnable() {
            public void run() {
                emojiAnim.playAnimation();
                emojiAnim.addAnimatorListener(new Animator.AnimatorListener() {
                    @Override
                    public void onAnimationStart(Animator animation) {

                    }

                    @Override
                    public void onAnimationEnd(Animator animation) {
                        new Handler().postDelayed(new Runnable() {
                            public void run() {
                                emojiAnim.startAnimation(outStart);
                            }
                        }, 50);
                    }

                    @Override
                    public void onAnimationCancel(Animator animation) {

                    }

                    @Override
                    public void onAnimationRepeat(Animator animation) {

                    }
                });

            }
        }, 1000);
    }


    public void startCounter(final int seconds, final boolean countDown, final RingProgressBar ringProgressBar, final TextView counterText){
        if(mCounterStarted)
            return;

        mCounterStarted = true;
        out = new AlphaAnimation(1.0f, 0.0f);
        out.setDuration(animTimeMillis);

        in = new AlphaAnimation(0.0f, 1.0f);
        in.setDuration(animTimeMillis);

        outStart = new AlphaAnimation(1.0f, 0.0f);
        outStart.setDuration(animTimeMillis);
        outStart.setAnimationListener(new Animation.AnimationListener() {
            @Override
            public void onAnimationStart(Animation animation) {

            }

            @Override
            public void onAnimationEnd(Animation animation) {
                mRingProgressBar.setVisibility(View.INVISIBLE);
                mCounterText.setVisibility(View.INVISIBLE);
                mFinalMessage.setVisibility(View.VISIBLE);
                mFinalMessage.startAnimation(in);
                mFinalMessage.setText("Facial gestures enabled!");
                checkMarkAnim.setVisibility(View.VISIBLE);
                checkMarkAnim.playAnimation();
            }

            @Override
            public void onAnimationRepeat(Animation animation) {

            }
        });


        // Set the progress bar's progress
        ringProgressBar.setProgress(countDown ? 100 : 0);
        ringProgressBar.setOnProgressListener(new RingProgressBar.OnProgressListener()
        {
            @Override
            public void progressToComplete()
            {
                // Progress reaches the maximum callback default Max value is 100
                //Toast.makeText(MainActivity.this, "complete", Toast.LENGTH_SHORT).show();
            }
        });

        timer = new CountDownTimer(seconds * 1000, 10) {

            public void onTick(long millisUntilFinished) {
                float percentage = (millisUntilFinished / ((float) seconds * 1000.0f));
                percentage = countDown ? percentage: 1-percentage;
                //Toast.makeText(MainActivity.this, "Timer " + millisUntilFinished + " perc: " + percentage, Toast.LENGTH_SHORT).show();
                ringProgressBar.setProgress((int) (percentage * 100));
                counterText.setText(String.valueOf((int) (percentage * seconds) + 1));

                if(millisUntilFinished < animStartTime && !animStarted){
                    animStarted = true;
                    ringProgressBar.startAnimation(out);
                    counterText.startAnimation(outStart);
                }
            }

            public void onFinish() {
                if(!animStarted){
                    animStarted = true;
                    ringProgressBar.startAnimation(out);
                    counterText.startAnimation(outStart);
                }
                ringProgressBar.setProgress(countDown ? 0 : 100);
                counterText.setText(String.valueOf(countDown ? 0 : seconds));
                this.cancel();
            }
        };

        timer.start();

    }

    public void stopCounter(){
        mCounterStarted = false;
        mRingProgressBar.setVisibility(View.VISIBLE);
        mCounterText.setVisibility(View.VISIBLE);
        mFinalMessage.setVisibility(View.INVISIBLE);
        if (out != null)
            out.cancel();
        if (outStart != null)
            outStart.cancel();
        if (in != null)
            in.cancel();
        if (timer != null)
            timer.cancel();
        animStarted = false;
    }

    public void restartCounter(final int seconds, final boolean countDown, final RingProgressBar ringProgressBar, final TextView counterText){
        stopCounter();
        startCounter(seconds, countDown, ringProgressBar, counterText);
    }

    /**
     * Handles the requesting of the camera permission.  This includes
     * showing a "Snackbar" message of why the permission is needed then
     * sending the request.
     */
    private void requestCameraPermission() {
        Log.w(TAG, "Camera permission is not granted. Requesting permission");

        final String[] permissions = new String[]{Manifest.permission.CAMERA};

        if (!ActivityCompat.shouldShowRequestPermissionRationale(this,
                Manifest.permission.CAMERA)) {
            ActivityCompat.requestPermissions(this, permissions, RC_HANDLE_CAMERA_PERM);
            return;
        }

        final Activity thisActivity = this;

        View.OnClickListener listener = new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ActivityCompat.requestPermissions(thisActivity, permissions,
                        RC_HANDLE_CAMERA_PERM);
            }
        };

        Snackbar.make(mGraphicOverlay, R.string.permission_camera_rationale,
                Snackbar.LENGTH_INDEFINITE)
                .setAction(R.string.ok, listener)
                .show();
    }

    /**
     * Creates and starts the camera.  Note that this uses a higher resolution in comparison
     * to other detection examples to enable the barcode detector to detect small barcodes
     * at long distances.
     */
    private void createCameraSource() {

        Context context = getApplicationContext();
        FaceDetector detector = new FaceDetector.Builder(context)
                .setClassificationType(FaceDetector.ACCURATE_MODE)
                .build();

        detector.setProcessor(
                new MultiProcessor.Builder<>(new GraphicFaceTrackerFactory())
                        .build());

        if (!detector.isOperational()) {
            // Note: The first time that an app using face API is installed on a device, GMS will
            // download a native library to the device in order to do detection.  Usually this
            // completes before the app is run for the first time.  But if that download has not yet
            // completed, then the above call will not detect any faces.
            //
            // isOperational() can be used to check if the required native library is currently
            // available.  The detector will automatically become operational once the library
            // download completes on device.
            Log.w(TAG, "Face detector dependencies are not yet available.");
        }

        Log.e("TEST", mCameraContainer.getHeight() +"x" +mCameraContainer.getWidth());

        mCameraSource = new CameraSource.Builder(context, detector)
                .setRequestedPreviewSize(640, 480)
                .setAutoFocusEnabled(true)
                .setFacing(CameraSource.CAMERA_FACING_FRONT)
                .setRequestedFps(30.0f)
                .build();
    }


    /**
     * Restarts the camera.
     */
    @Override
    protected void onResume() {
        super.onResume();

        startCameraSource();
    }

    /**
     * Stops the camera.
     */
    @Override
    protected void onPause() {
        super.onPause();
        mPreview.stop();
    }

    /**
     * Releases the resources associated with the camera source, the associated detector, and the
     * rest of the processing pipeline.
     */
    @Override
    protected void onDestroy() {
        super.onDestroy();
        if (mCameraSource != null) {
            mCameraSource.release();
        }
    }


    /**
     * Callback for the result from requesting permissions. This method
     * is invoked for every call on {@link #requestPermissions(String[], int)}.
     * <p>
     * <strong>Note:</strong> It is possible that the permissions request interaction
     * with the user is interrupted. In this case you will receive empty permissions
     * and results arrays which should be treated as a cancellation.
     * </p>
     *
     * @param requestCode  The request code passed in {@link #requestPermissions(String[], int)}.
     * @param permissions  The requested permissions. Never null.
     * @param grantResults The grant results for the corresponding permissions
     *                     which is either {@link PackageManager#PERMISSION_GRANTED}
     *                     or {@link PackageManager#PERMISSION_DENIED}. Never null.
     * @see #requestPermissions(String[], int)
     */
    @Override
    public void onRequestPermissionsResult(int requestCode, String[] permissions, int[] grantResults) {
        if (requestCode != RC_HANDLE_CAMERA_PERM) {
            Log.d(TAG, "Got unexpected permission result: " + requestCode);
            super.onRequestPermissionsResult(requestCode, permissions, grantResults);
            return;
        }

        if (grantResults.length != 0 && grantResults[0] == PackageManager.PERMISSION_GRANTED) {
            Log.d(TAG, "Camera permission granted - initialize the camera source");
            // we have permission, so create the camerasource
            createCameraSource();
            return;
        }

        Log.e(TAG, "Permission not granted: results len = " + grantResults.length +
                " Result code = " + (grantResults.length > 0 ? grantResults[0] : "(empty)"));

        DialogInterface.OnClickListener listener = new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int id) {
                finish();
            }
        };

        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle("Face Tracker sample")
                .setMessage(R.string.no_camera_permission)
                .setPositiveButton(R.string.ok, listener)
                .show();
    }

    //==============================================================================================
    // Camera Source Preview
    //==============================================================================================

    /**
     * Starts or restarts the camera source, if it exists.  If the camera source doesn't exist yet
     * (e.g., because onResume was called before the camera source was created), this will be called
     * again when the camera source is created.
     */
    private void startCameraSource() {

        // check that the device has play services available.
        int code = GoogleApiAvailability.getInstance().isGooglePlayServicesAvailable(
                getApplicationContext());
        if (code != ConnectionResult.SUCCESS) {
            Dialog dlg =
                    GoogleApiAvailability.getInstance().getErrorDialog(this, code, RC_HANDLE_GMS);
            dlg.show();
        }

        if (mCameraSource != null) {
            try {
                mPreview.start(mCameraSource, mGraphicOverlay);
            } catch (IOException e) {
                Log.e(TAG, "Unable to start camera source.", e);
                mCameraSource.release();
                mCameraSource = null;
            }
        }
    }

    //==============================================================================================
    // Graphic Face Tracker
    //==============================================================================================

    /**
     * Factory for creating a face tracker to be associated with a new face.  The multiprocessor
     * uses this factory to create face trackers as needed -- one for each individual.
     */
    private class GraphicFaceTrackerFactory implements MultiProcessor.Factory<Face> {
        @Override
        public Tracker<Face> create(Face face) {
            return new GraphicFaceTracker(mGraphicOverlay);
        }
    }

    /**
     * Face tracker for each detected individual. This maintains a face graphic within the app's
     * associated face overlay.
     */
    private class GraphicFaceTracker extends Tracker<Face> {
        private GraphicOverlay mOverlay;
        private FaceGraphic mFaceGraphic;

        GraphicFaceTracker(GraphicOverlay overlay) {
            mOverlay = overlay;
            mFaceGraphic = new FaceGraphic(overlay);
        }

        /**
         * Start tracking the detected face instance within the face overlay.
         */
        @Override
        public void onNewItem(int faceId, Face item) {
            mFaceGraphic.setId(faceId);
        }

        /**
         * Update the position/characteristics of the face within the overlay.
         */
        @Override
        public void onUpdate(FaceDetector.Detections<Face> detectionResults, Face face) {
            mOverlay.add(mFaceGraphic);
            mFaceGraphic.updateFace(face);
            face.getLandmarks().forEach(x -> Log.e("TEST", ""+x.getType()));
            /*if(face.getIsLeftEyeOpenProbability() < 0.5){
                if(!mLeftEyeOpen)
                    startCounter(3, true, mRingProgressBar, mCounterText);
                else{
                    restartCounter(3, true, mRingProgressBar, mCounterText);
                }
                mLeftEyeOpen = true;
            }
            else{
                mLeftEyeOpen = false;
                stopCounter();
            }*/
        }

        /**
         * Hide the graphic when the corresponding face was not detected.  This can happen for
         * intermediate frames temporarily (e.g., if the face was momentarily blocked from
         * view).
         */
        @Override
        public void onMissing(FaceDetector.Detections<Face> detectionResults) {
            mOverlay.remove(mFaceGraphic);
        }

        /**
         * Called when the face is assumed to be gone for good. Remove the graphic annotation from
         * the overlay.
         */
        @Override
        public void onDone() {
            mOverlay.remove(mFaceGraphic);
        }
    }

}
