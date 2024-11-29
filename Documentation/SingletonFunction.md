# SingletonFunction

## Overview
The `SingletonFunction` class implements a membership function for fuzzy singletons, which are fuzzy sets characterized by a single point where the membership value is 1. This class is part of the UnityFuzzy namespace and is used to represent crisp (classical) numbers within a fuzzy domain. It allows developers to determine the degree of membership of a specific value to the fuzzy singleton, making it useful for fuzzy logic applications.

## Variables
- **support**: A private float variable that represents the unique point where the membership value is 1. This is the only value that the membership function recognizes as having full membership.

## Functions
- **SingletonFunction(float support)**: Constructor that initializes a new instance of the `SingletonFunction` class. It takes a float parameter `support`, which is the only x value where the membership function returns 1.

- **LeftLimit**: A public property that returns the leftmost x value of the membership function, which is the same as the `support` value.

- **RightLimit**: A public property that returns the rightmost x value of the membership function, which is also the same as the `support` value.

- **GetMembership(float x)**: This method calculates the membership degree of a given value `x` to the singleton function. It returns 1 if `x` matches the `support`, and 0 otherwise. This effectively determines whether a given value is fully represented by the fuzzy singleton or not.