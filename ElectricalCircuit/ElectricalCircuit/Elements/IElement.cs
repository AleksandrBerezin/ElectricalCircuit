using ElectricalCircuit.Segments;

namespace ElectricalCircuit.Elements
{
    /// <summary>
    /// <see cref="IElement"/> defines fields and methods for working with elements
    /// </summary>
    public interface IElement : ISegment
    {
        /// <summary>
        /// Gets and sets element name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets and sets element value
        /// </summary>
        double Value { get; set; }

        /// <summary>
        /// Returns element type
        /// </summary>
        ElementType Type { get; }
    }
}