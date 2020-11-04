using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaded : MonoBehaviour
{

    public string modScene;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Scene Loader Awake");
    }

    void OnEnable()
    {
        Debug.Log("OnEnable Called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

        if (scene.name == modScene)
        {
            Debug.Log("scene name");
            ModMenu.instance.modObjs.SetActive(true);
            //reset house variables in game manager here
            //QuestManager.instance.questMarkersComplete
        }

        //route 1 check
//        if (WorldMod.instance.route1.isOn)
//        {
//            Debug.Log("Route 1 Unlocked");
//        }
//
//        if (!WorldMod.instance.route1.isOn)
//        {
//            Debug.Log("Route 1 Locked");
//        }
        


        //read values from ModMenu?

        //Assign GameObjects to ModMenu?

        //Assign Bosses to ModMenu

        //assign the path exits to the mod menu
//        foreach (var exit in pathExits)
//        {
//            WorldMod.instance.pathExits.ToArray();
//        }
        // check for bool value on route 1


    }
}
