using UnityEngine;
using UnityEngine.InputSystem;

namespace GameInput
{
    public partial class Controls
    {
        private static Controls _instance;

        public static Controls Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Controls();
                    _instance.Enable();
                }

                return _instance;
            }
            private set => _instance = value;
        }
    }
}

