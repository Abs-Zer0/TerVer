using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Containers
{
    public class CarouselView : UIBehaviour
    {
        public GameObject prefab;
        public string objectName = "";
        public bool preserveAspect = true;
        public Button increase, decrease, add, remove;
        public TMPro.TMP_Text currentTxt;
        public RectTransform contentPanel;
        [Min(0)]
        public int spacing = 0;
        [Range(1, 100)]
        public int scrollSpeed = 1;

        public bool IsActionable { get; set; } = true;
        public bool HasChanged { get; private set; } = false;

        private int current = -1;
        private int lastCurrent = -1;
        private Coroutine scroll;
        private List<RectTransform> children;
        private int lastCount = 0;
        private RectTransform rectTransform;

        protected override void Awake()
        {
            base.Awake();

            this.rectTransform = GetComponent<RectTransform>();

            if (this.contentPanel != null)
            {
                if (this.contentPanel.parent != this.rectTransform)
                    this.contentPanel.SetParent(this.rectTransform);

                this.contentPanel.anchorMin = new Vector2(0.5f, 0);
                this.contentPanel.anchorMax = new Vector2(0.5f, 1);
                this.contentPanel.sizeDelta = Vector2.zero;
                this.contentPanel.pivot = new Vector2(0, 0.5f);
                this.contentPanel.localPosition = Vector3.zero;
                this.contentPanel.ForceUpdateRectTransforms();

                if (this.increase != null)
                    this.increase.onClick.AddListener(() => Increase());
                if (this.decrease != null)
                    this.decrease.onClick.AddListener(() => Decrease());
                if (this.add != null)
                    this.add.onClick.AddListener(() => Add());
                if (this.remove != null)
                    this.remove.onClick.AddListener(() => Remove());

                if (this.currentTxt != null)
                    this.currentTxt.text = (this.current + 1).ToString();

                this.children = new List<RectTransform>();
            }
        }

        private void Update()
        {
            if (HasChanged)
            {
                this.lastCurrent = this.current;
                this.lastCount = this.children.Count;
                HasChanged = false;
            }

            if (this.current != this.lastCurrent || this.children.Count != this.lastCount)
                HasChanged = true;
        }

        public void SetPrefab(GameObject pref)
        {
            if (pref != null)
            {
                this.prefab = pref;
                this.objectName = this.prefab.name;
            }
        }

        private void Increase()
        {
            if (this.current < this.children.Count - 1)
            {
                this.lastCurrent = this.current;
                this.current++;

                try { StopCoroutine(this.scroll); }
                catch (Exception) { }
                this.scroll = StartCoroutine(Scroll());
            }
        }

        private void Decrease()
        {
            if (this.current > 0)
            {
                this.lastCurrent = this.current;
                this.current--;

                try { StopCoroutine(this.scroll); }
                catch (Exception) { }
                this.scroll = StartCoroutine(Scroll());
            }
        }

        private void Add()
        {
            Add(this.prefab);
        }

        public void Add(GameObject pref)
        {
            if (IsActionable && pref != null)
            {
                this.lastCount = this.children.Count;
                this.children.Add(Canvas.Instantiate(pref, this.contentPanel).GetComponent<RectTransform>());

                UpdateChildren();
                RenameChildren();

                this.lastCurrent = this.current;
                this.current++;

                try { StopCoroutine(this.scroll); }
                catch (Exception) { }
                this.scroll = StartCoroutine(Scroll());
            }
        }

        private void Remove()
        {
            if (IsActionable && this.children.Count > 0)
            {
                Canvas.DestroyImmediate(this.children[this.current].gameObject);
                this.lastCount = this.children.Count;
                this.children.RemoveAt(this.current);

                UpdateChildren();
                RenameChildren();

                if (this.current >= this.children.Count)
                {
                    this.lastCurrent = this.current;
                    this.current = this.children.Count - 1;
                }

                try { StopCoroutine(this.scroll); }
                catch (Exception) { }
                this.scroll = StartCoroutine(Scroll());
            }
        }

        public List<RectTransform> GetChildren()
        {
            return this.children;
        }

        private void UpdateChildren()
        {
            Vector3 pos = Vector3.zero;
            for (int i = 0; i < this.children.Count; i++)
            {
                float scale = this.contentPanel.rect.height / this.children[i].rect.height;
                this.children[i].sizeDelta = new Vector2(this.children[i].sizeDelta.x, scale * this.children[i].sizeDelta.y);
                if (this.preserveAspect)
                    this.children[i].sizeDelta = new Vector2(scale * this.children[i].sizeDelta.x, this.children[i].sizeDelta.y);
                this.children[i].ForceUpdateRectTransforms();

                pos += new Vector3(this.children[i].rect.width * 0.5f + this.spacing, 0, 0);
                this.children[i].localPosition = pos;
                pos += new Vector3(this.children[i].rect.width * 0.5f + this.spacing, 0, 0);
            }
        }

        private void RenameChildren()
        {
            for (int i = 0; i < this.children.Count; i++)
                this.children[i].gameObject.name = this.objectName + " " + i.ToString();
        }

        private IEnumerator Scroll()
        {
            if (this.currentTxt != null)
                this.currentTxt.text = (this.current + 1).ToString();

            if (this.children.Count > 0)
            {
                Vector3 target = new Vector3(-this.children[this.current].localPosition.x, 0, 0);
                while (Mathf.Abs(this.contentPanel.localPosition.x - target.x) > Constants.tolerance)
                {
                    this.contentPanel.localPosition = Vector3.Lerp(this.contentPanel.localPosition, target, this.scrollSpeed * 0.3f * Time.deltaTime);
                    yield return null;
                }
                this.contentPanel.localPosition = target;
            }
            else
                this.contentPanel.localPosition = Vector3.zero;
        }
    }
}
