# CrystalDatabase

## Overview
The `CrystalDatabase` script is a Unity ScriptableObject that serves as a repository for managing a collection of `CrystalMetadata` objects. It allows for adding, removing, and searching for crystals based on various criteria such as name, tags, and type. This structure is essential for organizing crystal data in a game or application, making it easier to handle and manipulate crystal information within the broader codebase.

## Variables
- `public List<CrystalMetadata> crystals`: This is a list that holds all the `CrystalMetadata` objects in the database. It acts as the primary storage for crystal data.

## Functions
- `public void AddCrystal(CrystalMetadata crystal)`: This function adds a new `CrystalMetadata` object to the `crystals` list if it is not already present. It ensures that there are no duplicate entries in the database.

- `public void RemoveCrystal(CrystalMetadata crystal)`: This function removes a specified `CrystalMetadata` object from the `crystals` list if it exists. It helps in managing the database by allowing the removal of crystals when necessary.

- `public CrystalMetadata FindCrystalByName(string name)`: This function searches for a `CrystalMetadata` object in the `crystals` list by its name. It returns the first matching crystal found or `null` if no match is found.

- `public List<CrystalMetadata> FindCrystalsByTag(string tag)`: This function retrieves a list of `CrystalMetadata` objects that contain a specified tag. It returns all crystals that match the tag criteria.

- `public List<CrystalMetadata> FindCrystalsByType(string type)`: This function searches for and returns a list of `CrystalMetadata` objects that match a specified type. It allows for filtering crystals based on their type designation.