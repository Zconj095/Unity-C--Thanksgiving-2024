using System;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Q-Learning algorithm implementation.
    /// </summary>
    public class QLearning
    {
        private int states;
        private int actions;
        private double[][] qvalues;
        private IExplorationPolicy explorationPolicy;
        private double discountFactor = 0.95;
        private double learningRate = 0.25;

        /// <summary>
        /// Number of possible states.
        /// </summary>
        public int StatesCount => states;

        /// <summary>
        /// Number of possible actions.
        /// </summary>
        public int ActionsCount => actions;

        /// <summary>
        /// Exploration policy.
        /// </summary>
        public IExplorationPolicy ExplorationPolicy
        {
            get => explorationPolicy;
            set => explorationPolicy = value;
        }

        /// <summary>
        /// Learning rate, [0, 1].
        /// </summary>
        public double LearningRate
        {
            get => learningRate;
            set
            {
                if (value < 0 || value > 1.0)
                    throw new ArgumentOutOfRangeException("Learning rate should be between 0 and 1.");
                learningRate = value;
            }
        }

        /// <summary>
        /// Discount factor, [0, 1].
        /// </summary>
        public double DiscountFactor
        {
            get => discountFactor;
            set
            {
                if (value < 0 || value > 1.0)
                    throw new ArgumentOutOfRangeException("Discount factor should be between 0 and 1.");
                discountFactor = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the QLearning class.
        /// </summary>
        public QLearning(int states, int actions, IExplorationPolicy explorationPolicy, bool randomize = true)
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
                        qvalues[i][j] = rand.NextDouble() / 10.0;
                    }
                }
            }
        }

        /// <summary>
        /// Get the next action for a given state based on the exploration policy.
        /// </summary>
        public int GetAction(int state)
        {
            return explorationPolicy.ChooseAction(qvalues[state]);
        }

        /// <summary>
        /// Update the Q-value for a state-action pair.
        /// </summary>
        public void UpdateState(int previousState, int action, double reward, int nextState)
        {
            double[] nextActionEstimations = qvalues[nextState];
            double maxNextExpectedReward = double.MinValue;

            foreach (var estimation in nextActionEstimations)
            {
                if (estimation > maxNextExpectedReward)
                {
                    maxNextExpectedReward = estimation;
                }
            }

            double[] previousActionEstimations = qvalues[previousState];
            previousActionEstimations[action] = (1.0 - learningRate) * previousActionEstimations[action]
                                                + learningRate * (reward + discountFactor * maxNextExpectedReward);
        }
    }

    /// <summary>
    /// Interface for exploration policies.
    /// </summary>
    public interface IExplorationPolicy
    {
        int ChooseAction(double[] qvalues);
    }

    /// <summary>
    /// Epsilon-greedy exploration policy implementation.
    /// </summary>
    public class EpsilonGreedyPolicy : IExplorationPolicy
    {
        private readonly double epsilon;

        public EpsilonGreedyPolicy(double epsilon)
        {
            this.epsilon = epsilon;
        }

        public int ChooseAction(double[] qvalues)
        {
            if (UnityEngine.Random.value < epsilon)
            {
                return UnityEngine.Random.Range(0, qvalues.Length);
            }

            int bestAction = 0;
            double maxQValue = qvalues[0];
            for (int i = 1; i < qvalues.Length; i++)
            {
                if (qvalues[i] > maxQValue)
                {
                    maxQValue = qvalues[i];
                    bestAction = i;
                }
            }

            return bestAction;
        }
    }
}
