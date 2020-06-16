using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CarTemplate
{
    namespace Persons
    {
        [RequireComponent(typeof(Image), typeof(CircleCollider2D))]
        public class PersonSlot : UIBehaviour
        {
            public Image smile, cap, border;
            public bool isDriverPlace = false;

            private RectTransform rectTransform;
            private CircleCollider2D collider;
            private PersonSpawner spawner;
            private static Color normalBorder = Color.white, selectedBorder = new Color(100f / 255f, 175f / 255f, 215f / 255f);

            protected override void Awake()
            {
                base.Awake();

                this.rectTransform = GetComponent<RectTransform>();
                this.collider = GetComponent<CircleCollider2D>();
                this.collider.radius = (this.rectTransform.rect.width + this.rectTransform.rect.height) * 0.25f;

                if (this.smile != null)
                {
                    this.smile.raycastTarget = false;

                    ChangeSmile(0);
                }

                if (this.border != null)
                {
                    this.border.raycastTarget = false;

                    this.border.color = normalBorder;
                }

                if (this.cap != null)
                    this.cap.raycastTarget = false;
            }

            public bool IsEngaged()
            {
                return this.spawner != null;
            }

            public void Select()
            {
                if (this.border != null)
                    this.border.color = selectedBorder;
            }

            public void Unselect()
            {
                if (this.border != null)
                    this.border.color = normalBorder;
            }

            public void SetPerson(PersonSpawner person)
            {
                this.spawner = person;
                this.spawner.SetSlot(this);

                if (this.smile != null)
                {
                    this.smile.color = this.spawner.GetColor();
                    ChangeSmile(1);
                }
            }

            public void UnsetPerson()
            {
                if (this.smile != null)
                    ChangeSmile(0);

                this.spawner.RemoveSlot();
                this.spawner = null;
            }

            public bool equals(PersonSlot other)
            {
                return this.smile.color == other.smile.color;
            }

            private void ChangeSmile(float alpha)
            {
                Color color = this.smile.color;
                color.a = alpha;
                this.smile.color = color;

                if (this.cap != null && this.isDriverPlace)
                {
                    color = this.cap.color;
                    color.a = alpha;
                    this.cap.color = color;
                }
            }
        }
    }
}
