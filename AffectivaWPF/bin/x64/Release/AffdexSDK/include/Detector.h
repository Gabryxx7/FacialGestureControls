#pragma once
#pragma warning( disable : 4996 )
#pragma warning(push, 0)
#include <string>
#include <memory>
#pragma warning(pop)

#include "typedefs.h"
#include "ImageListener.h"
#include "FaceListener.h"
#include "ProcessStatusListener.h"

namespace affdex
{

    // Forward References
    class DetectorBase;

    /// <summary>
    /// Base detector class.
    /// </summary>
    class Detector
    {
    public:

        /// <summary>
        /// Constructor.
        /// </summary>
        AFFDEXSDK Detector(){};

        /// <summary>
        /// Virtual destructor.
        /// </summary>
        AFFDEXSDK virtual ~Detector(){};

        /// <summary>
        /// Initialize the detector.
        /// </summary>
        /// <exception cref="AffdexException"> <see cref="AffdexException"/> on failure to initialize.</exception>
        AFFDEXSDK virtual void start();

        /// <summary>
        /// Stop the detector.
        /// </summary>
        AFFDEXSDK virtual void stop();

        /// <summary>
        /// Reset the processing state of the detector.
        /// This method enables an already initialized detector to begin processing a second video/camera feed.
        /// </summary>
        AFFDEXSDK virtual void reset();

        /// <summary>
        /// Returns the state of the detector.
        /// </summary>
        /// <returns> True if the detector is initialized. False otherwise. </returns>
        AFFDEXSDK virtual bool isRunning();
        
        /// <summary>
        /// Gets the face detector configuration in use
        /// </summary>
        AFFDEXSDK virtual FaceDetectorMode getFaceDetectorMode() const;

        /// <summary>
        /// Gets the max number of faces to be tracked.
        /// </summary>
        AFFDEXSDK virtual unsigned int getMaxNumberFaces() const;

        /// <summary>
        /// Sets the path to the license file used to verify the SDK.
        /// </summary>
        /// <param name="licensePath">The license path.</param>
        AFFDEXSDK DEPRECATED virtual void setLicensePath(const affdex::path &licensePath);

        /// <summary>
        /// Sets the license string used to verify the SDK.
        /// </summary>
        /// <param name="licenseString">The license string.</param>
        AFFDEXSDK DEPRECATED virtual void setLicenseString(const char * licenseString);

        /// <summary>
        /// Sets the classifier path.
        /// </summary>
        /// <param name="classifierPath">Path to the directory containing the classifier data files.</param>
        AFFDEXSDK virtual void setClassifierPath(const affdex::path &classifierPath);

        /// <summary>
        /// Sets the <see cref="FaceListener" /> listener.
        /// </summary>
        /// <param name="listener">The listener.</param>
        AFFDEXSDK virtual void setFaceListener(FaceListener* listener);

        /// <summary>
        /// Gets the current <see cref="FaceListener" />.
        /// </summary>
        /// <returns> Pointer to <see cref="FaceListener" />. NULL if not listener was set.</returns>
        AFFDEXSDK FaceListener* getFaceListener();

        /// <summary>
        /// Sets the image listener.
        /// </summary>
        /// <param name="listener">The listener.</param>
        AFFDEXSDK virtual void setImageListener(ImageListener* listener);

        /// <summary>
        /// Gets the current <see cref="ImageListener" />.
        /// </summary>
        /// <returns> Pointer to <see cref="ImageListener" />. NULL if not listener was set.</returns>
        AFFDEXSDK ImageListener* getImageListener();

        /// <summary>
        /// Gets the current <see cref="ProcessStatusListener" />.
        /// </summary>
        /// <returns> Pointer to <see cref="ProcessStatusListener" />. NULL if not listener was set.</returns>
        AFFDEXSDK virtual ProcessStatusListener* getProcessStatusListener();

        /// <summary>
        /// Sets the Processing Status listener.
        /// </summary>
        /// <param name="listener">The listener.</param>
        AFFDEXSDK virtual void setProcessStatusListener(ProcessStatusListener* listener);
        
        /// <summary>
        /// Configure the detection state of all expressions.
        /// </summary>
        /// <param name="detectAllExpressions">True to enable detection of all expressions. False to disable.</param>
        AFFDEXSDK virtual void setDetectAllExpressions(bool detectAllExpressions);

        /// <summary>
        /// Configure the detection state of all expressions.
        /// </summary>
        /// <param name="detectAllEmotions">True to enable detection of all emotions. False to disable.</param>
        AFFDEXSDK virtual void setDetectAllEmotions(bool detectAllEmotions);
        
        /// <summary>
        /// Configure the detection state of all emojis.
        /// </summary>
        /// <param name="detectAllEmojis">True to enable detection of all emojis. False to disable.</param>
        AFFDEXSDK virtual void setDetectAllEmojis(bool detectAllEmojis);
        
        /// <summary>
        /// Configure the detection state of all appearances.
        /// </summary>
        /// <param name="detectAllAppearances">True to enable detection of all appearances. False to disable.</param>
        AFFDEXSDK virtual void setDetectAllAppearances(bool detectAllAppearances);

        /// <summary>
        /// Gets the current state of gender detection.
        /// </summary>
        /// <returns> True if gender detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectGender();

        /// <summary>
        /// Configure the gender detection state.
        /// </summary>
        /// <param name="activate">True to enable gender. False to disable.</param>
        AFFDEXSDK virtual void setDetectGender(bool activate);

        /// <summary>
        /// Gets the current state of glasses detection.
        /// </summary>
        /// <returns> True if glasses detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectGlasses();

        /// <summary>
        /// Configure the glasses detection state.
        /// </summary>
        /// <param name="activate">True to enable glasses. False to disable.</param>
        AFFDEXSDK virtual void setDetectGlasses(bool activate);

        /// <summary>
        /// Gets the current state of age detection.
        /// </summary>
        /// <returns> True if age detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectAge();

        /// <summary>
        /// Configure the age detection state.
        /// </summary>
        /// <param name="activate">True to enable age detection. False to disable.</param>
        AFFDEXSDK virtual void setDetectAge(bool activate);
        
        /// <summary>
        /// Gets the current state of ethnicity detection.
        /// </summary>
        /// <returns> True if age detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectEthnicity();

        /// <summary>
        /// Configure the ethnicity detection state.
        /// </summary>
        /// <param name="activate">True to enable age detection. False to disable.</param>
        AFFDEXSDK virtual void setDetectEthnicity(bool activate);


        /// <summary>
        /// Gets the current state of engagement detection.
        /// </summary>
        /// <returns> True if engagement detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectEngagement();

        /// <summary>
        /// Configure the engagement detection state.
        /// </summary>
        /// <param name="activate">True to enable engagement. False to disable.</param>
        AFFDEXSDK virtual void setDetectEngagement(bool activate);

        /// <summary>
        /// Gets the current state of lip corner depressor detection.
        /// </summary>
        /// <returns> True if lip corner depressor detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectLipCornerDepressor();

        /// <summary>
        /// Configure the lip corner depressor detection state.
        /// </summary>
        /// <param name="activate">True to enable lip corner depressor. False to disable.</param>
        AFFDEXSDK virtual void setDetectLipCornerDepressor(bool activate);

        /// <summary>
        /// Gets the current state of smile detection.
        /// </summary>
        /// <returns> True if smile detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectSmile();

        /// <summary>
        /// Configure the smile detection state.
        /// </summary>
        /// <param name="activate">True to enable smile. False to disable.</param>
        AFFDEXSDK virtual void setDetectSmile(bool activate);

        /// <summary>
        /// Gets the current state of attention detection.
        /// </summary>
        /// <returns> True if attention detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectAttention();

        /// <summary>
        /// Configure the attention detection state.
        /// </summary>
        /// <param name="activate">True to enable attention. False to disable.</param>
        AFFDEXSDK virtual void setDetectAttention(bool activate);

        /// <summary>
        /// Gets the current state of valence detection.
        /// </summary>
        /// <returns> True if valence detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectValence();

        /// <summary>
        /// Gets the current state of joy detection.
        /// </summary>
        /// <returns> True if joy detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectJoy();

        /// <summary>
        /// Configure the joy detection state.
        /// </summary>
        /// <param name="activate">True to enable joy. False to disable.</param>
        AFFDEXSDK virtual void setDetectJoy(bool activate);

        /// <summary>
        /// Gets the current state of fear detection.
        /// </summary>
        /// <returns> True if fear detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectFear();

        /// <summary>
        /// Configure the fear detection state.
        /// </summary>
        /// <param name="activate">True to enable fear. False to disable.</param>
        AFFDEXSDK virtual void setDetectFear(bool activate);

        /// <summary>
        /// Gets the current state of disgust detection.
        /// </summary>
        /// <returns> True if disgust detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectDisgust();

        /// <summary>
        /// Configure the disgust detection state.
        /// </summary>
        /// <param name="activate">True to enable disgust. False to disable.</param>
        AFFDEXSDK virtual void setDetectDisgust(bool activate);

        /// <summary>
        /// Gets the current state of sadness detection.
        /// </summary>
        /// <returns> True if sadness detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectSadness();

        /// <summary>
        /// Configure the sadness detection state.
        /// </summary>
        /// <param name="activate">True to enable sadness. False to disable.</param>
        AFFDEXSDK virtual void setDetectSadness(bool activate);

        /// <summary>
        /// Gets the current state of anger detection.
        /// </summary>
        /// <returns> True if anger detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectAnger();

        /// <summary>
        /// Configure the anger detection state.
        /// </summary>
        /// <param name="activate">True to enable anger. False to disable.</param>
        AFFDEXSDK virtual void setDetectAnger(bool activate);

        /// <summary>
        /// Gets the current state of surprise detection.
        /// </summary>
        /// <returns> True if surprise detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectSurprise();

        /// <summary>
        /// Configure the surprise detection state.
        /// </summary>
        /// <param name="activate">True to enable surprise. False to disable.</param>
        AFFDEXSDK virtual void setDetectSurprise(bool activate);

        /// <summary>
        /// Gets the current state of contempt detection.
        /// </summary>
        /// <returns> True if contempt detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectContempt();

        /// <summary>
        /// Configure the contempt detection state.
        /// </summary>
        /// <param name="activate">True to enable contempt. False to disable.</param>
        AFFDEXSDK virtual void setDetectContempt(bool activate);

        /// <summary>
        /// Configure the valence detection state.
        /// </summary>
        /// <param name="activate">True to enable valence. False to disable.</param>
        AFFDEXSDK virtual void setDetectValence(bool activate);

        /// <summary>
        /// Gets the current state of eyebrow raise detection.
        /// </summary>
        /// <returns> True if eyebrow raise detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectBrowRaise();

        /// <summary>
        /// Configure the inner eyebrow raise detection state.
        /// </summary>
        /// <param name="activate">True to enable inner eyebrow raise. False to disable.</param>
        AFFDEXSDK virtual void setDetectInnerBrowRaise(bool activate);

        /// <summary>
        /// Gets the current state of inner eyebrow raise detection.
        /// </summary>
        /// <returns> True if inner eyebrow raise detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectInnerBrowRaise();

        /// <summary>
        /// Configure the eyebrow raise detection state.
        /// </summary>
        /// <param name="activate">True to enable eyebrow raise. False to disable.</param>
        AFFDEXSDK virtual void setDetectBrowRaise(bool activate);

        /// <summary>
        /// Gets the current state of eyebrow furrow detection.
        /// </summary>
        /// <returns> True if eyebrow furrow detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectBrowFurrow();

        /// <summary>
        /// Configure the eyebrow furrow detection state.
        /// </summary>
        /// <param name="activate">True to enable eyebrow furrow. False to disable.</param>
        AFFDEXSDK virtual void setDetectBrowFurrow(bool activate);

        /// <summary>
        /// Gets the current state of nose wrinkler detection.
        /// </summary>
        /// <returns> True if nose wrinkler detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectNoseWrinkle();

        /// <summary>
        /// Configure the nose wrinkler detection state.
        /// </summary>
        /// <param name="activate">True to enable nose wrinkler. False to disable.</param>
        AFFDEXSDK virtual void setDetectNoseWrinkle(bool activate);

        /// <summary>
        /// Gets the current state of upper lip raiser detection.
        /// </summary>
        /// <returns> True if upper lip raiser detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectUpperLipRaise();

        /// <summary>
        /// Configure the upper lip raiser detection state.
        /// </summary>
        /// <param name="activate">True to enable upper lip raiser. False to disable.</param>
        AFFDEXSDK virtual void setDetectUpperLipRaise(bool activate);

        /// <summary>
        /// Gets the current state of chin raiser detection.
        /// </summary>
        /// <returns> True if chin raiser detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectChinRaise();

        /// <summary>
        /// Configure the chin raiser detection state.
        /// </summary>
        /// <param name="activate">True to enable chin raiser. False to disable.</param>
        AFFDEXSDK virtual void setDetectChinRaise(bool activate);

        /// <summary>
        /// Gets the current state of lip pucker detection.
        /// </summary>
        /// <returns> True if lip pucker detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectLipPucker();

        /// <summary>
        /// Configure the lip pucker detection state.
        /// </summary>
        /// <param name="activate">True to enable lip pucker. False to disable.</param>
        AFFDEXSDK virtual void setDetectLipPucker(bool activate);

        /// <summary>
        /// Gets the current state of lip press detection.
        /// </summary>
        /// <returns> True if lip press detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectLipPress();

        /// <summary>
        /// Configure the lip press detection state.
        /// </summary>
        /// <param name="activate">True to enable lip press. False to disable.</param>
        AFFDEXSDK virtual void setDetectLipPress(bool activate);

        /// <summary>
        /// Gets the current state of mouth open detection.
        /// </summary>
        /// <returns> True if mouth open detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectMouthOpen();

        /// <summary>
        /// Configure the mouth open detection state.
        /// </summary>
        /// <param name="activate">True to enable mouth open. False to disable.</param>
        AFFDEXSDK virtual void setDetectMouthOpen(bool activate);

        /// <summary>
        /// Gets the current state of lip suck detection.
        /// </summary>
        /// <returns> True if lip suck detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectLipSuck();

        /// <summary>
        /// Configure the lip suck detection state.
        /// </summary>
        /// <param name="activate">True to enable lip suck. False to disable.</param>
        AFFDEXSDK virtual void setDetectLipSuck(bool activate);

        /// <summary>
        /// Gets the current state of smirk detection.
        /// </summary>
        /// <returns> True if smirk detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectSmirk();

        /// <summary>
        /// Configure the smirk detection state.
        /// </summary>
        /// <param name="activate">True to enable smirk. False to disable.</param>
        AFFDEXSDK virtual void setDetectSmirk(bool activate);

        /// <summary>
        /// Gets the current state of eye closure detection.
        /// </summary>
        /// <returns> True if eye closure detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectEyeClosure();

        /// <summary>
        /// Configure the eye closure detection state.
        /// </summary>
        /// <param name="activate">True to enable eye closure. False to disable.</param>
        AFFDEXSDK virtual void setDetectEyeClosure(bool activate);

        /// <summary>
        /// Gets the current state of eye widen detection.
        /// </summary>
        /// <returns> True if eye widen detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectEyeWiden();

        /// <summary>
        /// Configure the eye widen detection state.
        /// </summary>
        /// <param name="activate">True to enable eye widen. False to disable.</param>
        AFFDEXSDK virtual void setDetectEyeWiden(bool activate);

        /// <summary>
        /// Gets the current state of cheek raise detection.
        /// </summary>
        /// <returns> True if cheek raise detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectCheekRaise();

        /// <summary>
        /// Configure the cheek raise detection state.
        /// </summary>
        /// <param name="activate">True to enable cheek raise. False to disable.</param>
        AFFDEXSDK virtual void setDetectCheekRaise(bool activate);

        /// <summary>
        /// Gets the current state of lid tighten detection.
        /// </summary>
        /// <returns> True if lid tighten detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectLidTighten();

        /// <summary>
        /// Configure the lid tighten detection state.
        /// </summary>
        /// <param name="activate">True to enable lid tighten. False to disable.</param>
        AFFDEXSDK virtual void setDetectLidTighten(bool activate);

        /// <summary>
        /// Gets the current state of dimpler detection.
        /// </summary>
        /// <returns> True if dimpler detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectDimpler();

        /// <summary>
        /// Configure the dimpler detection state.
        /// </summary>
        /// <param name="activate">True to enable dimpler. False to disable.</param>
        AFFDEXSDK virtual void setDetectDimpler(bool activate);

        /// <summary>
        /// Gets the current state of lip stretch detection.
        /// </summary>
        /// <returns> True if lip stretch detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectLipStretch();

        /// <summary>
        /// Configure the lip stretch detection state.
        /// </summary>
        /// <param name="activate">True to enable lip stretch. False to disable.</param>
        AFFDEXSDK virtual void setDetectLipStretch(bool activate);

        /// <summary>
        /// Gets the current state of jaw drop detection.
        /// </summary>
        /// <returns> True if jaw drop stretch detection is currently enabled.</returns>
        AFFDEXSDK virtual bool getDetectJawDrop();

        /// <summary>
        /// Configure the jaw drop detection state.
        /// </summary>
        /// <param name="activate">True to enable jaw drop. False to disable.</param>
        AFFDEXSDK virtual void setDetectJawDrop(bool activate);

        /// <summary>
        /// Enable Analytics.
        /// </summary>
        AFFDEXSDK virtual void enableAnalytics();

        /// <summary>
        /// Disable Analytics.
        /// </summary>
        AFFDEXSDK virtual void disableAnalytics();

    protected:

        /// <summary>
        /// Initializes a new instance of the <see cref="Detector" /> class.
        /// </summary>
        /// <param name="processFrameRate">Maximum processing framerate.</param>
        Detector(const float processFrameRate);

        /// <summary>
        /// Initializes a new instance of the <see cref="Detector" /> class.
        /// </summary>
        /// <param name="useStaticClassifiers">The use static classifiers.</param>
        /// <param name="processFrameRate">Maximum processing framerate.</param>
        /// <param name="faceConfig">Face detector configuration.</param>
        Detector(const bool useStaticClassifiers, const float processFrameRate,
                 const FaceDetectorMode faceConfig);

    protected:

        /// <summary>
        /// Smart pointer to underlying DetectorBase
        /// </summary>
        std::shared_ptr<affdex::DetectorBase> mDetectorBase;
    };
}
