using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    public enum ModelStorageMode
    {
        AllModels,
        MinimumOnly,
        MaximumOnly
    }

    public class CrossValidationValues2<TModel>
    {
        public TModel Model { get; private set; }
        public double TrainingValue { get; private set; }
        public double ValidationValue { get; private set; }
        public object Tag { get; set; }

        public CrossValidationValues2(TModel model, double trainingValue, double validationValue)
        {
            Model = model;
            TrainingValue = trainingValue;
            ValidationValue = validationValue;
        }

        public CrossValidationValues2<TModel> Clone(bool includeModel)
        {
            var modelClone = includeModel && Model != null ? (TModel)((ICloneable)Model).Clone() : default;
            return new CrossValidationValues2<TModel>(modelClone, TrainingValue, ValidationValue)
            {
                Tag = Tag
            };
        }
    }

    public class EarlyStopping<TModel> where TModel : class, ICloneable
    {
        public int MaxIterations { get; set; }
        public double Tolerance { get; set; }
        public Dictionary<int, CrossValidationValues2<TModel>> History { get; private set; }
        public KeyValuePair<int, CrossValidationValues2<TModel>> MinValidationValue { get; private set; }
        public KeyValuePair<int, CrossValidationValues2<TModel>> MaxValidationValue { get; private set; }
        public ModelStorageMode Mode { get; set; }
        public Func<int, CrossValidationValues2<TModel>> IterationFunction { get; set; }

        public EarlyStopping()
        {
            History = new Dictionary<int, CrossValidationValues2<TModel>>();
            Tolerance = 1e-5;
        }

        public bool Compute()
        {
            double lastError = double.PositiveInfinity;

            for (int i = 0; i < MaxIterations; i++)
            {
                var value = IterationFunction?.Invoke(i);
                if (value == null)
                {
                    Debug.LogError("IterationFunction must return a valid CrossValidationValues2 object.");
                    return false;
                }

                double currentError = value.TrainingValue;

                if (Mode == ModelStorageMode.AllModels)
                {
                    var clone = value.Clone(includeModel: true);
                    History[i] = clone;
                    UpdateMinAndMaxValidationValues(i, clone);
                }
                else
                {
                    var copy = value.Clone(includeModel: false);
                    History[i] = copy;
                    UpdateMinAndMaxValidationValues(i, value);
                }

                if (Math.Abs(currentError - lastError) < Tolerance * Math.Abs(lastError))
                {
                    return true; // Converged
                }

                lastError = currentError;
            }

            return false; // Max iterations reached
        }

        private void UpdateMinAndMaxValidationValues(int iteration, CrossValidationValues2<TModel> value)
        {
            if (MinValidationValue.Value == null || value.ValidationValue < MinValidationValue.Value.ValidationValue)
            {
                MinValidationValue = new KeyValuePair<int, CrossValidationValues2<TModel>>(iteration, value.Clone(includeModel: true));
            }

            if (MaxValidationValue.Value == null || value.ValidationValue > MaxValidationValue.Value.ValidationValue)
            {
                MaxValidationValue = new KeyValuePair<int, CrossValidationValues2<TModel>>(iteration, value.Clone(includeModel: true));
            }
        }
    }
}
