using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wavelist : MonoBehaviour {

    public Dictionary<string,int[]> wave_dict = new Dictionary<string, int[]>();

    // Use this for initialization
    void Awake () {
        wave_dict.Add("wave_0", new int[] { 2, 1, 1, 1, 2, 1 });
        wave_dict.Add("wave_1", new int[] { 3, 4, 2, 2, 2, 1 });
        wave_dict.Add("wave_2", new int[] { 1, 2, 2, 0, 0, 1 });

    }
}
