using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class ScoreCounterUI : MonoBehaviour {

        TextMeshProUGUI textComponent;

        int score = 0;

        int displayedScore = 0;

        float displayedScoref = 0;

        float popUpAnimation = 0;

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

            if (popUpAnimation > 0)
            {
                popUpAnimation -= Time.deltaTime * 1.6F;
                if (popUpAnimation < 0)
                    popUpAnimation = 0;

                textComponent.fontSize = 60 + popUpAnimation * 30;
            }
        }

        void UpdateText(int score)
        {
            displayedScore = score;
            textComponent.text = "" + score;
            popUpAnimation = 1;
        }

        void HandleScoreChanged(int newScore)
        {
            score = newScore;
        }
    }
}