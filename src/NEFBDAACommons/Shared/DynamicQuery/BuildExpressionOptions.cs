using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace NEFBDAACommons.Shared.DynamicQuery
{
    /// <summary>
    /// Options to use when building expressions
    /// </summary>
    public class BuildExpressionOptions
    {
        /// <summary>
        /// The <see cref="CultureInfo"/> to use when converting string representations (default InvariantCulture).
        /// </summary>
        public CultureInfo CultureInfo { get; set; } = CultureInfo.InvariantCulture;

        /// <summary>
        /// Whether <see cref="DateTime"/> types should be parsed as UTC.
        /// </summary>
        public bool ParseDatesAsUtc { get; set; }

        /// <summary>
        /// Whether or not to use indexed property
        /// </summary>
        public bool UseIndexedProperty { get; set; }

        /// <summary>
        /// The name of indexable property to use.
        /// </summary>
        public string IndexedPropertyName { get; set; }
    }
}
