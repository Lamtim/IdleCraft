using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : GenericSingleton<UpgradeManager>
{
    [SerializeField] private Transform upgradesContainer;
    [SerializeField] private GameObject upgradePrefab;

    private int upgradeClickBaseCost = 10;
    private float clickUpgradeCostMult = 1.5f;
    public int clickUpgradeLevel = 0;
    private int craftCost = 20;
    private int goldPerCraft = 10;
    private int passiveIncomeCost = 100;

    private int passiveIncome = 0;
    private List<Upgrade> availableUpgrades;


    public void InstantiateUpgradeManager()
    {
        availableUpgrades = new List<Upgrade>
        {
            new Upgrade("Upgrade Click", 10, () => UpgradeClick(), 1),
            //new Upgrade("Craft Gold", 50, () => CraftItems(), 1),
            //new Upgrade("Passive Income", 100, () => UnlockPassiveIncome(), 1)
        };
        UpdateUpradeUI();
    }

    public float ClickUpgradeCost()
    {
        return upgradeClickBaseCost * Mathf.Pow(clickUpgradeCostMult, clickUpgradeLevel);
    }

    public void UpgradeClick()
    {
        if (GameController.Instance.copperCount >= ClickUpgradeCost())
        {
            GameController.Instance.copperCount -= ClickUpgradeCost();
            clickUpgradeLevel++;
            UpdateUpradeUI();
        }
    }

    /*private void CraftItems()
    {
        if (copperCount >= craftCost)
        {
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
    }*/

    public void UpdateUpradeUI()
    {

        foreach (Transform child in upgradesContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var upgrade in availableUpgrades)
        {
            if (GameController.Instance.copperCount >= upgrade.Cost / 2)
            {
                var button = Instantiate(upgradePrefab, upgradesContainer);
                button.GetComponentInChildren<TMP_Text>().text = $"{upgrade.Name} ({ClickUpgradeCost()} copper)";
                button.GetComponent<Button>().onClick.AddListener(() =>
                {
                    upgrade.Action.Invoke();
                    UpdateUpradeUI();
                });
                //button.GetComponentInChildren<Button>().interactable = copperCount >= upgrade.Cost;
            }
        }
    }
}
