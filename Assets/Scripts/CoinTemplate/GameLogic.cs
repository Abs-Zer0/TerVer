using CoinTemplate.Panels;
using Panels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CoinTemplate
{
    public class GameLogic : MonoBehaviour
    {
        public CoinsPanel coins;
        public BlocksPanel blocks;
        public Hearths hearths;
        public TMPro.TMP_Text condition;
        public string txtCondition = "";
        public TMPro.TMP_Text answerEnum, answerDenom;
        public Button checkBtn, nextBtn;
        public DisappearPanel gameOverPanel, notSelectedPanel;

        public static int throws, reshka, orel;
        private Rational resolve;

        private void Awake()
        {
            throws = Random.Range(1, 5);
            orel = Random.Range(1, throws);
            reshka = throws - orel;
            this.resolve = Functions.Binom(throws, orel) * Rational.half.Pow(orel) * Rational.half.Pow(reshka);

            if (this.condition != null)
                this.condition.text = this.txtCondition.Replace("${throws}", throws.ToString()).Replace("${throwsTimes}", Functions.Times(throws))
                    .Replace("${orel}", orel.ToString()).Replace("${orelTimes}", Functions.Times(orel));

            if (this.answerEnum != null && this.answerDenom != null)
            {
                this.answerEnum.text = "?";
                this.answerDenom.text = "?";
            }

            if (this.checkBtn != null)
                this.checkBtn.onClick.AddListener(() => CheckAction());

            if (this.nextBtn != null)
            {
                this.nextBtn.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
                this.nextBtn.gameObject.SetActive(false);
            }
        }

        private void CheckAction()
        {
            if (this.coins != null && this.blocks != null)
            {
                if (this.coins.selected != null && this.blocks.selected != null)
                {
                    int coinWeight = this.coins.selected.index;
                    int blockWeight = this.blocks.selected.index;
                    Rational answer = new Rational(coinWeight, blockWeight);

                    if (this.resolve == answer)
                        CorrectAnswer();
                    else
                        CheckHearths();
                }
                else if (this.notSelectedPanel != null)
                    this.notSelectedPanel.ToNormal();
            }
        }

        private void CorrectAnswer()
        {
            if (this.nextBtn != null)
                this.nextBtn.gameObject.SetActive(true);

            if (this.answerEnum != null && this.answerDenom != null)
            {
                this.answerEnum.text = this.resolve.Enumerator.ToString();
                this.answerDenom.text = this.resolve.Denomenator.ToString();
            }
        }

        private void CheckHearths()
        {
            if (this.hearths != null)
            {
                if (this.hearths.TakeDamage() <= 0)
                {
                    if (this.gameOverPanel != null)
                        this.gameOverPanel.ToNormal();

                    if (this.coins != null && this.blocks != null)
                    {
                        this.coins.ToNonActionable();
                        this.blocks.ToNonActionable();
                    }

                    if (this.nextBtn != null)
                        this.nextBtn.gameObject.SetActive(true);

                    if (this.answerEnum != null && this.answerDenom != null)
                    {
                        this.answerEnum.text = this.resolve.Enumerator.ToString();
                        this.answerDenom.text = this.resolve.Denomenator.ToString();
                    }
                }
            }
        }
    }
}
