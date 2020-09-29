using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayButtonSound()
    {
        AudioManager.instance.PlaySFX(4);
    }
}
