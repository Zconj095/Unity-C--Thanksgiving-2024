# QLearning.cs

## Overview
The `QLearning` class implements the Q-Learning algorithm, which is a reinforcement learning technique used to learn the value of actions in given states. This class is part of the `EdgeLoreMachineLearning` namespace and allows the interaction between an agent and its environment through state-action pairs. It maintains Q-values, which represent the expected future rewards for each action taken in a particular state, and updates these values based on the rewards received. The class also integrates an exploration policy to balance exploration and exploitation during the learning process.

## Variables
- `states`: An integer representing the number of possible states in the environment.
- `actions`: An integer representing the number of possible actions the agent can take.
- `qvalues`: A two-dimensional array of doubles that stores the Q-values for each state-action pair.
- `explorationPolicy`: An instance of `IExplorationPolicy`, which defines the strategy for exploring actions.
- `discountFactor`: A double value (default is 0.95) that determines the importance of future rewards in the learning process.
- `learningRate`: A double value (default is 0.25) that controls how much new information overrides old information in the learning process.

## Functions
- `QLearning(int states, int actions, IExplorationPolicy explorationPolicy, bool randomize = true)`: Constructor that initializes a new instance of the `QLearning` class with the specified number of states and actions, an exploration policy, and an optional parameter to randomize Q-values.

- `int StatesCount`: Property that returns the number of possible states.

- `int ActionsCount`: Property that returns the number of possible actions.

- `IExplorationPolicy ExplorationPolicy`: Property that gets or sets the exploration policy used in the Q-learning process.

- `double LearningRate`: Property that gets or sets the learning rate, with validation to ensure it is between 0 and 1.

- `double DiscountFactor`: Property that gets or sets the discount factor, with validation to ensure it is between 0 and 1.

- `int GetAction(int state)`: Method that retrieves the next action for a given state based on the defined exploration policy.

- `void UpdateState(int previousState, int action, double reward, int nextState)`: Method that updates the Q-value for a specific state-action pair based on the reward received and the estimated future rewards from the next state.

## Interfaces
- `IExplorationPolicy`: An interface that defines the method `ChooseAction(double[] qvalues)`, which returns the action to take given the Q-values.

## Classes
- `EpsilonGreedyPolicy`: A class that implements the `IExplorationPolicy` interface using the epsilon-greedy strategy, which chooses a random action with probability `epsilon` and the best action based on Q-values otherwise. It contains:
  - `EpsilonGreedyPolicy(double epsilon)`: Constructor that initializes the epsilon value.
  - `int ChooseAction(double[] qvalues)`: Method that selects an action based on the epsilon-greedy policy.