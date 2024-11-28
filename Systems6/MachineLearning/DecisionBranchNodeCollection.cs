using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Unity-compatible collection of decision nodes.
    ///   A decision branch specifies the index of an attribute whose
    ///   current value should be compared against its children nodes.
    /// </summary>
    public class DecisionBranchNodeCollection : Collection<DecisionNode>
    {
        private DecisionNode owner;

        /// <summary>
        ///   Gets or sets the index of the attribute to be
        ///   used in this stage of the decision process.
        /// </summary>
        public int AttributeIndex { get; set; }

        /// <summary>
        ///   Gets the attribute that is being used in
        ///   this stage of the decision process, given
        ///   by the current <see cref="AttributeIndex"/>.
        /// </summary>
        public DecisionVariable Attribute
        {
            get
            {
                if (owner?.Owner?.Attributes == null || AttributeIndex < 0 || AttributeIndex >= owner.Owner.Attributes.Count)
                {
                    throw new InvalidOperationException("The attribute cannot be resolved. Check the decision tree configuration.");
                }

                return owner.Owner.Attributes[AttributeIndex];
            }
        }

        /// <summary>
        ///   Gets or sets the decision node that contains this collection.
        /// </summary>
        public DecisionNode Owner
        {
            get => owner;
            set => owner = value;
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="DecisionBranchNodeCollection"/> class.
        /// </summary>
        /// <param name="owner">The <see cref="DecisionNode"/> to whom this collection belongs.</param>
        public DecisionBranchNodeCollection(DecisionNode owner)
        {
            this.owner = owner ?? throw new ArgumentNullException(nameof(owner));
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="DecisionBranchNodeCollection"/> class with specified children.
        /// </summary>
        /// <param name="attributeIndex">Index of the attribute to be processed.</param>
        /// <param name="nodes">The children nodes. Each child node should be responsible
        /// for a possible value of a discrete attribute or for a region of a continuous-valued attribute.</param>
        public DecisionBranchNodeCollection(int attributeIndex, IEnumerable<DecisionNode> nodes)
        {
            if (nodes == null)
                throw new ArgumentNullException(nameof(nodes));
            if (!nodes.Any())
                throw new ArgumentException("Node collection is empty.", nameof(nodes));

            AttributeIndex = attributeIndex;
            AddRange(nodes);
        }

        /// <summary>
        ///   Adds the elements of the specified collection to the end of the collection.
        /// </summary>
        /// <param name="children">The child nodes to be added.</param>
        public void AddRange(IEnumerable<DecisionNode> children)
        {
            if (children == null)
                throw new ArgumentNullException(nameof(children));

            foreach (var child in children)
                Add(child);
        }

        /// <summary>
        /// Returns a <see cref="string"/> representation of this collection.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this instance.</returns>
        public override string ToString()
        {
            var values = string.Join(", ", this.Select(x => x.ToString()).ToArray());
            return $"{Attribute} ({AttributeIndex}) => {values}";
        }
    }
}
