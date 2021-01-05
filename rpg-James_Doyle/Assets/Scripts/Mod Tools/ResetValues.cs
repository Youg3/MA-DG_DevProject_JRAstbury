using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResetValues : MonoBehaviour
{

    private bool initialStart = false;

    //private GameObject<GenCharStats>[] characterStats;

    public GameObject[] characterPrefabs;

    public GameObject[] initialValueObj;

    private LinkedList<GenCharStats> characterStats;

    void Start()
    {
        if(!initialStart)
        {
            //store all values here
            //enemies ?&players?
            
/*            foreach (var prefab in characterPrefabs)
            {
                initialValueObj.AddComponent<GenCharStats>().charName = prefab.gameObject.GetComponent<GenCharStats>().charName;

                prefab.gameObject.GetComponent<GenCharStats>().charName = initialValueObj.SetValue(charName);

                prefab.gameObject.GetComponent<GenCharStats>().Clone();

                prefab.gameObject = initialValueObj;




                characterStats = new LinkedList<GenCharStats>();
               prefab = characterStats;
               characterPrefabs[prefab] = new LinkedList<GenCharStats>();
            }

            for (int i = 0; i <= characterPrefabs.Length -1; i++)
            {
                characterPrefabs[i] = new GameObject("" + i, typeof(GenCharStats));

                //characterPrefabs[i].gameObject.GetComponent<GenCharStats>().charName;

                var testObj = new object();

                testObj = characterPrefabs[i].gameObject;

                initialValueObj.ToArray();

            }*/

            //PCs

            //World


            initialStart = true;
        }

    }

}
