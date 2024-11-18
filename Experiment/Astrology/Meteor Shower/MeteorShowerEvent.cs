// MeteorShowerEvent.cs

using UnityEngine;
using Enviro; // Make sure this namespace is correct

public class MeteorShowerEvent : CelestialEvent
{
    public override void ApplyEventInfluence(CelestialEntity entity)
    {
        base.ApplyEventInfluence(entity); // Call the base class method

        // Add your specific Meteor Shower logic here
        UnityEngine.Debug.Log("Meteor shower affecting " + entity.entityName); 
    }
}