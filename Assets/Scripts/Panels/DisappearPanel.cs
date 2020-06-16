using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Panels
{
    public class DisappearPanel : UIBehaviour
    {
        public Vector3 normal = Vector3.one, disappeared = Vector3.one;
        [Range(1, 100)]
        public int speed = 1;
        public bool isSelfClosable = false;

        private SurroundingsDisappearPanel surroundings;
        private Coroutine disappearing;

        protected override void Awake()
        {
            base.Awake();

            this.transform.localScale = this.disappeared;

            if (this.isSelfClosable)
            {
                GameObject surroundPanel = new GameObject("Surroundings");
                surroundPanel.transform.SetParent(this.transform);

                RectTransform surroundRect = surroundPanel.AddComponent<RectTransform>();
                surroundRect.localScale = Vector3.one;
                surroundRect.sizeDelta = new Vector2(Screen.width, Screen.height);
                surroundRect.SetAsFirstSibling();
                surroundRect.ForceUpdateRectTransforms();

                BoxCollider2D surroundCollider = surroundPanel.AddComponent<BoxCollider2D>();
                surroundCollider.size = surroundRect.sizeDelta;

                surroundPanel.AddComponent<Image>().color = new Color(0, 0, 0, 0);

                this.surroundings = surroundPanel.AddComponent<SurroundingsDisappearPanel>();
                this.surroundings.ParentPanel = this;


                GameObject areaPanel = new GameObject("Area");
                areaPanel.transform.SetParent(surroundRect);

                RectTransform areaRect = areaPanel.AddComponent<RectTransform>();
                areaRect.localScale = Vector3.one;
                areaRect.sizeDelta = GetComponent<RectTransform>().rect.size;
                areaRect.ForceUpdateRectTransforms();

                BoxCollider2D areaCollider = areaPanel.AddComponent<BoxCollider2D>();
                areaCollider.size = areaRect.sizeDelta;

                areaPanel.AddComponent<Image>().color = new Color(0, 0, 0, 0);

                areaPanel.AddComponent<AreaDisappearPanel>();
            }
        }

        public bool IsDisappeared()
        {
            return this.transform.localScale == disappeared;
        }

        public void ToDisappeared()
        {
            try { StopCoroutine(this.disappearing); }
            catch (Exception) { }
            this.disappearing = StartCoroutine(Disappear(this.disappeared));
        }

        public void ToNormal()
        {
            try { StopCoroutine(this.disappearing); }
            catch (Exception) { }
            this.disappearing = StartCoroutine(Disappear(this.normal));
        }

        private IEnumerator Disappear(Vector3 state)
        {
            while (Mathf.Abs(transform.localScale.x - state.x) > Constants.tolerance && Mathf.Abs(transform.localScale.y - state.y) > Constants.tolerance)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, state, speed * 0.3f * Time.deltaTime);
                yield return null;
            }
            transform.localScale = state;
        }
    }

    [RequireComponent(typeof(BoxCollider2D), typeof(Image))]
    public class SurroundingsDisappearPanel : UIBehaviour, IPointerClickHandler
    {
        public DisappearPanel ParentPanel { get; set; }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (ParentPanel != null)
                if (!ParentPanel.IsDisappeared())
                    ParentPanel.ToDisappeared();
        }
    }

    [RequireComponent(typeof(BoxCollider2D), typeof(Image))]
    public class AreaDisappearPanel : UIBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {

        }
    }
}
