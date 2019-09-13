using System;
using UnityEngine;

namespace Breakin.Sound
{
    public interface ISFXController
    {
        /// <summary>
        /// Plays a sound at the world origin
        /// </summary>
        /// <param name="index"></param>
        void PlaySound(int index);

        /// <summary>
        /// Plays a sound at the specified position
        /// </summary>
        /// <param name="index"></param>
        /// <param name="source"></param>
        void PlaySound(int index, Vector3 source);

        /// <summary>
        /// Invoked whenever a sound is played
        /// </summary>
        event Action SoundPlayed;
    }
}