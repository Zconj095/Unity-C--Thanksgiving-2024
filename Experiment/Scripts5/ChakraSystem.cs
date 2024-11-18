using UnityEngine;

public class ChakraSystem : MonoBehaviour
{
    public GameObject[] chakras; // 0 - Root, 1 - Sacral, ..., 6 - Crown
    public float[] chakraEnergies;
    public float maxEnergy = 100f;
    public float energyFlowSpeed = 5f;
    public float healingRate = 5f;

    private bool heartChakraActive = false;
    private PlayerHealth playerHealth; // Assume you have a PlayerHealth script

    public AudioClip chakraActivationSound;
    public AudioClip chakraDeactivationSound;
    private AudioSource audioSource;

    void Start()
    {
        foreach (GameObject chakra in chakras)
        {
            chakra.SetActive(true);
        }

        chakraEnergies = new float[chakras.Length];
        for (int i = 0; i < chakraEnergies.Length; i++)
        {
            chakraEnergies[i] = maxEnergy / 2;
        }

        playerHealth = GetComponent<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (heartChakraActive && playerHealth != null)
        {
            playerHealth.Heal(healingRate * Time.deltaTime);
        }

        // Example: Activate or deactivate chakras based on player input
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToggleChakra(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ActivateHeartChakra(!heartChakraActive);
        }

        BalanceChakras();
    }

    public void ToggleChakra(int index)
    {
        if (index >= 0 && index < chakras.Length)
        {
            bool isActive = chakras[index].activeSelf;
            chakras[index].SetActive(!isActive);

            if (audioSource != null)
            {
                if (chakras[index].activeSelf)
                {
                    audioSource.PlayOneShot(chakraActivationSound);
                }
                else
                {
                    audioSource.PlayOneShot(chakraDeactivationSound);
                }
            }
        }
    }

    public void ActivateHeartChakra(bool activate)
    {
        heartChakraActive = activate;
        chakras[3].SetActive(activate);

        if (audioSource != null)
        {
            if (activate)
            {
                audioSource.PlayOneShot(chakraActivationSound);
            }
            else
            {
                audioSource.PlayOneShot(chakraDeactivationSound);
            }
        }
    }

    void BalanceChakras()
    {
        for (int i = 0; i < chakraEnergies.Length - 1; i++)
        {
            float energyDifference = chakraEnergies[i] - chakraEnergies[i + 1];
            float energyFlow = energyDifference * Time.deltaTime * energyFlowSpeed;

            chakraEnergies[i] -= energyFlow;
            chakraEnergies[i + 1] += energyFlow;
        }

        for (int i = 0; i < chakraEnergies.Length; i++)
        {
            chakraEnergies[i] = Mathf.Clamp(chakraEnergies[i], 0, maxEnergy);
        }
    }

    public void AddEnergyToChakra(int index, float amount)
    {
        if (index >= 0 && index < chakras.Length)
        {
            chakraEnergies[index] += amount;
            chakraEnergies[index] = Mathf.Clamp(chakraEnergies[index], 0, maxEnergy);
        }
    }

    public float GetChakraEnergy(int index)
    {
        if (index >= 0 && index < chakras.Length)
        {
            return chakraEnergies[index];
        }
        return 0;
    }

    public void AlignChakras()
    {
        float averageEnergy = 0f;
        foreach (float energy in chakraEnergies)
        {
            averageEnergy += energy;
        }
        averageEnergy /= chakraEnergies.Length;

        for (int i = 0; i < chakraEnergies.Length; i++)
        {
            chakraEnergies[i] = averageEnergy;
        }
    }
}
