using System;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Sarsa learning algorithm (on-policy temporal difference control).
    /// </summary>
    public class Sarsa
    {
        private int states;         // Number of possible states
        private int actions;        // Number of possible actions
        private double[][] qvalues; // Q-value table
        private IExplorationPolicy explorationPolicy;
        private double discountFactor = 0.95; // Discount factor
        private double learningRate = 0.25;   // Learning rate

        /// <summary>
        /// Number of possible states.
        /// </summary>
        public int StatesCount => states;

        /// <summary>
        /// Number of possible actions.
        /// </summary>
        public int ActionsCount => actions;

        /// <summary>
        /// Exploration policy used to choose actions.
        /// </summary>
        public IExplorationPolicy ExplorationPolicy
        {
            get => explorationPolicy;
            set => explorationPolicy = value;
        }

        /// <summary>
        /// Learning rate, in the range [0, 1].
        /// </summary>
        public double LearningRate
        {
            get => learningRate;
            set
            {
                if (value < 0 || value > 1)
                    throw new ArgumentOutOfRangeException("Learning rate must be between 0 and 1.");
                learningRate = value;
            }
        }

        /// <summary>
        /// Discount factor, in the range [0, 1].
        /// </summary>
        public double DiscountFactor
        {
            get => discountFactor;
            set
            {
                if (value < 0 || value > 1)
                    throw new ArgumentOutOfRangeException("Discount factor must be between 0 and 1.");
                discountFactor = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the Sarsa class.
        /// </summary>
        public Sarsa(int states, int actions, IExplorationPolicy explorationPolicy, bool randomize = true)
        {
            this.states = states;
            this.actions = actions;
            this.explorationPolicy = explorationPolicy;

            qvalues = new double[states][];
            for (int i = 0; i < states; i++)
            {
                qvalues[i] = new double[actions];
            }

            if (randomize)
            {
                var rand = new System.Random();
                for (int i = 0; i < states; i++)
                {
                    for (int j = 0; j < actions; j++)
                    {
                        qvalues[i][j] = rand.NextDouble() / 10.0; // Small random values
                    }
                }
            }
        }

        /// <summary>
        /// Gets the next action for the given state based on the exploration policy.
        /// </summary>
        public int GetAction(int state)
        {
            return explorationPolicy.ChooseAction(qvalues[state]);
        }

        /// <summary>
        /// Updates the Q-value for a non-terminal state-action pair.
        /// </summary>
        public void UpdateState(int previousState, int previousAction, double reward, int nextState, int nextAction)
        {
            double[] previousActionEstimations = qvalues[previousState];

            // Update Q-value for the previous state-action pair
            double target = reward + discountFactor * qvalues[nextState][nextAction];
            previousActionEstimations[previousAction] = (1.0 - learningRate) * previousActionEstimations[previousAction]
                                                        + learningRate * target;
        }

        /// <summary>
        /// Updates the Q-value for a terminal state-action pair.
        /// </summary>
        public void UpdateState(int previousState, int previousAction, double reward)
        {
            double[] previousActionEstimations = qvalues[previousState];

            // Update Q-value for the terminal state-action pair
            previousActionEstimations[previousAction] = (1.0 - learningRate) * previousActionEstimations[previousAction]
                                                        + learningRate * reward;
        }
    }
}
