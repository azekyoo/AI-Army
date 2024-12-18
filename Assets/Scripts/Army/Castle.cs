using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    [Header("Unit Configuration")]
    public GameObject unitPrefab;
    public int unitCost = 100;
    public Transform spawnPoint;

    [Header("Resources")] 
    private int currentCoins = 150; // Montant initial de pièces

    [SerializeField] TMP_Text m_CoinsText;

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
            //Debug.Log("Pas assez de pièces ou production en cours!");
        }
    }

    private void SpawnUnit()
    {
        if (spawnPoint && unitPrefab)
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
    public void AddCoins50()
    {
        currentCoins = currentCoins + 50;
        Debug.Log("TEST BOUTON! : " + currentCoins);
        UpdateCoinsDisplay();
    }

    private void UpdateCoinsDisplay()
    {
        if (m_CoinsText)
        {
            m_CoinsText.text = $"{currentCoins}";
        }
    }
}