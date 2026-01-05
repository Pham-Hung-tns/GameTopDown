using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundLibrary", menuName = "Audio/Sound Library")]
public class SoundLibrary : ScriptableObject
{
    public List<Sound> musics = new List<Sound>();
    public List<Sound> sfxs = new List<Sound>();

    [Header("Startup")]
    [Tooltip("Name of the music to autoplay on start (must match a music entry).")]
    public string startupMusicName;
}
