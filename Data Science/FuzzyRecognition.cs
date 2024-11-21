using UnityEngine;
using System.Collections.Generic;

public class FuzzyRecognition : MonoBehaviour
{
    /// <summary>
    /// Represents a fuzzy set with a name and a degree of membership.
    /// </summary>
    public struct FuzzySet
    {
        public string Name; // Name of the fuzzy set
        public float DegreeOfMembership; // Membership degree (0 to 1)

        public FuzzySet(string name, float degree)
        {
            Name = name;
            DegreeOfMembership = Mathf.Clamp01(degree); // Ensure degree is always between 0 and 1
        }
    }

    [Header("Fuzzy Recognition Settings")]
    [SerializeField] private float recognitionThreshold = 0.7f; // Minimum degree of membership for recognition
    [SerializeField] private float distanceFactor = 5f; // Maximum distance for recognition
    [SerializeField, Range(0f, 1f)] private float similarityFactor = 0.8f; // Factor for calculating similarity

    private List<FuzzySet> fuzzySets = new List<FuzzySet>(); // Fuzzy sets for recognition
    private List<GameObject> objectsInScene = new List<GameObject>(); // Scene objects to recognize

    private void Start()
    {
        InitializeObjectsInScene();
        InitializeFuzzySets();
    }

    private void Update()
    {
        UpdateFuzzyRecognition();
        CheckRecognition();
    }

    /// <summary>
    /// Initializes objects in the scene with random positions and tags.
    /// </summary>
    private void InitializeObjectsInScene()
    {
        objectsInScene.Add(new GameObject("Object1"));
        objectsInScene.Add(new GameObject("Object2"));
        objectsInScene.Add(new GameObject("Object3"));

        foreach (var obj in objectsInScene)
        {
            obj.transform.position = new Vector3(
                Random.Range(-10f, 10f),
                Random.Range(0f, 10f),
                Random.Range(-10f, 10f)
            );

            obj.tag = Random.Range(0, 2) == 0 ? "Recognizable" : "Unrecognizable";
        }
    }

    /// <summary>
    /// Initializes fuzzy sets based on the number of objects in the scene.
    /// </summary>
    private void InitializeFuzzySets()
    {
        foreach (var obj in objectsInScene)
        {
            fuzzySets.Add(new FuzzySet(obj.name, 0f));
        }
    }

    /// <summary>
    /// Updates the fuzzy recognition for each object in the scene.
    /// </summary>
    private void UpdateFuzzyRecognition()
    {
        for (int i = 0; i < objectsInScene.Count; i++)
        {
            GameObject obj = objectsInScene[i];
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            float similarity = CalculateSimilarity(obj);

            float recognitionDegree = GetRecognitionDegree(distance, similarity);
            fuzzySets[i] = new FuzzySet(obj.name, recognitionDegree);

            Debug.Log($"Recognition degree for {obj.name} (Tag: {obj.tag}): {recognitionDegree:F2}");
        }
    }

    /// <summary>
    /// Calculates the recognition degree based on distance and similarity.
    /// </summary>
    private float GetRecognitionDegree(float distance, float similarity)
    {
        float distanceDegree = Mathf.Clamp01(1 - (distance / distanceFactor));
        float similarityDegree = Mathf.Clamp01(similarity);

        return Mathf.Min(distanceDegree, similarityDegree); // Combine using the minimum for fuzzy logic
    }

    /// <summary>
    /// Calculates the similarity of an object based on its tag or properties.
    /// </summary>
    private float CalculateSimilarity(GameObject obj)
    {
        switch (obj.tag)
        {
            case "Recognizable":
                return 1f; // Fully similar
            case "Unrecognizable":
                return 0.3f; // Partially similar
            default:
                return 0f; // No similarity
        }
    }

    /// <summary>
    /// Checks and logs objects recognized based on the fuzzy recognition threshold.
    /// </summary>
    private void CheckRecognition()
    {
        foreach (var fuzzySet in fuzzySets)
        {
            if (fuzzySet.DegreeOfMembership >= recognitionThreshold)
            {
                Debug.Log($"{fuzzySet.Name} is recognized with a degree of {fuzzySet.DegreeOfMembership:F2}");
                // Add behavior for recognized objects (e.g., triggering events)
            }
        }
    }
}
