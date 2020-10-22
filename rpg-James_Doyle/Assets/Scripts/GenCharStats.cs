using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenCharStats : MonoBehaviour
{
    public string charName;
    public int currentHp;
    public int maxHp;
    public int currentMp;
    public int maxMp;
    public int strength;
    public int defence;
    public int wpnPwr;
    public int armPwr;

    public bool isActive;

}
