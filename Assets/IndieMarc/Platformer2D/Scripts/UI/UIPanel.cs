﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IndieMarc.Platformer
{

    public class UIPanel : MonoBehaviour
    {
        public float display_speed = 2f;

        public UnityAction onShow;
        public UnityAction onHide;

        private CanvasGroup canvas_group;
        private bool visible;

        protected virtual void Awake()
        {
            canvas_group = GetComponent<CanvasGroup>();
            canvas_group.alpha = 0f;
            visible = false;
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {

            float add = visible ? display_speed : -display_speed;
            float alpha = Mathf.Clamp01(canvas_group.alpha + add * Time.deltaTime);
            canvas_group.alpha = alpha;

            if (!visible && alpha < 0.01f)
                AfterHide();
        }

        public virtual void Toggle(bool instant = false)
        {
            if (IsVisible())
                Hide(instant);
            else
                Show(instant);
        }

        public virtual void Show(bool instant = false)
        {
            visible = true;
            gameObject.SetActive(true);

            if (instant || display_speed < 0.01f)
                canvas_group.alpha = 1f;

            if (onShow != null)
                onShow.Invoke();
        }

        public virtual void Hide(bool instant = false)
        {
            visible = false;
            if (instant || display_speed < 0.01f)
                canvas_group.alpha = 0f;

            if (onHide != null)
                onHide.Invoke();
        }

        public virtual void AfterHide()
        {
            gameObject.SetActive(false);
        }

        public bool IsVisible()
        {
            return visible;
        }

        public float GetAlpha()
        {
            return canvas_group.alpha;
        }
    }

}