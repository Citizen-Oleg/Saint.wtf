using UnityEngine;
using Zenject;

namespace PlayerComponent
{
    public class Player : MonoBehaviour
    {
        public Inventory Inventory { get; private set; }

        [Inject]
        public void Constructor(Inventory inventory)
        {
            Inventory = inventory;
        }
    }
}