using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        string[] deathMessages = {"Death is but just another state of existence!",
                                    "Tis but a flesh wound!",
                                    "It's only a scratch, I'm sure you can walk it off...",
                                    "Oh stop being a baby, it's not that bad",
                                    "Not much of a surprise if I'm honest",
                                    "You are supposed to AVOID the squares!",
                                    "It seems you were not the chosen one",
                                    "Well that's just a shame :(",
                                    "The blocks only want a hug",
                                    "It seems red is your favorite color",
                                    "You tried to walk into the light but smacked your face against a lamp",
                                    "You have a lot in common with muffins...",
                                    "I'm not sure how you died, but I'm sure it was hilarious",
                                    "But alas you have fallen, you fought well soldier",
                                    "Sadly, there is no difficulty setting",
                                    "Those blocks had families you know",
                                    "Hint: You may want to read the controls",
                                    "What does this button do again?",
                                    "Looks like you are getting used to this",
                                    "Sarcastic message goes here"};

        public BackgroundUI backgroundUI;
        public GameObject menuBits;
        public GameObject gameOverBits;
        public Slider comboSlider;
        public TextMeshProUGUI highScoreCounter;
        public Text sarcasticMessage;

        void Awake()
        {
            Instance = this;
        }

        public void ShowMenuBits()
        {
            menuBits.SetActive(true);
        }

        public void HideMenuBits()
        {
            menuBits.SetActive(false);
        }

        public void ShowGameOverBits()
        {
            gameOverBits.SetActive(true);
            sarcasticMessage.text = deathMessages[Mathf.RoundToInt(Random.value * (deathMessages.Length - 1))];
            highScoreCounter.text = "" + GameManager.Instance.HighScore;
        }

        public void HideGameOverBits()
        {
            gameOverBits.SetActive(false);
        }
    }
}