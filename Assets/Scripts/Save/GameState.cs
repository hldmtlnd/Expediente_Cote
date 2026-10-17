using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GameState
{
    public int dayEventIndex;
    public int nightEventIndex;
    public bool isNight;
    public bool powerCut;

    public float phoneBattery;
    public float cameraBattery;

    public bool hasRevolver;
    public bool hasAdvancedCamera;
    public int revolverAmmo;

    public Vector3 playerPosition;
    public Quaternion playerRotation;

    public List<string> completedEvents = new List<string>();
    public List<string> collectedPhotos = new List<string>();
}
