#pragma once

#pragma warning(push, 0)
#include <string>
#include <stdexcept>
#pragma warning(pop)

#include "typedefs.h"

namespace affdex
{
    /*
        * Copyright (c) 2015-2016 Affectiva, Inc. All rights reserved.
    */

    /// <summary>
    /// AffdexException.
    /// Base exception type for exceptions thrown by the Affectiva SDK.
    /// </summary>
    class AffdexException : public std::runtime_error
    {
    public:

        /// <summary>
        /// Retrieve the exception description.
        /// </summary>
        AFFDEXSDK std::string getExceptionMessage() const;

        /// <summary>
        /// Initializes a new instance of the <see cref="AffdexException"/> class.
        /// </summary>
        /// <param name="msg">The exception description.</param>
        AFFDEXSDK AffdexException(const std::string &msg);

    };
}
