using UnityEngine;
using System.Collections.Generic;
using System;

// NeuralChakra class for Unity implementation
public class NeuralChakra : MonoBehaviour
{
    public float activation;
    public float synapticFlux;
    public List<float> weights;
    
    public void Initialize(int inputCount)
    {
        weights = new List<float>();
        for (int i = 0; i < inputCount; i++)
        {
            weights.Add(UnityEngine.Random.Range(-1f, 1f));
        }
    }

    public void HebbianLearning(List<float> inputs, float learningRate)
    {
        for (int i = 0; i < weights.Count; i++)
        {
            weights[i] += learningRate * activation * inputs[i];
        }
    }
}

// Neural network layer
public class NeuralChakraLayer : MonoBehaviour
{
    public List<NeuralChakra> neurons;
    
    public void Initialize(int neuronCount, int inputCount)
    {
        neurons = new List<NeuralChakra>();
        for (int i = 0; i < neuronCount; i++)
        {
            GameObject neuronObj = new GameObject($"Neuron_{i}");
            neuronObj.transform.parent = this.transform;
            NeuralChakra neuron = neuronObj.AddComponent<NeuralChakra>();
            neuron.Initialize(inputCount);
            neurons.Add(neuron);
        }
    }

    public List<float> Forward(List<float> inputs)
    {
        List<float> activations = new List<float>();
        foreach (NeuralChakra neuron in neurons)
        {
            neuron.activation = 0f;
            for (int i = 0; i < inputs.Count; i++)
            {
                neuron.activation += neuron.weights[i] * inputs[i];
            }
            // Changed from Mathf.Tanh to (float)Math.Tanh
            neuron.activation = (float)Math.Tanh(neuron.activation);
            activations.Add(neuron.activation);
        }
        return activations;
    }
}

// Main neural network controller that integrates with the chakra system
public class HebbianChakraNetwork : MonoBehaviour
{
    public ChakraSystem chakraSystem;
    private NeuralChakraLayer inputLayer;
    private NeuralChakraLayer hiddenLayer;
    private NeuralChakraLayer outputLayer;
    
    public float learningRate = 0.01f;
    private float centroid;
    public float desiredCentroid = 0.5f;

    void Start()
    {
        InitializeNetwork();
    }

    void InitializeNetwork()
    {
        // Create layer GameObjects
        GameObject inputLayerObj = new GameObject("InputLayer");
        GameObject hiddenLayerObj = new GameObject("HiddenLayer");
        GameObject outputLayerObj = new GameObject("OutputLayer");
        
        inputLayerObj.transform.parent = transform;
        hiddenLayerObj.transform.parent = transform;
        outputLayerObj.transform.parent = transform;

        // Initialize layers
        inputLayer = inputLayerObj.AddComponent<NeuralChakraLayer>();
        hiddenLayer = hiddenLayerObj.AddComponent<NeuralChakraLayer>();
        outputLayer = outputLayerObj.AddComponent<NeuralChakraLayer>();

        inputLayer.Initialize(7, 7);  // 7 chakras
        hiddenLayer.Initialize(4, 7);
        outputLayer.Initialize(1, 4);
    }

    public void Train()
    {
        // Get chakra energies as input
        List<float> inputs = new List<float>();
        for (int i = 0; i < chakraSystem.chakraEnergies.Length; i++)
        {
            inputs.Add(chakraSystem.chakraEnergies[i] / chakraSystem.maxEnergy);
        }

        // Forward pass
        List<float> hiddenActivations = inputLayer.Forward(inputs);
        List<float> outputActivations = hiddenLayer.Forward(hiddenActivations);

        // Apply Hebbian learning
        foreach (NeuralChakra neuron in inputLayer.neurons)
        {
            neuron.HebbianLearning(inputs, learningRate);
        }
        foreach (NeuralChakra neuron in hiddenLayer.neurons)
        {
            neuron.HebbianLearning(hiddenActivations, learningRate);
        }

        // Update centroid
        ComputeCentroid(outputActivations);
        
        // Apply cybernetic feedback
        CyberneticFeedback();
    }

    private void ComputeCentroid(List<float> activations)
    {
        centroid = 0f;
        foreach (float activation in activations)
        {
            centroid += activation;
        }
        centroid /= activations.Count;
    }

    private void CyberneticFeedback()
    {
        float feedback = learningRate * (desiredCentroid - centroid);
        foreach (NeuralChakra neuron in hiddenLayer.neurons)
        {
            for (int i = 0; i < neuron.weights.Count; i++)
            {
                neuron.weights[i] += feedback;
            }
        }

        // Apply feedback to chakra system
        float feedbackEnergy = feedback * chakraSystem.maxEnergy;
        for (int i = 0; i < chakraSystem.chakraEnergies.Length; i++)
        {
            chakraSystem.AddEnergyToChakra(i, feedbackEnergy);
        }
    }

    // Quantum entanglement simulation between chakras
    public float CalculateChakraEntanglement(int chakra1, int chakra2)
    {
        float activation1 = chakraSystem.GetChakraEnergy(chakra1) / chakraSystem.maxEnergy;
        float activation2 = chakraSystem.GetChakraEnergy(chakra2) / chakraSystem.maxEnergy;
        return Mathf.Cos(activation1 - activation2);
    }

    void Update()
    {
        // Train the network each frame
        Train();

        // Example of checking entanglement between heart and throat chakras
        float entanglement = CalculateChakraEntanglement(3, 4);
        if (entanglement > 0.8f)
        {
            // High entanglement might trigger special effects or bonuses
            chakraSystem.AlignChakras();
        }
    }
}