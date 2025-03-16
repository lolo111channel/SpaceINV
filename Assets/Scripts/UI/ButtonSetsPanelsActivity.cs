using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;


namespace SpaceInv
{
    public class ButtonSetsPanelsActivity : MonoBehaviour
    {
        [SerializeField] private GameObject[] _panelsThatTheyWillActive;
        [SerializeField] private GameObject[] _panelsThatTheyWontActive;

        private Button _btn;

        private void OnEnable()
        {
            _btn = GetComponent<Button>();

            if (_btn != null)
            {
                _btn.onClick.AddListener(SetPanelActive);
            }
        }

        private void SetPanelActive()
        {
            foreach (var panel in _panelsThatTheyWillActive)
            {
                panel.SetActive(true);
            }

            foreach (var panel in  _panelsThatTheyWontActive)
            {
                panel.SetActive(false);
            }
        }
    }

}

