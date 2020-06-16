using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Panels
{
    public class ImageGalleryPanel : UIBehaviour
    {
        public SVGImage vectorField;
        public Image rasterField;
        public Sprite[] imgs = new Sprite[0];

        public Button next, prev;
        public TMPro.TMP_Text currentTxt;

        private int current = -1;

        protected override void Awake()
        {
            base.Awake();

            if (this.imgs.Length > 0)
            {
                this.current = 0;

                if (this.vectorField != null)
                    this.vectorField.preserveAspect = true;

                if (this.rasterField != null)
                    this.rasterField.preserveAspect = true;

                UpdateSprite();
            }

            if (this.next != null)
                this.next.onClick.AddListener(() => Next());

            if (this.prev != null)
                this.prev.onClick.AddListener(() => Prev());
        }

        public void Next()
        {
            if (this.imgs.Length > 0)
            {
                this.current = (this.current + 1) % this.imgs.Length;

                UpdateSprite();
            }
        }

        public void Prev()
        {
            if (this.imgs.Length > 0)
            {
                this.current--;
                if (this.current < 0) this.current = this.imgs.Length - 1;

                UpdateSprite();
            }
        }

        public void Show(int index)
        {
            if (index >= 0 && index < this.imgs.Length)
            {
                this.current = index;

                UpdateSprite();
            }
        }

        private void UpdateSprite()
        {
            if (this.vectorField != null)
                this.vectorField.sprite = this.imgs[this.current];

            if (this.rasterField != null)
                this.rasterField.sprite = this.imgs[this.current];

            if (this.currentTxt != null)
                this.currentTxt.text = (this.current + 1).ToString() + "/" + this.imgs.Length.ToString();
        }
    }
}
