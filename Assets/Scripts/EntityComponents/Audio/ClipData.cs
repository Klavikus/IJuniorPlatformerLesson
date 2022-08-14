using System;
using UnityEngine;

namespace EntityComponents.Audio
{
    [Serializable]
    public class ClipData
    {
        public AudioType Type;
        public AudioClip Clip;
        public float Pitch;
    }
}