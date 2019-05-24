#pragma once

namespace affdex
{
    /// <summary>
    /// Interface providing callbacks to signal the appearance/disappearance of faces
    /// from a sequence of frames.
    /// </summary>
    class FaceListener
    {
    public:
        /// <summary>
        /// Indicates that the face detector has started tracking a new face.
        /// <para>
        /// When the face tracker detects a face for the first time method is called.
        /// The receiver should expect that tracking continues until detection has stopped.
        /// </para>
        /// </summary>
        /// <param name="timestamp">Frame timestamp when new face was first observed.</param>
        /// <param name="faceId">Face identified.</param>
        virtual void onFaceFound( float timestamp, FaceId faceId ) = 0;

        /// <summary>
        /// Indicates that the face detector has stopped tracking a face.
        /// <para>
        /// When the face tracker no longer finds a face this method is called. The receiver should expect that there is no face tracking until the detector is
        /// started.
        /// </para>
        /// </summary>
        /// <param name="timestamp">Frame timestamp when previously observed face is no longer present.</param>
        /// <param name="faceId">Face identified.</param>
        virtual void onFaceLost(float timestamp, FaceId faceId) = 0;

        virtual ~FaceListener() {};
    };
}

