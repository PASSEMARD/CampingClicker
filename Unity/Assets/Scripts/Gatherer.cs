using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace clicker
{
    public class Gatherer : MonoBehaviour
    {
        private float timePassed;
        public const float TIME_TO_PASSED_BEFORE_COLLECT = 5f;
        [field: SerializeField] public PlayerInformation player { get; private set; }

        /// <summary>
        /// Init
        /// </summary>
        void Start()
        {
            timePassed = 0f;
        }

        // Update the timer
        void Update()
        {
            timePassed += Time.deltaTime;

            // If the timer has passed, collect 
            if(timePassed > TIME_TO_PASSED_BEFORE_COLLECT)
            {
                Collect();

                // Reset time
                timePassed = 0f;
            }
        }

        /// <summary>
        /// Add score to the user depending on the level of the gatherer
        /// </summary>
        private void Collect()
        {
            player.CollectFromGatherer();
        }
    }
}
