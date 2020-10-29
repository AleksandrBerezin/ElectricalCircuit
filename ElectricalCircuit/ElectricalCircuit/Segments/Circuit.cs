using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using ElectricalCircuit.Segments;

namespace ElectricalCircuit
{
    /// <summary>
    /// <see cref="Circuit"/> stores list of the circuit segments
    /// </summary>
    public class Circuit : SegmentBase
    {
        /// <summary>
        /// Circuit name. Name must not be empty
        /// </summary>
        private string _name;

        /// <summary>
        /// Gets and sets citcuit name
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Название цепи не должно быть пустым");
                }

                _name = value;
            }
        }

        /// <summary>
        /// Create an instance of <see cref="Circuit"/>
        /// </summary>
        /// <param name="name"></param>
        public Circuit(string name)
        {
            Name = name;
            SubSegments = new ObservableCollection<ISegment>();
            SubSegments.CollectionChanged += OnCollectionChanged;
        }
        
        /// <summary>
        /// Create an instance of <see cref="Circuit"/>
        /// </summary>
        public Circuit()
        {
            SubSegments = new ObservableCollection<ISegment>();
            SubSegments.CollectionChanged += OnCollectionChanged;
        }

        /// <inheritdoc/>
        public override Complex CalculateZ(double frequency)
        {
            var impedance = new Complex();
            foreach (var segment in SubSegments)
            {
                impedance += segment.CalculateZ(frequency);
            }

            return impedance;
        }

        /// <summary>
        /// Method for calculating impedance of the circuit
        /// </summary>
        /// <param name="frequencies"></param>
        /// <returns></returns>
        public Complex[] CalculateZ(List<double> frequencies)
        {
            var impedances = new List<Complex>();
            foreach (var frequency in frequencies)
            {
                impedances.Add(CalculateZ(frequency));
            }

            return impedances.ToArray();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Name;
        }

        /// <inheritdoc/>
        public override object Clone()
        {
            var circuit = (Circuit)base.Clone();
            circuit.Name = Name;

            return circuit;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj))
            {
                return false;
            }

            var circuit = (Circuit)obj;

            if (Name != circuit.Name)
            {
                return false;
            }

            return true;
        }
    }
}