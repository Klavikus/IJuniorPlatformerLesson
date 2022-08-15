using System;
using UnityEngine;

namespace EntityComponents.Audio
{
    public enum AudioType
    {
        Move,
        Jump,
        Landing,
        Dashing,
        MainAttack,
        SecondaryAttack,
        Dying,
        ItemPickup,
    }

    [Serializable]
    public class ClipData
    {
        public AudioType Type;
        public AudioClip Clip;
        public float Pitch;
    }
}