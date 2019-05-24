#pragma once

#pragma warning(push, 0) 
#include <memory>
#pragma warning(pop)

#include "FrameDetector.h"
#include "ProcessStatusListener.h"

namespace affdex
{
    // Forward Declarations
    class Video;

    /// <summary>
    /// A Detector used to process a video file.
    /// This class opens a video file and will immediately start decoding and processing frames from the video file.
    /// </summary>
    class VideoDetector : public Detector
    {
    public:

        /// <summary>
        /// Creates a Video Detector.
        /// </summary>
        /// <param name="processFPS">Peak frame-rate at which frames from the video will be processed. Frames occuring in excess of this rate will be dropped.</param>
        /// <param name="maxNumFaces">The max number of faces to be tracked.</param>
        /// <param name="faceConfig">Face detector configuration.</param>
        AFFDEXSDK VideoDetector(const double processFPS = DEFAULT_PROCESSING_FRAMERATE, 
                                const unsigned int maxNumFaces = DEFAULT_MAX_NUM_FACES,
                                const FaceDetectorMode faceConfig = affdex::FaceDetectorMode::SMALL_FACES);

        AFFDEXSDK virtual ~VideoDetector() override;

        /// <summary>
        /// Initializes the VideoDetector and starts producing frames and results immediately.
        /// </summary>
        AFFDEXSDK virtual void start() override;

        /// <summary>
        /// Open a video file
        /// <param name="path">Path to the video file to be processed.</param>
        /// </summary>
        AFFDEXSDK void process(const affdex::path& path);

        /// <summary>
        /// Notifies the VideoDetector to stop processing frames. Immediately stops processing.
        /// </summary>
        AFFDEXSDK virtual void stop() override;

        /// <summary>
        /// Sets the ProcessStatusListener listener.
        /// </summary>
        /// <param name="listener">The listener.</param>
        AFFDEXSDK void setProcessStatusListener(ProcessStatusListener* listener) override;

    private:
        void onComplete();
        void onException(AffdexException);
        
        /// <summary>
        /// Worker callback for processing a frame synchronously.
        /// </summary>
        void processFrame(Frame frame);

        std::shared_ptr<Video> mVideo;
    };
}