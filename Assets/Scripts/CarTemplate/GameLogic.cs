using CarTemplate.Containers;
using CarTemplate.Controls;
using Panels;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CarTemplate
{
    public class GameLogic : MonoBehaviour
    {
        public PlatformsCarouselView platforms;
        public Color[] spawnColors = new Color[0];
        public Hearths hearths;
        public ScorePanel score;
        public TMPro.TMP_Text condition;
        public string txtCondition = "";
        public Button checkBtn, nextBtn;
        public DisappearPanel gameOverPanel;

        public static int places, caps, resolves;
        public static Color[] colors;

        private void Awake()
        {
            places = Random.Range(2, 5);
            caps = Random.Range(2, places + 1);
            resolves = caps * Functions.Factorial(places - 1);

            colors = new Color[places];
            int i = 0;
            for (; i < this.spawnColors.Length && i < colors.Length; i++)
                colors[i] = this.spawnColors[i];
            for (; i < places; i++)
                colors[i] = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255));

            if (this.condition != null)
                this.condition.text = this.txtCondition.Replace("${places}", places.ToString()).Replace("${caps}", caps.ToString());

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
            if (this.platforms != null)
            {
                List<Platform> cars = this.platforms.GetCars();
                bool[] marks = new bool[resolves];

                CheckAmount(cars, marks);

                CheckFullness(cars, marks);

                CheckRepeats(cars, marks);

                int rights = marks.Count(el => el == true);
                if (rights == resolves)
                    CorrectAnswer();
                else
                    CheckHearths();

                UpdateScore(rights);
            }
        }

        private void CheckAmount(List<Platform> cars, bool[] marks)
        {
            for (int i = 0; i < marks.Length; i++)
                if (i < cars.Count)
                    marks[i] = cars[i] != null;
                else
                    marks[i] = false;
        }

        private void CheckFullness(List<Platform> cars, bool[] marks)
        {
            for (int i = 0; i < cars.Count; i++)
                if (marks[i])
                    marks[i] = cars[i].slots.Select(el => el.IsEngaged()).Aggregate((f, s) => f && s);
        }

        private void CheckRepeats(List<Platform> cars, bool[] marks)
        {
            for (int i = 0; i < cars.Count; i++)
            {
                if (marks[i])
                {
                    for (int j = i + 1; j < cars.Count; j++)
                    {
                        if (marks[j])
                        {
                            marks[i] = !cars[i].equals(cars[j]);
                        }
                    }
                }
            }
        }

        private void CorrectAnswer()
        {
            if (this.nextBtn != null)
                this.nextBtn.gameObject.SetActive(true);
        }

        private void CheckHearths()
        {
            if (this.hearths != null)
            {
                if (this.hearths.TakeDamage() <= 0)
                {
                    if (this.gameOverPanel != null)
                        this.gameOverPanel.ToNormal();

                    if (this.platforms != null)
                        this.platforms.ToNonActionable();

                    if (this.nextBtn != null)
                        this.nextBtn.gameObject.SetActive(true);
                }
            }
        }

        private void UpdateScore(int rights)
        {
            if (this.score != null)
                this.score.Rewrite(rights);
        }
    }
}
