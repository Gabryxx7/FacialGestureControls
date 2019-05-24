#pragma once

#pragma warning(push, 0) 
#include <memory>
#pragma warning(pop)

#include "Detector.h"
#include "Frame.h"

namespace affdex
{
    // Forward Declarations
    class AsyncProcessor;

    /// <summary>
    /// A <see cref="Detector" /> used to process a sequence of pushed frames.
    /// </summary>
    class FrameDetector : public Detector
    {

    public:

      /// <summary>
      /// Creates a FrameDetector.
      /// </summary>
      /// <param name="bufferSize">Size of buffer to use for processing </param>
      /// <param name="processFrameRate">Maximum processing framerate.</param>
      /// <param name="maxNumFaces">The max number of faces to be tracked.</param>
      /// <param name="faceConfig">Maximum processing framerate.</param>
      AFFDEXSDK FrameDetector(const int bufferSize,
                              const float processFrameRate = DEFAULT_PROCESSING_FRAMERATE,
                              const unsigned int maxNumFaces = DEFAULT_MAX_NUM_FACES,
                              const FaceDetectorMode faceConfig = affdex::FaceDetectorMode::LARGE_FACES);

        /// <summary>
        /// Finalizes an instance of the <see cref="FrameDetector"/> class.
        /// </summary>
        AFFDEXSDK virtual ~FrameDetector() override;

        /// <summary>
        /// Initializes the FrameDetector in preparation for handling frames subsequently pushed via <see cref="process" />
        /// </summary>
        AFFDEXSDK virtual void start() override;

        /// <summary>
        /// Re-initializes the FrameDetector in preparation for handling frames subsequently pushed via <see cref="process" />
        /// This can be called between camera sessions / videos without having to re-initialize the detector (stop/start).
        /// After this call, Frame timestamps can begin at 0 again.
        /// </summary>
        AFFDEXSDK virtual void reset() override;

        /// <summary>
        /// Immediately signal the frameProcessing to stop, deallocating resources.
        /// </summary>
        AFFDEXSDK virtual void stop() override;

        /// <summary>
        /// Provide a frame for the detector to process.
        /// Callers may pass frames to this method at any rate that is suitable for them. The
        /// detector expects subsequent frames to be related to previous frames (e.g. a video stream).
        /// </summary>
        /// <param name="frame">Video frame to be added to the processing buffer.</param>
        AFFDEXSDK virtual void process(Frame frame);

    private:

		/// <summary>
		/// The asynchronous procesing engine.
		/// </summary>
		std::shared_ptr<AsyncProcessor> mAsyncProcessor;

    };
}
