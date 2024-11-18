using UnityEngine;

public class BorrowedEnergy : MonoBehaviour
{
    public float borrowedEnergyAmount = 0.8f;      // Amount of energy borrowed from an external source
    public float energyRepaymentFactor = 0.7f;     // Repayment factor for the borrowed energy
    public float borrowingDuration = 0.9f;         // Duration of borrowing the energy

    private float totalBorrowedEnergy;

    void Update()
    {
        totalBorrowedEnergy = CalculateBorrowedEnergy();
        Debug.Log("Total Borrowed Energy: " + totalBorrowedEnergy);
    }

    float CalculateBorrowedEnergy()
    {
        float k30 = 1.0f; // Constant
        return k30 * Mathf.Pow(borrowedEnergyAmount, 0.5f) * Mathf.Pow(energyRepaymentFactor, 0.3f) * Mathf.Pow(borrowingDuration, 0.2f);
    }
}
