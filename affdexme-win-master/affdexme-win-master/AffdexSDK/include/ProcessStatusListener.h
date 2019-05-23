#pragma once

#include "AffdexException.h"
namespace affdex
{
    /// <summary>
    /// Interface providing callbacks for the state of video processing.
    /// <para>
    /// This listener is only used by the VideoDetector for video processing.
    /// </para>
    /// </summary>
    class ProcessStatusListener
    {
    public:
        /// <summary>
        /// Indicates that the face detector has failed with an exception.
        /// </summary>
        /// <param name="ex"><see cref="AffdexException" /> encountered during processing.</param>
        virtual void onProcessingException(AffdexException ex) = 0;

        /// <summary>
        /// Indicates that the face detector has processed the video.
        /// <para>
        /// When the face tracker completed processing of the last frame, this method is called.
        /// </para>
        /// </summary>
        virtual void onProcessingFinished() = 0;

        virtual ~ProcessStatusListener() {};
    };
}


