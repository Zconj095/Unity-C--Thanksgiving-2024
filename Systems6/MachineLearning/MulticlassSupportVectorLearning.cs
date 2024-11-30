using System;
using System.Collections.Generic;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    public class MulticlassSupportVectorLearning<TKernel, TInput>
        where TKernel : IKernel<TInput>
        where TInput : IList<float> // Constrain TInput to IList<float>
    {
        private TKernel kernel;
        private MulticlassSupportVectorMachine<TKernel, TInput> model;

        public Func<(int Class1, int Class2, SupportVectorMachine<TKernel, TInput> Machine), ISupervisedLearning<SupportVectorMachine<TKernel, TInput>, TInput, bool>> Learner { get; set; }

        public MulticlassSupportVectorLearning(TKernel kernel)
        {
            this.kernel = kernel;
            Learner = parameters =>
            {
                return new SequentialMinimalOptimization<TKernel, TInput>
                {
                    Model = parameters.Machine
                };
            };
        }

        public MulticlassSupportVectorLearning(MulticlassSupportVectorMachine<TKernel, TInput> machine)
        {
            model = machine;
            Learner = parameters =>
            {
                var pairwiseModel = machine[parameters.Class1, parameters.Class2];
                return new SequentialMinimalOptimization<TKernel, TInput>
                {
                    Model = pairwiseModel
                };
            };
        }

        public MulticlassSupportVectorMachine<TKernel, TInput> Train(TInput[] inputs, int[] outputs)
        {
            var uniqueClasses = new HashSet<int>(outputs);
            int numberOfClasses = uniqueClasses.Count;
            int inputDimensions = inputs[0].Count;

            model = new MulticlassSupportVectorMachine<TKernel, TInput>(inputDimensions, kernel, numberOfClasses);

            foreach (var (class1, class2) in GenerateClassPairs(uniqueClasses))
            {
                var machine = model[class1, class2];
                var learner = Learner((class1, class2, machine));

                var pairInputs = FilterInputsForPair(inputs, outputs, class1, class2, out var pairOutputs);
                learner.Train(pairInputs, pairOutputs);
            }

            Debug.Log("Multiclass training completed.");
            return model;
        }

        public int Predict(TInput input)
        {
            return model.Predict(input);
        }

        private IEnumerable<(int Class1, int Class2)> GenerateClassPairs(HashSet<int> classes)
        {
            var classList = new List<int>(classes);
            for (int i = 0; i < classList.Count; i++)
            {
                for (int j = i + 1; j < classList.Count; j++)
                {
                    yield return (classList[i], classList[j]);
                }
            }
        }

        private TInput[] FilterInputsForPair(TInput[] inputs, int[] outputs, int class1, int class2, out bool[] pairOutputs)
        {
            var selectedInputs = new List<TInput>();
            var selectedOutputs = new List<bool>();

            for (int i = 0; i < outputs.Length; i++)
            {
                if (outputs[i] == class1 || outputs[i] == class2)
                {
                    selectedInputs.Add(inputs[i]);
                    selectedOutputs.Add(outputs[i] == class1);
                }
            }

            pairOutputs = selectedOutputs.ToArray();
            return selectedInputs.ToArray();
        }
    }

    public class MulticlassSupportVectorMachine<TKernel, TInput>
        where TKernel : IKernel<TInput>
    {
        private Dictionary<(int Class1, int Class2), SupportVectorMachine<TKernel, TInput>> machines;

        public MulticlassSupportVectorMachine(int inputs, TKernel kernel, int classes)
        {
            machines = new Dictionary<(int, int), SupportVectorMachine<TKernel, TInput>>();
            for (int i = 0; i < classes; i++)
            {
                for (int j = i + 1; j < classes; j++)
                {
                    machines[(i, j)] = new SupportVectorMachine<TKernel, TInput>(inputs, kernel);
                }
            }
        }

        public SupportVectorMachine<TKernel, TInput> this[int class1, int class2] => machines[(class1, class2)];

        public int Predict(TInput input)
        {
            var votes = new Dictionary<int, int>();

            foreach (var ((class1, class2), machine) in machines)
            {
                int predicted = machine.Predict(input) ? class1 : class2;

                if (!votes.ContainsKey(predicted))
                {
                    votes[predicted] = 0;
                }
                votes[predicted]++;
            }

            int bestClass = -1;
            int maxVotes = -1;

            foreach (var (cls, count) in votes)
            {
                if (count > maxVotes)
                {
                    bestClass = cls;
                    maxVotes = count;
                }
            }

            return bestClass;
        }
    }

    public class SupportVectorMachine<TKernel, TInput>
        where TKernel : IKernel<TInput>
    {
        private TKernel kernel;

        public SupportVectorMachine(int inputs, TKernel kernel)
        {
            this.kernel = kernel;
        }

        public bool Predict(TInput input)
        {
            // Example prediction logic
            return true; // Placeholder
        }
    }

    public interface IKernel<TInput>
    {
        double Function(TInput input1, TInput input2);
    }

    public interface ISupervisedLearning<TModel, TInput, TOutput>
    {
        void Train(TInput[] inputs, TOutput[] outputs);
    }

    public class SequentialMinimalOptimization<TKernel, TInput> : ISupervisedLearning<SupportVectorMachine<TKernel, TInput>, TInput, bool>
        where TKernel : IKernel<TInput>
    {
        public SupportVectorMachine<TKernel, TInput> Model { get; set; }

        public void Train(TInput[] inputs, bool[] outputs)
        {
            Debug.Log("Training binary SVM.");
        }
    }
}
