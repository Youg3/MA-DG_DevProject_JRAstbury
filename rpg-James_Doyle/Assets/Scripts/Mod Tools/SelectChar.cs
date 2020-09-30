using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectChar : MonoBehaviour
{
    public static SelectChar instance;

    public int selectChar;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

}
