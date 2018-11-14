using System;

namespace Geta.Bring.Tracking.Model
{
    /// <summary>
    /// Tracking event definitions.
    /// </summary>
    public class TrackingEventDefinition
    {
        public TrackingEventDefinition(string term, string explanation)
        {
            Explanation = explanation ?? throw new ArgumentNullException(nameof(explanation));
            Term = term ?? throw new ArgumentNullException(nameof(term));
        }

        /// <summary>
        /// Tracking event definition term.
        /// </summary>
        public string Term { get; }

        /// <summary>
        /// Tracking event definition explanation.
        /// </summary>
        public string Explanation { get; }
    }
}