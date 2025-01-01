using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateUI();
        clickButton.onClick.AddListener(GenerateResources);
        upgradeButton.onClick.AddListener(UpgradeClick);
        craftItemButton.onClick.AddListener(CraftItems);
        unlockPassiveIncome.onClick.AddListener(UnlockPassiveIncome);
        InvokeRepeating(nameof(GeneratePassiveIncome), 1f, 1f);
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
        
        copperCountText.text = $"Copper: {copperCount}";
        goldCountText.text = $"Gold: {goldCount}";
        upgradeButton.GetComponentInChildren<TMP_Text>().text = $"Upgrade Click ({upgradeClickCost} copper)";
        craftItemButton.GetComponentInChildren<TMP_Text>().text = $"Craft Item ({craftCost} copper)";
        unlockPassiveIncome.GetComponentInChildren<TMP_Text>().text = $"Passive Income ({passiveIncomeCost} copper)";
        upgradeButton.interactable = copperCount >= upgradeClickCost;
        craftItemButton.interactable = copperCount >= craftCost;
        unlockPassiveIncome.interactable = copperCount >= passiveIncomeCost;
    }
}
