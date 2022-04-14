using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace clicker
{
    public class PlayerInformation : MonoBehaviour
    {
        [SerializeField] private UpgradesValues ressourcesPerClick;
        public UpgradeInfo RessourcePerClickInfo { get { return ressourcesPerClick.GetInfo(); } }

        [SerializeField] private UpgradesValues ressourcesPerTime;
        public UpgradeInfo RessourcePerTimeInfo { get { return ressourcesPerTime.GetInfo(); } }

        /* This function would have been use to generate automatically the desired values 
         * for ressourcesPerClick and ressourcesPerTime if the values wouldn't have been 
         * access by the inspector (still ir for educational purpose only)

        private void InitArray()
        {
            int[] ressourcesPerClick = new int[11];
            for (int i = 0; i < 11; i++)
            {
                ressourcesPerClick[i] = 1 << i; // Time to get funky (bitwise operation)
            }

            int[] ressourcesPerTime = new int[11];
            for (int i = 0; i < 11; i++)
            {
                if (i == 0)
                    ressourcesPerTime[i] = 0;
                else
                    ressourcesPerTime[i] = 1 << (i - 1); // Time to get funky #2
            }
        } */
        [field: SerializeField] public Scorer scorer { get; private set; }

        /// <summary>
        /// Function to trigger when the user click on the tent
        /// </summary>
        public void UserCLickOnTent()
        {
            scorer.IncreaseScore(ressourcesPerClick.GetValue());
        }

        /// <summary>
        /// Collect the score gain by the Gatherer
        /// </summary>
        public void CollectFromGatherer()
        {
            scorer.IncreaseScore(ressourcesPerTime.GetValue());
        }

        /// <summary>
        /// Upgrade an Upgradable Value
        /// </summary>
        /// <param name="id">The ID of the upgrade to be made</param>
        public void MakeUpgrade(int id)
        {
            UpgradesValues upgradeToMake = null;
            switch (id)
            {
                case 0:
                    upgradeToMake = ressourcesPerClick;
                    break;
                case 1:
                    upgradeToMake = ressourcesPerTime;
                    break;
                default: // A error will be written without troubling the user 
                    upgradeToMake = ressourcesPerClick;
                    Debug.LogError("Error id value for Player.MakeUpgrade id = " + id);
                    break;
            }

            if(upgradeToMake.CanBeIncrease)
            {
                if(scorer.Score >= UpgradesValues.COST_UPGRADE)
                {
                    upgradeToMake.IncreaseLvl();
                    scorer.DecreaseScore(UpgradesValues.COST_UPGRADE);
                }
            }
        }
    }

}

