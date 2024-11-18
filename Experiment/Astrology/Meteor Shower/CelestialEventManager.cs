// CelestialEventManager.cs

using UnityEngine;
using System.Collections.Generic;

public class CelestialEventManager : MonoBehaviour
{
    public List<CelestialEvent> celestialEvents;
    public List<CelestialEntity> celestialEntities;

    private void Start()
    {
        // Initialize and start events (example with MeteorShowerEvent)
        MeteorShowerEvent meteorShower = gameObject.AddComponent<MeteorShowerEvent>(); 
        meteorShower.eventName = "Meteor Shower";
        meteorShower.eventDuration = 60f; 
        meteorShower.eventMagnitude = 2f;
        meteorShower.eventCurve = AnimationCurve.Linear(0, 0, 1, 1); 
        celestialEvents.Add(meteorShower); 

        // Example of applying the event to a specific entity
        CelestialEntity nightSky = celestialEntities.Find(entity => entity.entityName == "Night Sky");
        if (nightSky != null)
        {
            meteorShower.ApplyEventInfluence(nightSky);
        }
    }
}