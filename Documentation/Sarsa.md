# Sarsa Class Documentation

## Overview
The `Sarsa` class implements the Sarsa learning algorithm, which is an on-policy temporal difference control method used in reinforcement learning. This algorithm is designed to improve the decision-making process of agents by learning the value of actions taken in various states. The `Sarsa` class maintains a Q-value table that is updated based on the rewards received, allowing the agent to refine its strategy over time. This class fits within the broader context of machine learning algorithms in the `EdgeLoreMachineLearning` namespace, enabling intelligent behavior in applications such as games or simulations.

## Variables
- `int states`: Represents the number of possible states in the environment.
- `int actions`: Represents the number of possible actions that can be taken.
- `double[][] qvalues`: A two-dimensional array that holds the Q-values for each state-action pair, which indicates the expected utility of taking a certain action in a given state.
- `IExplorationPolicy explorationPolicy`: An interface that defines the exploration strategy used to select actions based on the current Q-values.
- `double discountFactor`: A value (between 0 and 1) that determines the importance of future rewards. A lower value prioritizes immediate rewards, while a higher value emphasizes long-term rewards.
- `double learningRate`: A value (between 0 and 1) that dictates how much new information overrides old information in the learning process.

## Functions
- `public int StatesCount`: A property that returns the total number of states available in the environment.
  
- `public int ActionsCount`: A property that returns the total number of actions available to the agent.

- `public IExplorationPolicy ExplorationPolicy`: A property that allows getting or setting the exploration policy used for action selection.

- `public double LearningRate`: A property that gets or sets the learning rate, ensuring it remains within the range [0, 1].

- `public double DiscountFactor`: A property that gets or sets the discount factor, ensuring it remains within the range [0, 1].

- `public Sarsa(int states, int actions, IExplorationPolicy explorationPolicy, bool randomize = true)`: A constructor that initializes a new instance of the `Sarsa` class. It sets up the Q-value table and optionally populates it with small random values.

- `public int GetAction(int state)`: A method that retrieves the next action to take for a given state based on the exploration policy.

- `public void UpdateState(int previousState, int previousAction, double reward, int nextState, int nextAction)`: A method that updates the Q-value for a non-terminal state-action pair using the received reward and the next state-action pair.

- `public void UpdateState(int previousState, int previousAction, double reward)`: A method that updates the Q-value for a terminal state-action pair using the received reward.