using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    [SerializeField] private TMP_Text copperCountText;
    [SerializeField] private TMP_Text goldCountText;
    [SerializeField] private Button clickButton;
    [SerializeField] private Transform upgradesContainer;
    [SerializeField] private GameObject upgradePrefab;

    private int copperPerClick = 1;
    private int upgradeClickCost = 10;
    private int craftCost = 20;
    private int goldPerCraft = 10;
    private int passiveIncomeCost = 100;

    private int copperCount;
    private int goldCount;
    private int passiveIncome = 0;
    private List<Upgrade> availableUpgrades;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        availableUpgrades = new List<Upgrade>
        {
            new Upgrade("Upgrade Click", 10, () => UpgradeClick(), 1),
            new Upgrade("Craft Gold", 50, () => CraftItems(), 1),
            new Upgrade("Passive Income", 100, () => UnlockPassiveIncome(), 1)
        };

        clickButton.onClick.AddListener(GenerateResources);
        InvokeRepeating(nameof(GeneratePassiveIncome), 1f, 1f);
        UpdateUI();
    }

    private void GenerateResources(){
        copperCount += copperPerClick;
        UpdateUI();
    }

    void UpgradeClick()
    {
        if (copperCount >= upgradeClickCost)
        {
            copperCount -= upgradeClickCost;
            copperPerClick++;
            UpdateUI();
        }
    }
    
    private void CraftItems(){
        if (copperCount >= craftCost){
            copperCount -= craftCost;
            goldCount += goldPerCraft;
            UpdateUI();
        }
    }
    
    private void UnlockPassiveIncome()
    {
        if (copperCount >= passiveIncomeCost)
        {
            copperCount -= passiveIncomeCost;
            passiveIncome++;
            UpdateUI();
        }
    }

    private void GeneratePassiveIncome()
    {
        copperCount += passiveIncome;
        UpdateUI();
    }

    private void UpdateUI(){
        
        foreach (Transform child in upgradesContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var upgrade in availableUpgrades)
        {
            if (copperCount >= upgrade.Cost / 2)
            {
                var button = Instantiate(upgradePrefab, upgradesContainer);
                button.GetComponentInChildren<TMP_Text>().text = $"{upgrade.Name} ({upgrade.Cost} copper)";
                button.GetComponent<Button>().onClick.AddListener(() => {
                    upgrade.Action.Invoke();
                    UpdateUI();
                });
                //button.GetComponentInChildren<Button>().interactable = copperCount >= upgrade.Cost;
            }
        }

        copperCountText.text = $"Copper: {copperCount}";
        goldCountText.text = $"Gold: {goldCount}";
    }
}
