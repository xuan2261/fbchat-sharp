﻿using Newtonsoft.Json.Linq;
using System;

namespace fbchat_sharp.API
{
    /// <summary>
    /// Custom exception thrown by fbchat-sharp. All exceptions in the fbchat-sharp module inherits this
    /// </summary>
    public class FBchatException : Exception
    {
        /// <summary>
        /// Custom exception thrown by fbchat-sharp. All exceptions in the fbchat-sharp module inherits this
        /// </summary>
        /// <param name="message"></param>
        public FBchatException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Raised when we fail parsing a response from Facebook.
    /// This may contain sensitive data, so should not be logged to file.
    /// </summary>
    public class FBchatParseError : FBchatException
    {
        /// <summary>
        /// Raised when we fail parsing a response from Facebook.
        /// This may contain sensitive data, so should not be logged to file.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public FBchatParseError(string message, JToken data = null) : base(message)
        {
            this.Data.Add("data", data);
        }

        /// <summary>
        /// Returns a string representation of the current exception.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var msg = "{0}. Please report this, along with the data below!\n{1}";
            return string.Format(msg, this.Message, this.Data["data"]?.ToString());
        }
    }

    /// <summary>
    /// Thrown by fbchat-sharp when Facebook returns an error
    /// </summary>
    public class FBchatFacebookError : FBchatException
    {
        /// The error code that Facebook returned
        public long fb_error_code { get; set; }
        /// The error message that Facebook returned (In the user's own language)
        public string fb_error_message { get; set; }
        /// The status code that was sent in the http response (eg. 404) (Usually only set if not successful, aka. not 200)
        public int request_status_code { get; set; }

        /// <summary>
        /// Thrown by fbchat-sharp when Facebook returns an error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="fb_error_code"></param>
        /// <param name="fb_error_message"></param>
        /// <param name="request_status_code"></param>
        public FBchatFacebookError(string message, long fb_error_code = 0, string fb_error_message = null, int request_status_code = 0) 
            : base(message)
        {
            this.fb_error_code = fb_error_code;
            this.fb_error_message = fb_error_message;
            this.request_status_code = request_status_code;
        }
    }

    /// <summary>
    /// Raised by Facebook if:
    /// - Some function supplied invalid parameters.
    /// - Some content is not found.
    /// - Some content is no longer available.
    /// </summary>
    public class FBchatInvalidParameters : FBchatFacebookError
    {
        /// <summary>
        /// Raised by Facebook if:
        /// - Some function supplied invalid parameters.
        /// - Some content is not found.
        /// - Some content is no longer available.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="fb_error_code"></param>
        /// <param name="fb_error_message"></param>
        /// <param name="request_status_code"></param>
        public FBchatInvalidParameters(string message, long fb_error_code = 0, string fb_error_message = null, int request_status_code = 0) 
            : base(message, fb_error_code, fb_error_message, request_status_code)
        {
        }
    }

    /// <summary>
    /// Raised by Facebook if the client has been logged out.
    /// </summary>
    public class FBchatNotLoggedIn : FBchatFacebookError
    {
        /// <summary>
        /// Raised by Facebook if the client has been logged out.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="fb_error_code"></param>
        /// <param name="fb_error_message"></param>
        /// <param name="request_status_code"></param>
        public FBchatNotLoggedIn(string message, long fb_error_code = 1357001, string fb_error_message = null, int request_status_code = 0)
            : base(message, fb_error_code, fb_error_message)
        {
        }
    }

    /// <summary>
    /// Raised by Facebook if the client has been inactive for too long.
    /// This error usually happens after 1-2 days of inactivity.
    /// </summary>
    public class FBchatPleaseRefresh : FBchatFacebookError
    {
        /// <summary>
        /// Raised by Facebook if the client has been inactive for too long.
        /// This error usually happens after 1-2 days of inactivity.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="fb_error_code"></param>
        /// <param name="fb_error_message"></param>
        /// <param name="request_status_code"></param>
        public FBchatPleaseRefresh(string message, long fb_error_code = 1357004, string fb_error_message = null, int request_status_code = 0)
            : base(message, fb_error_code, fb_error_message)
        {
        }
    }

    /// <summary>
    /// Thrown by fbchat-sharp when wrong values are entered
    /// </summary>
    public class FBchatUserError : FBchatException
    {
        /// <summary>
        /// Thrown by fbchat-sharp when wrong values are entered
        /// </summary>
        /// <param name="message"></param>
        public FBchatUserError(string message) : base(message)
        {
        }
    }
}
