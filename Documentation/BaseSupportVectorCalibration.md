# EdgeLoreMachineLearning

## Overview
The `EdgeLoreMachineLearning` script defines a framework for implementing support vector machines (SVM) in Unity. It provides an interface for SVMs, a base class for calibration of these models, and a sample implementation of an SVM. The primary purpose of this script is to facilitate the creation and training of SVM models that can classify input data based on learned patterns.

## Variables
- **model**: An instance of the SVM model being calibrated, of type `TModel`.
- **kernel**: The kernel function used by the SVM model, of type `TKernel`.
- **Input**: An array of input vectors used for training the SVM.
- **Output**: An array of boolean values representing the output labels corresponding to the input vectors.

## Functions
### Interface: IBaseSystemSupportVectorMachine<TInput>
- **int Predict(TInput input)**: Predicts the class label for a given input.
- **TInput[] SupportVectors { get; set; }**: Gets or sets the support vectors for the SVM.
- **double[] Weights { get; set; }**: Gets or sets the weights for the support vectors.
- **double Threshold { get; set; }**: Gets or sets the threshold for classification.
- **BaseIKernel<TInput> Kernel { get; set; }**: Gets or sets the kernel used for the SVM.

### Interface: BaseIKernel<TInput>
- **double Compute(TInput x, TInput y)**: Computes the value of the kernel function for two input values.

### Interface: BaseILinear<TInput>
- **int GetInputLength(TInput[] inputs)**: An optional method to get the length of the input if required.

### Abstract Class: BaseSupportVectorCalibration<TModel, TKernel, TInput>
- **BaseSupportVectorCalibration(TModel machine)**: Initializes the calibration class with the given machine.
- **BaseSupportVectorCalibration()**: Default constructor.
- **bool IsLinear**: Checks if the model is linear.
- **TModel Model { get; set; }**: Gets or sets the machine being calibrated.
- **TKernel Kernel**: Provides access to the kernel of the machine.
- **abstract void InnerRun()**: Abstract method to implement the learning algorithm.
- **TModel Learn(TInput[] x, bool[] y, double[] weights = null)**: Trains the model to map inputs to outputs.
- **protected virtual TModel Create(int inputs, TKernel kernel)**: Creates a new machine model for learning.
- **private int GetNumberOfInputs(TInput[] inputs)**: Gets the number of inputs in the dataset.

### Class: BaseSystemSupportVectorMachine<TKernel, TInput>
- **int Predict(TInput input)**: Example prediction logic for the SVM model.
- **BaseSystemSupportVectorMachine(int inputs, TKernel kernel)**: Constructor that initializes the kernel and other properties as needed.
- **TInput[] SupportVectors { get; set; }**: Implementation of the support vectors property.
- **double[] Weights { get; set; }**: Implementation of the weights property.
- **double Threshold { get; set; }**: Implementation of the threshold property.
- **BaseIKernel<TInput> Kernel { get; set; }**: Implementation of the kernel property.