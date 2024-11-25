using System.Collections.Generic;
using UnityEngine;

public class NatureListManager : MonoBehaviour
{
    // Categories for lists
    private List<string> listOfFlowers = new List<string>();
    private List<string> listOfClouds = new List<string>();
    private List<string> listOfBerries = new List<string>();

    void Start()
    {
        // Initialize lists with example data
        InitializeFlowers();
        InitializeClouds();
        InitializeBerries();

        // Display lists
        DisplayList("Flowers", listOfFlowers);
        DisplayList("Clouds", listOfClouds);
        DisplayList("Berries", listOfBerries);
    }

    // Initialize the flower list
    private void InitializeFlowers()
    {
        listOfFlowers.Add("Rose");
        listOfFlowers.Add("Tulip");
        listOfFlowers.Add("Daisy");
        listOfFlowers.Add("Sunflower");
        listOfFlowers.Add("Orchid");
    }

    // Initialize the cloud list
    private void InitializeClouds()
    {
        listOfClouds.Add("Cumulus");
        listOfClouds.Add("Stratus");
        listOfClouds.Add("Cirrus");
        listOfClouds.Add("Nimbus");
        listOfClouds.Add("Altostratus");
    }

    // Initialize the berry list
    private void InitializeBerries()
    {
        listOfBerries.Add("Strawberry");
        listOfBerries.Add("Blueberry");
        listOfBerries.Add("Raspberry");
        listOfBerries.Add("Blackberry");
        listOfBerries.Add("Goji Berry");
    }

    // Display any list
    private void DisplayList(string category, List<string> items)
    {
        Debug.Log($"--- {category} ---");
        foreach (var item in items)
        {
            Debug.Log(item);
        }
    }

    // Utility function to search for an item in a list
    public bool SearchInList(string category, string item)
    {
        List<string> targetList = GetListByCategory(category);
        if (targetList != null && targetList.Contains(item))
        {
            Debug.Log($"{item} found in {category}!");
            return true;
        }
        Debug.Log($"{item} not found in {category}.");
        return false;
    }

    // Get the appropriate list by category name
    private List<string> GetListByCategory(string category)
    {
        switch (category.ToLower())
        {
            case "flowers":
                return listOfFlowers;
            case "clouds":
                return listOfClouds;
            case "berries":
                return listOfBerries;
            default:
                Debug.Log($"Category '{category}' not recognized.");
                return null;
        }
    }

    // Add a new item to a list
    public void AddToList(string category, string item)
    {
        List<string> targetList = GetListByCategory(category);
        if (targetList != null && !targetList.Contains(item))
        {
            targetList.Add(item);
            Debug.Log($"{item} added to {category}.");
        }
        else
        {
            Debug.Log($"{item} already exists in {category} or category is invalid.");
        }
    }

    // Remove an item from a list
    public void RemoveFromList(string category, string item)
    {
        List<string> targetList = GetListByCategory(category);
        if (targetList != null && targetList.Contains(item))
        {
            targetList.Remove(item);
            Debug.Log($"{item} removed from {category}.");
        }
        else
        {
            Debug.Log($"{item} not found in {category} or category is invalid.");
        }
    }
}
