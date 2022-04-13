using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace clicker
{
    public struct UpgradeInfo
    {
        public int value;
        public int actualLvl;

       public UpgradeInfo(int value, int actualLvl)
       {
            this.value = value;
            this.actualLvl = actualLvl;
       }
    }

    [Serializable]
    public class UpgradesValues
    {


        public const int COST_UPGRADE = 20;

        [SerializeField] private int[] values;
        public int PlayerLvl { get; protected set; }

        /// <summary>
        /// Return if the lvl can be increase futhermore or not
        /// </summary>
        public bool CanBeIncrease
        {
            get { return PlayerLvl < values.Length - 1; }
        }

        public void IncreaseLvl()
        {
            if(CanBeIncrease)
                PlayerLvl++;
        }

        public int GetValue()
        {
            return values[PlayerLvl];
        }

        public UpgradeInfo GetInfo()
        {
            return new UpgradeInfo(GetValue(), PlayerLvl);
        }
    }
}

