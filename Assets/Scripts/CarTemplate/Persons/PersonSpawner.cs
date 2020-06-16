using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CarTemplate
{
    namespace Persons
    {
        [RequireComponent(typeof(Image), typeof(BoxCollider2D))]
        public class PersonSpawner : UIBehaviour
        {
            public Image smile, cap, border;
            public bool isCap = false;
            public float fogging = 1;

            private RectTransform rectTransform;
            private BoxCollider2D collider;
            private PersonSlot slot;

            protected override void Awake()
            {
                base.Awake();

                this.rectTransform = GetComponent<RectTransform>();
                this.collider = GetComponent<BoxCollider2D>();
                this.collider.size = this.rectTransform.rect.size;

                if (this.smile != null)
                {
                    this.smile.raycastTarget = false;

                    ChangeSmile(1);
                }

                if (this.border != null)
                {
                    this.border.raycastTarget = false;

                    ChangeBorder(0);
                }

                if (this.cap != null)
                    this.cap.raycastTarget = false;
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

            public void SetSlot(PersonSlot slot)
            {
                this.slot = slot;

                if (this.smile != null)
                    ChangeSmile(this.fogging);
            }

            public void RemoveSlot()
            {
                this.slot = null;

                if (this.smile != null)
                    ChangeSmile(1);
            }

            public PersonSlot GetSlot()
            {
                return this.slot;
            }

            public bool IsEngaged()
            {
                return this.slot != null;
            }

            public void SetColor(Color color)
            {
                if (this.smile != null)
                {
                    Color newColor = new Color(color.r, color.g, color.b, this.smile.color.a);
                    this.smile.color = newColor;
                }
            }

            public Color GetColor()
            {
                if (this.smile == null)
                    return Color.white;

                return new Color(this.smile.color.r, this.smile.color.g, this.smile.color.b, 1);
            }

            private void ChangeSmile(float alpha)
            {
                Color color = this.smile.color;
                color.a = alpha;
                this.smile.color = color;

                if (this.cap != null && this.isCap)
                {
                    color = this.cap.color;
                    color.a = alpha;
                    this.cap.color = color;
                }
            }

            private void ChangeBorder(float alpha)
            {
                Color color = this.border.color;
                color.a = alpha;
                this.border.color = color;
            }
        }
    }
}
