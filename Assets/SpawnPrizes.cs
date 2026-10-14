using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary,
    Mythical
}

[System.Serializable]
public class PrizeData
{
    public GameObject prefab;
    public Rarity rarity;
}

public class SpawnPrizes : MonoBehaviour
{
    [Header("Spawner Settings")]
    public List<PrizeData> prizes = new List<PrizeData>();
    public Transform spawnPoint;
    public int numberOfPrizesToSpawn = 1;

    [Header("Spawn Cooldowns (seconds)")]
    public float epicCooldown = 300f;
    public float legendaryCooldown = 600f;
    public float mythicalCooldown = 900f;

    private float epicTimer;
    private float legendaryTimer;
    private float mythicalTimer;

    [Header("UI (Cooldown Display)")]
    public TextMeshProUGUI epicText;
    public TextMeshProUGUI legendaryText;
    public TextMeshProUGUI mythicalText;

    private void Start()
    {
        // Start cooldowns immediately
        epicTimer = epicCooldown;
        legendaryTimer = legendaryCooldown;
        mythicalTimer = mythicalCooldown;

        Spawnprizes();
    }

    private void Update()
    {
        epicTimer -= Time.deltaTime;
        legendaryTimer -= Time.deltaTime;
        mythicalTimer -= Time.deltaTime;

        // Check timers and spawn when ready
        if (epicTimer <= 0)
        {
            SpawnByRarity(Rarity.Epic);
            epicTimer = epicCooldown;
        }

        if (legendaryTimer <= 0)
        {
            SpawnByRarity(Rarity.Legendary);
            legendaryTimer = legendaryCooldown;
        }

        if (mythicalTimer <= 0)
        {
            SpawnByRarity(Rarity.Mythical);
            mythicalTimer = mythicalCooldown;
        }

        UpdateUI();
    }
    private void SpawnByRarity(Rarity rarity)
    {
        List<PrizeData> filtered = prizes.FindAll(p => p.rarity == rarity);

        if (filtered.Count == 0)
        {
            Debug.LogWarning("No prize found for rarity: " + rarity);
            return;
        }

        // Pick random within that rarity
        PrizeData chosen = filtered[Random.Range(0, filtered.Count)];

        Instantiate(chosen.prefab, spawnPoint.position, Quaternion.identity);
    }
    private void UpdateUI()
    {
        epicText.text = FormatTimer("Epic", epicTimer);
        legendaryText.text = FormatTimer("Legendary", legendaryTimer);
        mythicalText.text = FormatTimer("Mythical", mythicalTimer);
    }
    private string FormatTimer(string name, float timer)
    {
        if (timer <= 0)
            return name + ": 00:00";

        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);

        return name + ": " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }
    public void Spawnprizes()
    {
        for (int i = 0; i < numberOfPrizesToSpawn; i++)
        {
            GameObject selectedPrize = GetRandomPrize();
            Instantiate(selectedPrize, spawnPoint.position, Quaternion.identity);
        }
    }

    private GameObject GetRandomPrize()
    {
        float totalWeight = 0f;

        // Calculate valid weights ONLY if timers allow
        foreach (var prize in prizes)
        {
            if (CanSpawn(prize.rarity))
                totalWeight += GetWeight(prize.rarity);
        }

        float randomValue = Random.Range(0f, totalWeight);
        float current = 0f;

        foreach (var prize in prizes)
        {
            if (!CanSpawn(prize.rarity)) continue;

            current += GetWeight(prize.rarity);
            if (randomValue <= current)
            {
                ApplyCooldown(prize.rarity);
                return prize.prefab;
            }
        }

        return prizes[0].prefab;
    }

    private bool CanSpawn(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Epic:
                return epicTimer <= 0;
            case Rarity.Legendary:
                return legendaryTimer <= 0;
            case Rarity.Mythical:
                return mythicalTimer <= 0;
        }
        return true;
    }

    private void ApplyCooldown(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Epic:
                epicTimer = epicCooldown;
                break;
            case Rarity.Legendary:
                legendaryTimer = legendaryCooldown;
                break;
            case Rarity.Mythical:
                mythicalTimer = mythicalCooldown;
                break;
        }
    }

    private float GetWeight(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common: return 60f;
            case Rarity.Uncommon: return 10f;
            case Rarity.Rare: return 5f;
            case Rarity.Epic: return 3f;
            case Rarity.Legendary: return 1f;
            case Rarity.Mythical: return 0.5f;
        }
        return 1f;
    }
}