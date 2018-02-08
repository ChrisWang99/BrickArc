using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public GameObject[] Audios;
    public float[] DeltaTimes;

    public void InstantiateAudio(int index)
    {
        GameObject audio = Instantiate(Audios[index], transform);
        Destroy(audio, DeltaTimes[index]);
    }
}
