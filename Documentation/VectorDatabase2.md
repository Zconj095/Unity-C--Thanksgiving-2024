# VectorDatabase2

## Overview
The `VectorDatabase2` class is a Unity component that manages a collection of vectors stored both in memory and on disk. It allows for adding, retrieving, and checking the existence of vectors using unique identifiers. This class is useful for applications that require persistent storage of vector data, such as game development or machine learning tasks. It integrates file I/O operations to save and load vectors, ensuring that data persists between sessions.

## Variables

- `private Dictionary<int, float[]> vectors`: A dictionary that maps unique integer IDs to their corresponding vector arrays (float[]). This serves as the in-memory storage for the vectors.
  
- `private string databasePath`: A string that holds the path to the directory where vector files are stored on disk. This path is used when saving and loading vectors from files.

## Functions

- `public VectorDatabase2(string path)`: Constructor that initializes a new instance of the `VectorDatabase2` class with the specified path for the database.

- `public void AddVector(int id, float[] vector)`: Adds a new vector to the in-memory dictionary and saves it to disk. It takes an integer ID and a float array as parameters.

- `public float[] GetVector(int id)`: Retrieves a vector associated with the specified ID. It first checks the in-memory dictionary and, if not found, attempts to load it from disk.

- `public Dictionary<int, float[]> GetAllVectors()`: Returns the entire collection of vectors stored in memory as a dictionary.

- `public bool ContainsVector(float[] vector)`: Checks if a specified vector already exists in the in-memory dictionary. It compares the input vector with existing vectors using sequence equality.

- `public int GenerateUniqueId()`: Generates and returns a new unique ID for a vector by determining the maximum existing ID and adding one.

- `private void SaveToDisk(int id, float[] vector)`: Saves a vector to the disk as a binary file. The file is named using the vector's ID and is stored at the specified database path.

- `private float[] LoadFromDisk(int id)`: Loads a vector from the disk using its ID. If the file does not exist, it returns null.

- `private byte[] VectorToBytes(float[] vector)`: Converts a float array (vector) into a byte array for storage. Each float is transformed into bytes using the `BitConverter`.

- `private float[] BytesToVector(byte[] bytes)`: Converts a byte array back into a float array (vector). It reads the bytes in chunks corresponding to the size of a float and reconstructs the vector.