using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioList : MonosingletonTemp<AudioList>
{
    public List<AudioClip> audioClips = new List<AudioClip>();
}
