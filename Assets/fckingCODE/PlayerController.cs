using System.Collections.Generic;
using UnityEngine;

namespace fckingCODE
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerContainer PlayerContainer;

        private List<TowerController> TowerControllers;

        public List<TowerController> TowerControllers1
        {
            get => TowerControllers;
            set => TowerControllers = value;
        }
    }
}
