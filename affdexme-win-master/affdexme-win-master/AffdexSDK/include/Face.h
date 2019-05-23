#pragma once
#pragma warning( disable : 4505 )

#pragma warning(push, 0) 
#include <vector>
#include <map>
#include <unordered_map>
#include <cmath>
#pragma warning(pop)

#include "typedefs.h"

namespace affdex
{    

    /// <summary>
    /// Structure containing the facial expressions scores
    /// </summary>
    struct Expressions
    {
        /// <summary>
        /// Smile score
        /// range of the value is [0, 100]
        /// </summary>
        float smile;
        /// <summary>
        /// Inner brow raise score
        /// range of the value is [0, 100]
        /// </summary>
        float innerBrowRaise;
        /// <summary>
        /// Brow raise score
        /// range of the value is [0, 100]
        /// </summary>
        float browRaise;
        /// <summary>
        /// Brow furrow score
        /// range of the value is [0, 100]
        /// </summary>
        float browFurrow;
        /// <summary>
        /// Nose wrinkler score
        /// range of the value is [0, 100]
        /// </summary>
        float noseWrinkle;
        /// <summary>
        /// Upper lip raiser score
        /// range of the value is [0, 100]
        /// </summary>
        float upperLipRaise;
        /// <summary>
        /// Lip corner depressor score
        /// range of the value is [0, 100]
        /// </summary>
        float lipCornerDepressor;
        /// <summary>
        /// Chin raiser score
        /// range of the value is [0, 100]
        /// </summary>
        float chinRaise;
        /// <summary>
        /// Lip pucker score
        /// range of the value is [0, 100]
        /// </summary>
        float lipPucker;
        /// <summary>
        /// Lip press score
        /// range of the value is [0, 100]
        /// </summary>
        float lipPress;
        /// <summary>
        /// Lip suck score
        /// range of the value is [0, 100]
        /// </summary>
        float lipSuck;
        /// <summary>
        /// Mouth open score
        /// range of the value is [0, 100]
        /// </summary>
        float mouthOpen;
        /// <summary>
        /// Smirk score
        /// range of the value is [0, 100]
        /// </summary>
        float smirk;
        /// <summary>
        /// Eye closure score
        /// range of the value is [0, 100]
        /// </summary>
        float eyeClosure;
        /// <summary>
        /// Attention score
        /// range of the value is [0, 100]
        /// </summary>
        float attention;
        /// <summary>
        /// Eye Widen score
        /// range of the value is [0, 100]
        /// </summary>
        float eyeWiden;
        /// <summary>
        /// Cheek Raise score
        /// range of the value is [0, 100]
        /// </summary>
        float cheekRaise;
        /// <summary>
        /// Lid Tighten score
        /// range of the value is [0, 100]
        /// </summary>
        float lidTighten;
        /// <summary>
        /// Dimpler score
        /// range of the value is [0, 100]
        /// </summary>
        float dimpler;
        /// <summary>
        /// Lip Stretch score
        /// range of the value is [0, 100]
        /// </summary>
        float lipStretch;
        /// <summary>
        /// Jaw Drop score
        /// range of the value is [0, 100]
        /// </summary>
        float jawDrop;

        AFFDEXSDK Expressions();

        /// <summary>
        /// copy constructor
        /// </summary>
        AFFDEXSDK Expressions(const Expressions &);
    };

    /// <summary>
    /// Structure containing emotions scores
    /// </summary>
    struct Emotions
    {
        /// <summary>
        /// Joy score
        /// range of the value is [0, 100]
        /// </summary>
        float joy;
        /// <summary>
        /// Fear score
        /// range of the value is [0, 100]
        /// </summary>
        float fear;
        /// <summary>
        /// Disgust score
        /// range of the value is [0, 100]
        /// </summary>
        float disgust;
        /// <summary>
        /// Sadness score
        /// range of the value is [0, 100]
        /// </summary>
        float sadness;
        /// <summary>
        /// Anger score
        /// range of the value is [0, 100]
        /// </summary>
        float anger;
        /// <summary>
        /// Suprise score
        /// range of the value is [0, 100]
        /// </summary>
        float surprise;
        /// <summary>
        /// Contempt score
        /// range of the value is [0, 100]
        /// </summary>
        float contempt;
        /// <summary>
        /// Valence score
        /// range of the value is [-100, 100]
        /// </summary>
        float valence;
        /// <summary>
        /// Engagment score
        /// range of the value is [0, 100]
        /// </summary>
        float engagement;

        AFFDEXSDK Emotions();

        /// <summary>
        /// copy constructor
        /// </summary>
        AFFDEXSDK Emotions(const Emotions &);
    };

    /// <summary>
    /// Enumeration for the gender values
    /// </summary>
    enum class Gender {
        Unknown,
        Male,
        Female,

    };

    /// <summary>
    ///  Enumeration for the glasses values
    /// </summary>
    enum class Glasses {
        No,
        Yes
    };

    /// <summary>
    ///  Enumeration for the age values
    /// </summary>
    enum Age
    {
        AGE_UNKNOWN = 0,
        AGE_UNDER_18 = 1,
        AGE_18_24 = 2,
        AGE_25_34 = 3,
        AGE_35_44 = 4,
        AGE_45_54 = 5,
        AGE_55_64 = 6,
        AGE_65_PLUS = 7
    };

    /// <summary>
    ///  Enumeration for the ethnicity values
    /// </summary>
    enum Ethnicity
    {
        UNKNOWN = 0,
        CAUCASIAN = 1,
        BLACK_AFRICAN = 2,
        SOUTH_ASIAN = 3,
        EAST_ASIAN = 4,
        HISPANIC = 5
    };

    /// <summary>
    /// Structure containing appearance information for a face
    /// </summary>
    struct Appearance
    {
        /// <summary>
        /// Gender
        /// Values {Unknown, Male, Female}
        /// </summary>
        Gender gender;

        /// <summary>
        /// Glasses
        /// Values {No, Yes}
        /// </summary>
        Glasses glasses;

        /// <summary>
        /// Age
        /// Values {AGE_UNKNOWN, AGE_UNDER_18, AGE_18_24,
        /// AGE_25_34, AGE_35_44, AGE_45_54, AGE_55_64, AGE_65_PLUS}
        /// </summary>
        Age age;

        /// <summary>
        /// Ethnicity
        /// Values {UNKNOWN, CAUCASIAN, BLACK_AFRICAN, SOUTH_ASIAN, EAST_ASIAN, HISPANIC}
        /// </summary>
        Ethnicity ethnicity;

        AFFDEXSDK Appearance();

        /// <summary>
        /// copy constructor
        /// </summary>
        AFFDEXSDK Appearance(const Appearance &);
    };

    /// <summary>
    /// Structure containing the angles to represent the head orientation in degrees.
    /// </summary>
    struct Orientation
    {
        /// <summary>
        /// Pitch Angle
        /// </summary>
        float pitch;
        /// <summary>
        /// Yaw Angle
        /// </summary>
        float yaw;
        /// <summary>
        /// Roll Angle
        /// </summary>
        float roll;
        /// <summary>
        /// Constructor.
        /// </summary>
        AFFDEXSDK Orientation(){};
        /// <summary>
        /// constructs a new Orientation from a set of angles
        /// </summary>
        AFFDEXSDK Orientation(float pitch, float yaw, float roll)
            :
            pitch(pitch), yaw(yaw), roll(roll)
        {};

        /// <summary>
        /// Copy constructor.
        /// </summary>
        AFFDEXSDK Orientation(const Orientation & orientation)
            :
            pitch(orientation.pitch), yaw(orientation.yaw), roll(orientation.roll)
        {};

        /// <summary>
        /// Overloading the equals operator
        /// </summary>
        AFFDEXSDK bool operator==(const Orientation& lhs) const
        {
            double epsilon = 1.0e-2;
            return       (std::fabs(std::fabs(lhs.pitch) - std::fabs(this->pitch)) < epsilon)
                    && (std::fabs(std::fabs(lhs.yaw) - std::fabs(this->yaw)) < epsilon)
                    && (std::fabs(std::fabs(lhs.roll) - std::fabs(this->roll)) < epsilon);
        };
    };

    /// <summary>
    /// Structure containing the measurements
    /// </summary>
    struct Measurements
    {
        /// <summary>
        /// orientation of face
        /// </summary>
        Orientation orientation;

        /// <summary>
        /// distance between the two outer eye corner landmarks, in pixels
        /// </summary>
        float interocularDistance;

        AFFDEXSDK Measurements();

        /// <summary>
        /// copy constructor
        /// </summary>
        AFFDEXSDK Measurements(const Measurements &);
    };

    /// <summary>
    /// Enumeration for the emoji names used by dominantEmoji
    /// </summary>
    enum class Emoji {
        Relaxed = 9786,
        Smiley = 128515,
        Laughing = 128518,
        Kissing = 128535,
        Disappointed = 128542,
        Rage = 128545,
        Smirk = 128527,
        Wink = 128521,
        StuckOutTongueWinkingEye = 128540,
        StuckOutTongue = 128539,
        Flushed = 128563,
        Scream = 128561,
        Unknown = 128528
    };

    /// <summary>
    /// Converts Emoji enum value into string;
    /// <returns> A representation of the  <see cref="Emoji" />.</returns>
    /// </summary>
    static std::string EmojiToString(Emoji emoji) {
        std::string ret = "";
        switch (emoji)
        {
            case Emoji::Relaxed:
                ret = "Relaxed";
                break;
            case Emoji::Smiley:
                ret = "Smiley";
                break;
            case Emoji::Laughing:
                ret = "Laughing";
                break;
            case Emoji::Kissing:
                ret = "Kissing";
                break;
            case Emoji::Disappointed:
                ret = "Disappointed";
                break;
            case Emoji::Rage:
                ret = "Rage";
                break;
            case Emoji::Smirk:
                ret = "Smirk";
                break;
            case Emoji::Wink:
                ret = "Wink";
                break;
            case Emoji::StuckOutTongueWinkingEye:
                ret = "StuckOutTongueWinkingEye";
                break;
            case Emoji::StuckOutTongue:
                ret = "StuckOutTongue";
                break;
            case Emoji::Flushed:
                ret = "Flushed";
                break;
            case Emoji::Scream:
                ret = "Scream";
                break;
            case Emoji::Unknown:
            default:
                ret = "Unknown";
                break;
        };
        return ret;
    }
    
    /// <summary>
    /// Structure containing emoji(s) scores and the value of the most dominant emoji.
    /// </summary>
    struct Emojis
    {
        /// <summary>
        /// relaxed score
        /// range of the value is [0, 100]
        /// </summary>
        float relaxed;

        /// <summary>
        /// smiley score
        /// range of the value is [0, 100]
        /// </summary>
        float smiley;

        /// <summary>
        /// laughing score
        /// range of the value is [0, 100]
        /// </summary>
        float laughing;

        /// <summary>
        /// kiss score
        /// range of the value is [0, 100]
        /// </summary>
        float kissing;
        
        /// <summary>
        /// disappointment score
        /// range of the value is [0, 100]
        /// </summary>
        float disappointed;

        /// <summary>
        /// rage score
        /// range of the value is [0, 100]
        /// </summary>
        float rage;

        /// <summary>
        /// smirk score
        /// range of the value is [0, 100]
        /// </summary>
        float smirk;

        /// <summary>
        /// wink score
        /// range of the value is [0, 100]
        /// </summary>
        float wink;
        
        /// <summary>
        /// tongue out and wink score
        /// range of the value is [0, 100]
        /// </summary>
        float stuckOutTongueWinkingEye;
        
        /// <summary>
        /// tongue out score
        /// range of the value is [0, 100]
        /// </summary>
        float stuckOutTongue;
        
        /// <summary>
        /// flushed score
        /// range of the value is [0, 100]
        /// </summary>
        float flushed;   

        /// <summary>
        /// scream score
        /// range of the value is [0, 100]
        /// </summary>
        float scream;

        /// <summary>
        /// dominant emoji value is the most likely emoji present in the Frame.
        /// </summary>
        Emoji dominantEmoji;

        AFFDEXSDK Emojis();

        /// <summary>
        /// copy constructor
        /// </summary>
        AFFDEXSDK Emojis(const Emojis &);          
    };

    /// <summary>
    /// Structure containing basic feature point coordinates.
    /// Coordinate system is an x,y system with the top-left pixel center at ( x=0, y=0 )
    /// and the bottom right pixel at ( x=width-1, y=height-1 )
    /// </summary>
    struct FeaturePoint
    {
        /// <summary>
        /// Point identifier.
        /// </summary>
        int id;

        /// <summary>
        /// X-coordinate of point
        /// </summary>
        float x;

        /// <summary>
        /// Y-coordinate of point
        /// </summary>
        float y;

        /// <summary>
        /// Constructor.
        /// </summary>
        FeaturePoint(int id, float x, float y)
            :
            id(id), x(x), y(y)
        {};

        /// <summary>
        /// Copy constructor.
        /// </summary>
        FeaturePoint(const FeaturePoint & point)
            :
            id(point.id), x(point.x), y(point.y)
        {};
    };

    /// <summary>
    /// Structure containing face quality values
    /// </summary>
    struct FaceQuality
    {
        /// <summary>
        /// Indicates how well the face is lit for purposes of analysis. Value range is [0, 100].
        /// There are no hard boundaries, but consider the following ranges as general guidelines:
        /// <para/>0-10: too dark
        /// <para/>10-25: suboptimal
        /// <para/>25-75: well lit
        /// <para/>75-90: suboptimal
        /// <para/>90-100: too bright 
        /// </summary>
        float brightness;
    };

    /// <summary>
    /// face points
    /// </summary>
    typedef std::vector<FeaturePoint> VecFeaturePoint;

    /// <summary>
    /// Represents a face found within a processed Frame.
    /// </summary>
    class Face
    {
        friend class DetectorBase;
        friend class SingleFaceDetectorBase;
        friend class MultiFaceDetectorBase;

    public:

        /// <summary>
        /// Initializes a new instance of the <see cref="Face"/> class.
        /// </summary>
        AFFDEXSDK Face();

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="face">Face object.</param>
        AFFDEXSDK Face(const Face& face);

        /// <summary>
        /// The face id
        /// </summary>
        FaceId id;

        /// <summary>
        /// The Emotions
        /// </summary>
        Emotions emotions;

        /// <summary>
        /// The Facial Expressions
        /// </summary>
        Expressions expressions;

        /// <summary>
        /// The Face and Head Measurements
        /// </summary>
        Measurements measurements;

        /// <summary>
        /// Facial Appearance
        /// </summary>
        Appearance appearance;

        /// <summary>
        /// Emojis
        /// </summary>
        Emojis emojis;

        /// <summary>
        /// The face points
        /// </summary>
        VecFeaturePoint featurePoints;

        /// <summary>
        /// The quality of the image patch that contains the face.
        /// </summary>
        FaceQuality faceQuality;

    protected:
        /// <summary>
        /// construct new Face from MetricsMap
        /// </summary>
        Face(MetricsMap metrics);
    };
}
