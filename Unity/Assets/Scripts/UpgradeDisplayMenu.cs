using UnityEngine;
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
        [SerializeField] private TMP_Text priceUpgradeClick;

        [SerializeField] private TMP_Text actualValueGatherer;
        [SerializeField] private TMP_Text lvlGatherer;
        [SerializeField] private TMP_Text priceUpgradeGatherer;


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
        }

        private void AddLvl(TMP_Text text, int value)
        {
            text.SetText("lvl." + value.ToString());
        }


    }
}
