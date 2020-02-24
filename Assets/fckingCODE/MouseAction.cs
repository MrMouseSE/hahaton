using System;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

namespace fckingCODE
{
    public class MouseAction : MonoBehaviour
    { 
        public event Action OnMouseDwn;
        public event Action OnMouseUp;
        public event Action OnMouseDrg;
        private bool isPressed;


        private void Awake()
        {
            Instance = this;
        }

        public static MouseAction Instance { get; set; }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnMouseDwn?.Invoke();
                isPressed = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                OnMouseUp?.Invoke();
                isPressed = false;
            }

            if (isPressed)
            {
                OnMouseDrg?.Invoke();
            }
        }
    }
}
