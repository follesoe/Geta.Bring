using Geta.Bring.Shipping.Model.Errors;
using System;
using System.Collections.Generic;
using System.Linq;

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
            Errors = Enumerable.Empty<Error>();
        }

        /// <summary>
        /// List of estimates.
        /// </summary>
        public IEnumerable<T> Estimates { get; private set; }

        public bool Success { get; private set; }

        public IEnumerable<Error> Errors { get; private set; }

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

        public static EstimateResult<T> CreateFailure(Error error)
        {
            return CreateFailure(new []{ error });
        }

        public static EstimateResult<T> CreateFailure(IEnumerable<Error> errors)
        {
            if (errors == null) throw new ArgumentNullException(nameof(errors));

            return new EstimateResult<T>
            {
                Success = false,
                Errors = errors
            };
        }
    }
}