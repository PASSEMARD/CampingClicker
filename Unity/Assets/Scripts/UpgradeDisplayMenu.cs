using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace clicker
{
    public class UpgradeDisplayMenu : MonoBehaviour
    {
        /// <summary>
        /// Get the player information
        /// </summary>
        [field: SerializeField] public PlayerInformation player { get; private set; }

        [SerializeField] private TMP_Text actualValueClick;
        [SerializeField] private TMP_Text lvlClick;
        [SerializeField] private Text priceUpgradeClick;

        [SerializeField] private TMP_Text actualValueGatherer;
        [SerializeField] private TMP_Text lvlGatherer;
        [SerializeField] private Text priceUpgradeGatherer;

        [SerializeField] private Text pricePine1;
        [SerializeField] private Text pricePine2;
        [SerializeField] private Text pricePine3;


        private void OnEnable()
        {
            UpdateText();
        }

        public void UpdateText()
        {
            UpgradeInfo clickUpgrade = player.RessourcePerClickInfo;
            actualValueClick.SetText(clickUpgrade.value.ToString() + " per click");
            AddLvl(lvlClick, clickUpgrade.actualLvl);

            UpgradeInfo gathererUpgrade = player.RessourcePerTimeInfo;

            actualValueGatherer.SetText(gathererUpgrade.value.ToString() + " every " + Gatherer.GetTimeToPassedBeforeCollect.ToString("G")) ;
            AddLvl(lvlGatherer, clickUpgrade.actualLvl);

            // If we need to change the price between upgrades, now we can easily.
            priceUpgradeClick.text = "Upgrade for " + UpgradesValues.COST_UPGRADE.ToString();
            priceUpgradeGatherer.text = "Upgrade for " + UpgradesValues.COST_UPGRADE.ToString();
            pricePine1.text = "Buy for " + UpgradesValues.COST_UPGRADE.ToString();
            pricePine2.text = "Buy for " + UpgradesValues.COST_UPGRADE.ToString();
            pricePine3.text = "Buy for " + UpgradesValues.COST_UPGRADE.ToString();
        }

        private void AddLvl(TMP_Text text, int value)
        {
            text.SetText("lvl." + value.ToString());
        }


    }
}
