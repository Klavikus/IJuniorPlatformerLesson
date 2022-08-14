using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EntityComponents.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _mainSource;
        [SerializeField] private AudioSource _secondarySource;
        [SerializeField] private List<ClipData> _clipsData;

        public void Play(AudioType clipType, bool isMainSource = false, bool isLooping = false)
        {
            AudioSource currentSource = isMainSource ? _mainSource : _secondarySource;
            currentSource.clip = GetClipData(clipType).Clip;
            currentSource.pitch = GetClipData(clipType).Pitch;
            currentSource.loop = isLooping;
            currentSource.Play();
        }

        public void Stop(bool isMainSource)
        {
            if (isMainSource)
                _mainSource.Stop();
            else
                _secondarySource.Stop();
        }

        private ClipData GetClipData(AudioType clipType) =>
            _clipsData.FirstOrDefault(data => data.Type == clipType);
    }
}