#pragma once

// Include C++ headers
#include "typedefs.h"

#pragma warning(push, 0) 
#include <string>
#include <stdexcept>
#include <memory>
#pragma warning(pop)

namespace cv { class Mat; }  // Forward declaration

namespace affdex
{
    /// <summary>
    /// A wrapper class for images and their associated timestamps.
    /// </summary>
    class Frame
    {

    public:

        /// <summary>
        /// Enum specifying the pixel ordering.
        /// </summary>
        enum class COLOR_FORMAT
        {
            /// <summary>
            /// 24-bit pixels with Red, Green, Blue pixel ordering
            /// </summary>
            RGB,
            /// <summary>
            /// 24-bit pixels with Blue, Green, Red pixel ordering
            /// </summary>
            BGR,
            /// <summary>
            /// 32-bit pixels with Red, Green, Blue, Alpha  pixel ordering
            /// </summary>
            RGBA,
            /// <summary>
            /// 24-bit pixels with Blue, Green, Red, Alpha pixel ordering
            /// </summary>
            BGRA,
            /// <summary>
            /// 12-bit pixels with YUV information (NV21 encoding)
            /// </summary>
            YUV_NV21,
            /// <summary>
            /// 12-bit pixels with YUV information (I420 encoding)
            /// </summary>
            YUV_I420,
            /// <summary>
            /// 16-bit pixels with YUV information (YUY2 encoding)
            /// </summary>
            YUV_YUY2
        };

        /// <summary>
        /// the rotation of a frame
        /// </summary>
        enum ROTATION
        {
            /// <summary>
            /// frame is upright
            /// </summary>
            UPRIGHT = 1,
            /// <summary>
            /// frame is rotated 90 degrees clockwise
            /// </summary>
            CW_90 = 8,
            /// <summary>
            /// frame is rotated 180 degrees clockwise
            /// </summary>
            CW_180 = 3,
            /// <summary>
            /// frame is rotated 270 degrees clockwise
            /// </summary>
            CW_270 = 6
        };

        /// <summary>
        /// default constructor.
        /// </summary>
        AFFDEXSDK Frame() {};

        /// <summary>
        /// Constructs a new instance of the Frame class
        /// </summary>
        /// <param name="frameWidth">Width of the frame. Value has to be greater than zero</param>
        /// <param name="frameHeight">Height of the frame. Value has to be greater than zero</param>
        /// <param name="pixels">Pointer to an array of pixels</param>
        /// <param name="frameColorFormat">The frame color format.</param>
        /// <param name="timestamp">The timestamp of the frame (in seconds). Can be used as an identifier of the frame</param>
        AFFDEXSDK Frame(int frameWidth, int frameHeight, void * pixels, COLOR_FORMAT frameColorFormat, float timestamp = 0.0f);

        /// <summary>
        /// Constructs a new instance of the Frame class
        /// </summary>
        /// <param name="frameWidth">Width of the frame. Value has to be greater than zero</param>
        /// <param name="frameHeight">Height of the frame. Value has to be greater than zero</param>
        /// <param name="pixels">Pointer to an array of pixels</param>
        /// <param name="frameColorFormat">The frame color format.</param>
        /// <param name="rotation">The rotation of the frame</param>
        /// <param name="timestamp">The timestamp of the frame (in seconds). Can be used as an identifier of the frame</param>
        AFFDEXSDK Frame(int frameWidth, int frameHeight, void * pixels, COLOR_FORMAT frameColorFormat, ROTATION rotation, float timestamp = 0.0f);

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="frame">The frame.</param>
        AFFDEXSDK Frame(const Frame & frame);

        /// <summary>
        /// Initializes the Frame class
        /// </summary>
        /// <param name="frameWidth">Width of the frame. Value has to be greater than zero</param>
        /// <param name="frameHeight">Height of the frame. Value has to be greater than zero</param>
        /// <param name="pixels">Pointer to an array of pixels</param>
        /// <param name="frameColorFormat">The frame color format.</param>
        /// <param name="rotation">The rotation of the frame</param>
        /// <param name="timestamp">The timestamp of the frame (in seconds). Can be used as an identifier of the frame</param>
        AFFDEXSDK void init(int frameWidth, int frameHeight, void * pixels, COLOR_FORMAT frameColorFormat, ROTATION rotation, float timestamp);

        /// <summary>
        /// Get Frame's color format.
        /// </summary>
        /// <returns> color format used to create the Frame.</returns>
        /// <seealso cref="COLOR_FORMAT" />
        AFFDEXSDK COLOR_FORMAT getColorFormat() const;

        /// <summary>
        /// Get underlying byte array of pixels.
        /// </summary>
        /// <returns> Frame's byte array of pixels </returns>
        AFFDEXSDK std::shared_ptr<byte> getBGRByteArray();

        /// <summary>
        /// Get length of the byte array of pixels.
        /// </summary>
        /// <returns>Length of byte array.</returns>
        AFFDEXSDK int getBGRByteArrayLength() const;

        /// <summary>
        /// Gets the width.
        /// </summary>
        AFFDEXSDK int getWidth() const;

        /// <summary>
        /// Gets the height.
        /// </summary>
        AFFDEXSDK int getHeight() const;

        /// <summary>
        /// Gets the timestamp (value in seconds).
        /// </summary>
        AFFDEXSDK float getTimestamp() const;

        /// <summary>
        /// Set the timestamp (value in seconds).
        /// </summary>
        AFFDEXSDK void setTimestamp(float value);

        /// <summary>
        /// Get a CV MAT pointer to the image
        /// </summary>
        cv::Mat *getImage();

    private:

        /// <summary>
        /// Internal image structure
        /// </summary>
        std::shared_ptr<cv::Mat> mImage;

        /// <summary>
        /// The m no of channels
        /// </summary>
        int mNoOfChannels;

        /// <summary>
        /// Timestamp in seconds.
        /// </summary>
        float mTimestamp;

        /// <summary>
        /// The m color format
        /// </summary>
        COLOR_FORMAT mColorFormat;

    };
}
