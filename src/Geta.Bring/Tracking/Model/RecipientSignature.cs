using System;

namespace Geta.Bring.Tracking.Model
{
    /// <summary>
    /// Recipient signature.
    /// </summary>
    public class RecipientSignature
    {
        public RecipientSignature(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        /// <summary>
        /// Signature name.
        /// </summary>
        public string Name { get; }
    }
}