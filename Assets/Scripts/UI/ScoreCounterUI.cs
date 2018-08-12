using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class ScoreCounterUI : MonoBehaviour {

        TextMeshProUGUI textComponent;

        int score = 0;

        int displayedScore = 0;

        float displayedScoref = 0;

        // Use this for initialization
        void Start() {
            GameManager.Instance.Points.RegisterOnChangedHandler(HandleScoreChanged);
            textComponent = GetComponent<TextMeshProUGUI>();

            UpdateText(GameManager.Instance.Points.Value);
        }

        private void Update()
        {
            if(displayedScore != score)
            {
                float movePercent = 4f * Time.deltaTime;
                if (movePercent > 1)
                {
                    movePercent = 1;
                }
                displayedScoref += (score - displayedScoref) * movePercent;
                int updateScore = (int) Mathf.Round(displayedScoref);
                if (updateScore != displayedScore)
                {
                    UpdateText(updateScore);
                }
                
            }
        }

        void UpdateText(int score)
        {
            displayedScore = score;
            textComponent.text = "" + score;
        }

        void HandleScoreChanged(int newScore)
        {
            score = newScore;
        }
    }
}