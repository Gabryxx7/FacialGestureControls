#pragma once

#include "Detector.h"
#include "Frame.h"

namespace affdex
{
    /// <summary>
    /// A Detector for processing still photographs.
    /// <para>
    /// Unlike video detectors  ( VideoDetector, CameraDetector and FrameDetector),
    /// the PhotoDetector does not rely on temporal information and results may differ from those of video detectors.
    /// </para>
    /// </summary>
    class PhotoDetector : public Detector
    {

    public:
#ifdef NO_THREADING
        /// <summary>
        /// Initializes a new instance of the PhotoDetector class that tracks only one face.
        /// <param name="faceConfig">Maximum processing framerate.</param>
        /// </summary>
        AFFDEXSDK PhotoDetector(const FaceDetectorMode faceConfig = FaceDetectorMode::SMALL_FACES);
#else
        /// <summary>
        /// Initializes a new instance of the PhotoDetector class.
        /// <param name="maxNumFaces">The max number of faces to be tracked.</param>
        /// <param name="faceConfig">Maximum processing framerate.</param>
        /// </summary>
        AFFDEXSDK PhotoDetector(const unsigned int maxNumFaces = DEFAULT_MAX_NUM_FACES,
                                const FaceDetectorMode faceConfig = FaceDetectorMode::SMALL_FACES);

#endif
        /// <summary>
        /// Destructor.
        /// </summary>
        AFFDEXSDK virtual ~PhotoDetector() override;

        /// <summary>
        /// Initializes the PhotoDetector in preparation for handling photos subsequently pushed via PhotoDetector::process.
        /// </summary>
        AFFDEXSDK virtual void start() override;


        /// <summary>
        /// Notifies the PhotoDetector that the last photo has been pushed via PhotoDetector::process, allowing it to
        /// deallocate resources.
        /// </summary>
        AFFDEXSDK virtual void stop() override;

        /// <summary>
        /// Processes a photo.
        /// <para>
        /// Results will be processed synchronously and this method will block until the results callback returns.
        /// Subsequent calls to process will be processed independently.
        /// </para>
        /// </summary>
        /// <param name="photo"> Photo to be processed. Timestamp values are ignored and can be used to identify the photo.</param>
        AFFDEXSDK virtual void process(Frame photo);

    };
}
