using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Geta.Bring.Shipping.Model
{
    /// <summary>
    /// Estimate result.
    /// </summary>
    /// <typeparam name="T">Estimate type.</typeparam>
    public class EstimateResult<T> where T : IEstimate
    {
        
        private EstimateResult()
        {
            Estimates = Enumerable.Empty<T>();
            ErrorMessages = Enumerable.Empty<string>();
        }

        /// <summary>
        /// List of estimates.
        /// </summary>
        public IEnumerable<T> Estimates { get; private set; }

        public bool Success { get; private set; }
        public IEnumerable<string> ErrorMessages { get; private set; }

        /// <summary>
        /// Initializes new instance of <see cref="EstimateResult{T}"/> with successful status.
        /// </summary>
        /// <param name="estimates">List of estimates.</param>
        public static EstimateResult<T> CreateSuccess(IEnumerable<T> estimates)
        {
            if (estimates == null) throw new ArgumentNullException(nameof(estimates));
            return new EstimateResult<T>
            {
                Success = true,
                Estimates = estimates
            };
        }

        /// <summary>
        /// Initializes new instance of <see cref="EstimateResult{T}"/> with failure status.
        /// </summary>
        /// <param name="message">Error message.</param>
        public static EstimateResult<T> CreateFailure(string message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            return new EstimateResult<T>
            {
                Success = false,
                ErrorMessages = ParseErrorMessages(message)
            };
        }

        protected static string[] ParseErrorMessages(string message)
        {
            var regex = new Regex("FG_[A-Z0-9_]+");
            var matches = regex.Matches(message);
            var result = new string[matches.Count];

            for (var i = 0; i < matches.Count; i++)
            {
                result[i] = matches[i].Value;
            }

            return result;
        }

        /// <summary>
        /// Initializes new instance of <see cref="EstimateResult{T}"/> with failure status.
        /// </summary>
        /// <param name="messages">Sequence of error messages.</param>
        public static EstimateResult<T> CreateFailure(IEnumerable<string> messages)
        {
            if (messages == null) throw new ArgumentNullException(nameof(messages));

            return new EstimateResult<T>
            {
                Success = false,
                ErrorMessages = messages
            };
        }
    }
}