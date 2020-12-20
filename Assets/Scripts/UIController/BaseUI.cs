using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DGFactory
{

    public delegate void OnDismiss();

    public class BaseUI : MonoBehaviour
    {
        protected Button _btnClose;
        protected OnDismiss _onDismiss;


        protected virtual void Awake()
        {
            _btnClose = transform.Find("title/ButtonClose").GetComponent<Button>();

            _btnClose.onClick.AddListener(() =>
            {
                Hide();
                if (this._onDismiss != null)
                {
                    this._onDismiss();
                }
            });
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }

}