﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CoinTemplate
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Block : UIBehaviour
    {
        public int index = 0;

        public TMPro.TMP_Text indexTxt;
        public Image border;

        protected override void Awake()
        {
            base.Awake();

            RectTransform rectTransform = GetComponent<RectTransform>();
            GetComponent<BoxCollider2D>().size = rectTransform.rect.size;

            if (this.border != null)
            {
                this.border.raycastTarget = false;

                ChangeBorder(0);
            }

            if (this.indexTxt != null)
                this.indexTxt.raycastTarget = false;
        }

        protected override void Start()
        {
            base.Start();

            if (this.indexTxt != null)
                this.indexTxt.text = index.ToString();
        }

        public void Select()
        {
            if (this.border != null)
                ChangeBorder(1);
        }

        public void Unselect()
        {
            if (this.border != null)
                ChangeBorder(0);
        }

        private void ChangeBorder(float alpha)
        {
            Color color = this.border.color;
            color.a = alpha;
            this.border.color = color;
        }
    }
}
