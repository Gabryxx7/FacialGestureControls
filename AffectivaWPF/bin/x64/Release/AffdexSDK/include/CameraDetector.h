#pragma once

#pragma warning(push, 0)
#include <memory>
#pragma warning(pop)

#include "FrameDetector.h"

namespace affdex
{
    // Forward Declarations
    class Camera;

    /// <summary>
    /// A detector used to acquire and process frames from a physical camera.
    /// </summary>
    class CameraDetector : public FrameDetector
    {
    public:

        /// <summary>
        /// Creates a CameraDetector.
        /// This class acquires the device camera and will immediately start processing frames from the camera feed.
        /// Processing is asynchronous so some frames may be dropped.
        /// <param name="cameraId">Device id for the camera. </param>
        /// <param name="cameraFPS">Capture framerate from the camera. Must be positive.</param>
        /// <param name="processFPS">Maximum framerate from processing. Must be positive.</param>
        /// <param name="maxNumFaces">The max number of faces to be tracked.</param>
        /// <param name="faceConfig">Maximum processing framerate.</param>
        /// </summary>
        AFFDEXSDK CameraDetector(const int cameraId = 0, const double cameraFPS = 15,
                                 const double processFPS = DEFAULT_PROCESSING_FRAMERATE,
                                 const unsigned int maxNumFaces = DEFAULT_MAX_NUM_FACES,
                                 const FaceDetectorMode faceConfig = affdex::FaceDetectorMode::LARGE_FACES);

        /// <summary>
        /// Finalizes an instance of the <see cref="CameraDetector"/> class.
        /// </summary>
        AFFDEXSDK virtual ~CameraDetector() override;

        /// <summary>
        /// Initializes the CameraDetector and starts producing frames and results immediately.
        /// </summary>
        AFFDEXSDK virtual void start() override;

        /// <summary>
        /// Notifies the CameraDetector to stop processing frames. Immediately stops processing.
        /// </summary>
        AFFDEXSDK virtual void stop() override;

        /// <summary>
        /// Set/reset the camera framerate. Must be positive.
        /// <param name="cameraFPS">Capture framerate from the camera. Must be positive.</param>
        /// <exception cref="AffdexException"> AffdexException on an invalid FPS value </exception>
        /// </summary>
        AFFDEXSDK void setCameraFPS(const double cameraFPS);
        
        /// <summary>
        /// Set/reset the camera id. Must be positive.
        /// <param name="cameraId">Device id for the camera. </param>
        /// <exception cref="AffdexException"> AffdexException on an invalid value </exception>
        /// </summary>
        AFFDEXSDK void setCameraId(const int cameraId);

    private:

        /// Masking the parent FrameDetector's process command.
        using FrameDetector::process;
        void onException(AffdexException);

        std::shared_ptr<Camera> mCam;
    };
}