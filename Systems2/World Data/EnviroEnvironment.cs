using System.Collections.Generic;
using UnityEngine;

public class EnviroEnvironment : MonoBehaviour
{
    // Data structure for Biome
    public class Biome
    {
        public string Name;
        public List<string> Plants; // List of plant species
        public List<string> Animals; // List of animal species
        public string Description;

        public Biome(string name, List<string> plants, List<string> animals, string description)
        {
            Name = name;
            Plants = plants;
            Animals = animals;
            Description = description;
        }

        public void DisplayDetails()
        {
            Debug.Log($"Biome: {Name}");
            Debug.Log($"Plants: {string.Join(", ", Plants)}");
            Debug.Log($"Animals: {string.Join(", ", Animals)}");
            Debug.Log($"Description: {Description}");
        }
    }

    // Data structure for Habitat
    public class Habitat
    {
        public string Name;
        public string BiomeType; // Related biome type
        public string Requirements; // Minimum requirements for existence (e.g., Water, Shade)
        public string Description;

        public Habitat(string name, string biomeType, string requirements, string description)
        {
            Name = name;
            BiomeType = biomeType;
            Requirements = requirements;
            Description = description;
        }

        public void DisplayDetails()
        {
            Debug.Log($"Habitat: {Name}");
            Debug.Log($"Biome Type: {BiomeType}");
            Debug.Log($"Requirements: {Requirements}");
            Debug.Log($"Description: {Description}");
        }
    }

    // Data structure for Ecosystem
    public class Ecosystem
    {
        public string Name;
        public List<Biome> Biomes; // Collection of biomes
        public string WeatherPattern; // Weather and seasonal impacts
        public string TerrainType; // Terrain description
        public string Description;

        public Ecosystem(string name, List<Biome> biomes, string weatherPattern, string terrainType, string description)
        {
            Name = name;
            Biomes = biomes;
            WeatherPattern = weatherPattern;
            TerrainType = terrainType;
            Description = description;
        }

        public void DisplayDetails()
        {
            Debug.Log($"Ecosystem: {Name}");
            Debug.Log($"Weather Pattern: {WeatherPattern}");
            Debug.Log($"Terrain Type: {TerrainType}");
            Debug.Log($"Description: {Description}");
            Debug.Log("Biomes in this Ecosystem:");
            foreach (var biome in Biomes)
            {
                Debug.Log($"- {biome.Name}");
            }
        }
    }

    // Example data
    void Start()
    {
        // Creating sample biomes
        Biome forest = new Biome(
            "Forest",
            new List<string> { "Oak Trees", "Pine Trees", "Shrubs" },
            new List<string> { "Deer", "Birds", "Wolves" },
            "A region covered with dense trees and diverse wildlife."
        );

        Biome desert = new Biome(
            "Desert",
            new List<string> { "Cacti", "Succulents" },
            new List<string> { "Snakes", "Lizards", "Scorpions" },
            "A dry, arid region with minimal vegetation."
        );

        // Creating sample habitats
        Habitat forestHabitat = new Habitat(
            "Dense Forest Habitat",
            "Forest",
            "Water sources, Dense vegetation, Moderate temperature",
            "Supports diverse life forms due to optimal conditions."
        );

        Habitat desertHabitat = new Habitat(
            "Desert Habitat",
            "Desert",
            "Limited water, Extreme temperature adaptations",
            "Supports specialized life forms adapted to arid conditions."
        );

        // Creating a sample ecosystem
        Ecosystem mixedEcosystem = new Ecosystem(
            "Mixed Terrain Ecosystem",
            new List<Biome> { forest, desert },
            "Seasonal rain and dry spells",
            "Varied terrain including rocky areas, sandy plains, and dense forests",
            "A combination of biomes with unique weather and terrain impacts."
        );

        // Display details
        forest.DisplayDetails();
        desert.DisplayDetails();
        forestHabitat.DisplayDetails();
        desertHabitat.DisplayDetails();
        mixedEcosystem.DisplayDetails();
    }
}
