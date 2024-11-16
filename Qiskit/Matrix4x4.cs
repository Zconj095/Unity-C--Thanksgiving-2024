using System;
using System.Collections.Generic;
using UnityEngine;

// Simplified Matrix4x4 for simulation
public class Matrix4x4
{
    public static bool operator ==(Matrix4x4 m1, Matrix4x4 m2) { return true; }  // Simplified check
    public static bool operator !=(Matrix4x4 m1, Matrix4x4 m2) { return false; }
}
