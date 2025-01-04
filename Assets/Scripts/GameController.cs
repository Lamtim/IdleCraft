using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : GenericSingleton<GameController>
{
    [SerializeField] private TMP_Text copperCountText;
    [SerializeField] private TMP_Text goldCountText;
    [SerializeField] private Button clickButton;

    private int copperPerClick = 1;

    public float copperCount;
    private int goldCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpgradeManager.Instance.InstantiateUpgradeManager();
        clickButton.onClick.AddListener(GenerateResources);
        //InvokeRepeating(nameof(GeneratePassiveIncome), 1f, 1f);
    }

    private void Update()
    {
        copperCountText.text = $"Copper: {copperCount}";
        goldCountText.text = $"Gold: {goldCount}";
    }

    private void GenerateResources()
    {
        copperCount += CopperPerClick();
        UpgradeManager.Instance.UpdateUpradeUI();
    }

    private int CopperPerClick()
    {
        return 1 + UpgradeManager.Instance.clickUpgradeLevel;
    }
}
