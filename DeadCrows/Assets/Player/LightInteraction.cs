using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInteraction : MonoBehaviour
{
    public ParticleSystem[] P1Lights;
    public GameObject Darkness;
    int Increment = 0;

    void Start()
    {
        
       

        foreach (ParticleSystem P1Light in P1Lights)
        {
            P1Lights[Increment].Stop();
            ParticleSystem.EmissionModule em = P1Lights[0].GetComponent<ParticleSystem>().emission;
            //  P1Lights.EmissionModule.enabled = true;
            //P1Lights.GetComponent<ParticleSystem>().emission
            em.enabled = false;
            Increment++;    
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {


      // Debug.Log("Do something else here");
        //Checks if the collision objects tag  match
        if (collision.gameObject.tag == "LightActivator")
        {
            Darkness.SetActive(false);
            // P1Lights.enabled = true;
            Increment = 0;
            foreach (ParticleSystem P1Light in P1Lights)
            {
                P1Lights[Increment].Play();
                ParticleSystem.EmissionModule em = P1Lights[0].GetComponent<ParticleSystem>().emission;
                //  P1Lights.EmissionModule.enabled = true;
                //P1Lights.GetComponent<ParticleSystem>().emission
                em.enabled = true;
                Increment++;
            }

          //  P1Lights[0].Play();
          //ParticleSystem.EmissionModule em = P1Lights[0].GetComponent<ParticleSystem>().emission;
          ////  P1Lights.EmissionModule.enabled = true;
          //  //P1Lights.GetComponent<ParticleSystem>().emission
          // em.enabled = true;
           //em.enabled = false;
           //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Do something else here");
        }
    }
}
