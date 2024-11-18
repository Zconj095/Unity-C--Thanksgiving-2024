using System;
using UnityEngine;

public class CharacterAttributes : MonoBehaviour
{
    public event Action<float> OnAgilityChanged;
    public event Action<float> OnDexterityChanged;
    public event Action<float> OnMobilityChanged;
    public event Action<float> OnVigilanceChanged;
    public event Action<float> OnStaminaChanged;
    public event Action<float> OnWeightChanged;

    private float agility;
    public float Agility
    {
        get => agility;
        set
        {
            agility = value;
            OnAgilityChanged?.Invoke(agility);
        }
    }

    private float dexterity;
    public float Dexterity
    {
        get => dexterity;
        set
        {
            dexterity = value;
            OnDexterityChanged?.Invoke(dexterity);
        }
    }

    private float mobility;
    public float Mobility
    {
        get => mobility;
        set
        {
            mobility = value;
            OnMobilityChanged?.Invoke(mobility);
        }
    }

    private float vigilance;
    public float Vigilance
    {
        get => vigilance;
        set
        {
            vigilance = value;
            OnVigilanceChanged?.Invoke(vigilance);
        }
    }

    private float stamina;
    public float Stamina
    {
        get => stamina;
        set
        {
            stamina = value;
            OnStaminaChanged?.Invoke(stamina);
        }
    }

    private float weight;
    public float Weight
    {
        get => weight;
        set
        {
            weight = value;
            OnWeightChanged?.Invoke(weight);
        }
    }
}
