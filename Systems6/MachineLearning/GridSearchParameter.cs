using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    [Serializable]
    public struct GridSearchParameter
    {
        [SerializeField] private string name;
        [SerializeField] private double value;

        /// <summary>
        ///   Gets the name of the parameter.
        /// </summary>
        public string Name => name;

        /// <summary>
        ///   Gets the value of the parameter.
        /// </summary>
        public double Value => value;

        /// <summary>
        ///   Constructs a new parameter.
        /// </summary>
        /// <param name="name">The name for the parameter.</param>
        /// <param name="value">The value for the parameter.</param>
        public GridSearchParameter(string name, double value)
        {
            this.name = name;
            this.value = value;
        }

        public override bool Equals(object obj)
        {
            if (obj is GridSearchParameter parameter)
            {
                return parameter.name == name && Math.Abs(parameter.value - value) < 1e-10;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return name.GetHashCode() ^ value.GetHashCode();
        }

        public static bool operator ==(GridSearchParameter parameter1, GridSearchParameter parameter2)
        {
            return parameter1.Equals(parameter2);
        }

        public static bool operator !=(GridSearchParameter parameter1, GridSearchParameter parameter2)
        {
            return !parameter1.Equals(parameter2);
        }

        public override string ToString()
        {
            return $"{name}: {value}";
        }

        public static implicit operator double(GridSearchParameter param)
        {
            return param.Value;
        }
    }

    [Serializable]
    public class GridSearchParameterCollection : KeyedCollection<string, GridSearchParameter>
    {
        /// <summary>
        ///   Constructs a new collection of GridSearchParameter objects.
        /// </summary>
        public GridSearchParameterCollection(params GridSearchParameter[] parameters)
        {
            foreach (var param in parameters)
            {
                Add(param);
            }
        }

        /// <summary>
        ///   Constructs a new collection of GridSearchParameter objects.
        /// </summary>
        public GridSearchParameterCollection(IEnumerable<GridSearchParameter> parameters)
        {
            foreach (var param in parameters)
            {
                Add(param);
            }
        }

        /// <summary>
        ///   Returns the identifying value for an item in this collection.
        /// </summary>
        protected override string GetKeyForItem(GridSearchParameter item)
        {
            return item.Name;
        }
    }
}
