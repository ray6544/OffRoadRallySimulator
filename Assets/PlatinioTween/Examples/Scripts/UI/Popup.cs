using UnityEngine;
using Platinio.TweenEngine;
using Platinio.UI;

namespace Platinio
{
    public class Popup : MonoBehaviour
    {
        [SerializeField] private Vector2 startPosition = Vector2.zero;
        [SerializeField] private Vector2 desirePosition = Vector2.zero;
        [SerializeField] private RectTransform canvas = null;      
        [SerializeField] private float time = 0.5f;
        [SerializeField] private Ease enterEase = Ease.EaseInOutExpo;
        [SerializeField] private Ease exitEase = Ease.EaseInOutExpo;

        public bool isVisible    = false;
        public bool isBusy       = false;
        public bool IsStart;
        private RectTransform thisRect = null;

        private void Start()
        {
            thisRect = GetComponent<RectTransform>();   
            
            thisRect.anchoredPosition = thisRect.FromAbsolutePositionToAnchoredPosition(startPosition , canvas);
            if (IsStart == true)
            {
                Toggle();
            }
        }

        public void Show()
        {
            thisRect.MoveUI( desirePosition, canvas, time).SetEase(enterEase).SetOnComplete(delegate
            {
                isBusy = false;
                isVisible = true;
            });
            
        }

        public void Hide()
        {
            thisRect.MoveUI( startPosition, canvas, time).SetEase(exitEase).SetOnComplete(delegate
            {
                isBusy = false;
                isVisible = false;
               
                
            });
        }

        public void Toggle()
        {
            if (isBusy)
                return;

            isBusy = true;

            if (isVisible)
                Hide();
            else
                Show();
        }
    }

}

