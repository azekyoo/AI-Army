using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    [Header("Unit Configuration")]
    public GameObject unitPrefab;
    public int unitCost = 100;
    public Transform spawnPoint;

    [Header("Resources")] 
    private int currentCoins = 200; // Montant initial de pièces


    public Text coinsText;

    void Start()
    {
        UpdateCoinsDisplay();
    }

    void Update()
    {
        TryProduceUnit();
    }

    public void TryProduceUnit()
    {
        if (currentCoins >= unitCost)
        {
            // Déduire le coût
            currentCoins -= unitCost;
            UpdateCoinsDisplay();
            SpawnUnit();
        }
        else
        {
            Debug.Log("Pas assez de pièces ou production en cours!");
        }
    }

    private void SpawnUnit()
    {
        if (spawnPoint != null && unitPrefab != null)
        {
            // Spawn l'unité au point de spawn
            Instantiate(unitPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    public void AddCoins(int amount)
    {
        currentCoins += amount;
        UpdateCoinsDisplay();
    }

    private void UpdateCoinsDisplay()
    {
        if (coinsText != null)
        {
            coinsText.text = $"Pièces: {currentCoins}";
        }
    }
}