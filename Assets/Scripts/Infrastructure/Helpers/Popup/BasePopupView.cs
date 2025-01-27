using System;
using UnityEngine;

namespace Core.PopupSystem.Views
{
    public class BasePopupView : MonoBehaviour
    {
        public event Action OnClose;
        public event Action OnOpen;

        private void OnDestroy()
        {
            OnClose?.Invoke();
        }

        public virtual void SetData(object data)
        {
        }

        public void Open()
        {
            gameObject.SetActive(true);
            OnOpen?.Invoke();
        }

        public void Close()
        {
            Destroy(gameObject);
        }
    }
}