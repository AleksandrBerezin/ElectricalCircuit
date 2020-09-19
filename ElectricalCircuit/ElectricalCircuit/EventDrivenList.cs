using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalCircuit
{
    public sealed class EventDrivenList<T> : List<T>
    {
        public event ValueChangeEventHandler ItemAdded;
        public event ValueChangeEventHandler ItemRemoved;

        public void Add(T item)
        {
            base.Add(item);
            ItemAdded?.Invoke(this, EventArgs.Empty);
        }

        public void Remove(T item)
        {
            base.Remove(item);
            ItemRemoved?.Invoke(this, EventArgs.Empty);
        }
    }
}