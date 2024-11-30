using System;
using System.Collections.Generic;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    public class MultilabelSupportVectorLearning<TKernel, TInput>
        where TKernel : IKernel<TInput>
        where TInput : IList<float> // Added constraint for TInput
    {
        private TKernel kernel;
        private MultilabelSupportVectorMachine<TKernel, TInput> model;

        public Func<(int Class, SupportVectorMachine<TKernel, TInput> Machine), ISupervisedLearning<SupportVectorMachine<TKernel, TInput>, TInput, bool>> Learner { get; set; }

        public MultilabelSupportVectorLearning(TKernel kernel)
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

        public MultilabelSupportVectorLearning(MultilabelSupportVectorMachine<TKernel, TInput> machine)
        {
            model = machine;
            Learner = parameters =>
            {
                var binaryMachine = machine[parameters.Class];
                return new SequentialMinimalOptimization<TKernel, TInput>
                {
                    Model = binaryMachine
                };
            };
        }

        public MultilabelSupportVectorMachine<TKernel, TInput> Train(TInput[] inputs, int[][] outputs)
        {
            int numberOfClasses = outputs[0].Length;
            int inputDimensions = inputs[0].Count;

            model = new MultilabelSupportVectorMachine<TKernel, TInput>(inputDimensions, kernel, numberOfClasses);

            for (int i = 0; i < numberOfClasses; i++)
            {
                var binaryOutputs = ExtractBinaryOutputs(outputs, i);
                var machine = model[i];
                var learner = Learner((i, machine));
                learner.Train(inputs, binaryOutputs);
            }

            Debug.Log("Multilabel training completed.");
            return model;
        }

        public bool[] Predict(TInput input)
        {
            return model.Predict(input);
        }

        private bool[] ExtractBinaryOutputs(int[][] outputs, int classIndex)
        {
            var binaryOutputs = new bool[outputs.Length];
            for (int i = 0; i < outputs.Length; i++)
            {
                binaryOutputs[i] = outputs[i][classIndex] == 1;
            }
            return binaryOutputs;
        }
    }

    public class MultilabelSupportVectorMachine<TKernel, TInput>
        where TKernel : IKernel<TInput>
    {
        private SupportVectorMachine<TKernel, TInput>[] machines;

        public MultilabelSupportVectorMachine(int inputs, TKernel kernel, int classes)
        {
            machines = new SupportVectorMachine<TKernel, TInput>[classes];
            for (int i = 0; i < classes; i++)
            {
                machines[i] = new SupportVectorMachine<TKernel, TInput>(inputs, kernel);
            }
        }

        public SupportVectorMachine<TKernel, TInput> this[int index] => machines[index];

        public bool[] Predict(TInput input)
        {
            var predictions = new bool[machines.Length];
            for (int i = 0; i < machines.Length; i++)
            {
                predictions[i] = machines[i].Predict(input);
            }
            return predictions;
        }
    }

}
