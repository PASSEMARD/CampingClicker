using TMPro;
using UnityEngine;

namespace clicker
{
    public class Scorer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textbox;
        public int Score { get; set; }

        private void OnEnable()
        {
            _textbox.SetText(Score.ToString());
        }

        /// <summary>
        /// Increase the overal score.
        /// </summary>
        /// <param name="value">The value to increase the score.</param>
        public void IncreaseScore(int value)
        {
            Score += value;
            _textbox.SetText(Score.ToString());
        }

        /// <summary>
        /// Decrease the overal score.
        /// </summary>
        /// <param name="value">The value to decreade the score with.</param>
        public void DecreaseScore(int value)
        {
            IncreaseScore(-value);
        }
    }
}
