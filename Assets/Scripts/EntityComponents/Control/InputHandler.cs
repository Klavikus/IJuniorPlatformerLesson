using System;
using UnityEngine;

namespace EntityComponents.Control
{
    public abstract class InputHandler : MonoBehaviour
    {
        public Action<InputData> InputUpdated;
    }
}