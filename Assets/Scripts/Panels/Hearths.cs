using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Panels
{
    public class Hearths : UIBehaviour
    {
        public Color normal = Color.white, faded = Color.black;
        [Range(0, 100)]
        public int fadeSpeed = 1;
        public Image[] hearths = new Image[0];

        private int current = 0;

        protected override void Awake()
        {
            base.Awake();

            foreach (var hearth in this.hearths)
                hearth.color = this.normal;
            this.current = hearths.Length;
        }

        public int TakeDamage()
        {
            this.current--;

            StartCoroutine(Fade(this.current));

            return this.current;
        }

        private IEnumerator Fade(int index)
        {
            while (Mathf.Abs(this.hearths[index].color.r - this.faded.r) > Constants.tolerance &&
                Mathf.Abs(this.hearths[index].color.g - this.faded.g) > Constants.tolerance &&
                Mathf.Abs(this.hearths[index].color.b - this.faded.b) > Constants.tolerance)
            {
                this.hearths[index].color = Color.Lerp(this.hearths[index].color, this.faded, this.fadeSpeed * 0.3f * Time.deltaTime);
                yield return null;
            }
            this.hearths[index].color = this.faded;
        }
    }
}
