#pragma once

#pragma warning(push, 0) 
#include <map>
#include <memory>
#pragma warning(pop)

#include "Face.h"
#include "Frame.h"

namespace affdex
{
    /// <summary>
    /// Interface providing callbacks for capture and processsing results of individual frames.
    /// </summary>
    class ImageListener
    {
    public:

        /// <summary>
        /// Callback providing results for a processed frame.
        /// <para>
        /// The current interface allows for multiple faces to be processed.
        /// </para>
        /// </summary>
        /// <param name="faces">A dictionary of  <see cref="Face" /> objects containing metrics about each face found in the image.</param>
        /// <param name="image"> The <see cref="Frame" /> that was processed. </param>
        virtual void onImageResults(std::map<FaceId, Face> faces, Frame image) = 0;

        /// <summary>
        /// Callback for every input <see cref="Frame" />.
        /// <para>
        /// In cases where the processing framerate is lower than the input framerate, this method will
        /// all captured frames, including those that aren't processed. This can be useful for the CameraDetector
        /// or VideoDetector when used with a low processing framerate.
        /// </para>
        /// </summary>
        /// <param name="image">The <see cref="Frame" /> captured.</param>
        virtual void onImageCapture(Frame image) = 0;

        virtual ~ImageListener(){};
    };
}

