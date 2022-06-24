using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public AudioManager SFXManager;

    [HideInInspector]
    public int TotalEggsActivated = 0;

    [HideInInspector]
    public int LastItemsActivated = 0;

    [HideInInspector]
    public bool PlayersHaveMet = false;

    private bool EggsActivated = false;
    private bool LastActivation = false;

    private bool GameWon = false;

    // Update is called once per frame
    void Update()
    {
        if (PlayersHaveMet)
        {
            GameWon = true;

            // Do Stuff Here for Game Win Screen Etc
        }

        if (!EggsActivated && TotalEggsActivated == 2)
        {
            // Find GameObjects with ObjectNameTag
            GameObject[] ObjectNameTag = GameObject.FindGameObjectsWithTag("EggPlatform");

            // Lowers all Objects with Matching ObjectNameTag
            foreach (GameObject targetObject in ObjectNameTag)
            {
                targetObject.transform.position = new Vector3(targetObject.transform.position.x,
                                                              targetObject.transform.position.y - 1,
                                                              targetObject.transform.position.z);

                SFXManager.PlaySFX("ClearRubble");

                EggsActivated = true;
            }
        }

        if (!LastActivation && LastItemsActivated == 2)
        {
            GameObject LeftBarrier = GameObject.FindWithTag("L_Barrier");

            LeftBarrier.transform.rotation = new Quaternion(0, 0, 0, LeftBarrier.transform.rotation.w);
            LeftBarrier.transform.position = new Vector2(0.75f, 30.88f);

            GameObject RightBarrier = GameObject.FindWithTag("R_Barrier");

            RightBarrier.transform.rotation = new Quaternion(0, 0, 90, RightBarrier.transform.rotation.w);
            RightBarrier.transform.position = new Vector2(2.42f, 39.14f);

            SFXManager.PlaySFX("ClearRubble");

            LastActivation = true;
        }
    }
}
