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
        // Make sure the Candles are not active at the start
        foreach (ParticleSystem P1Light in P1Lights)
        {
            P1Lights[Increment].Stop();
            ParticleSystem.EmissionModule em = P1Lights[0].GetComponent<ParticleSystem>().emission;

            em.enabled = false;
            Increment++;    
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Checks if the collision objects tag match
        if (collision.gameObject.tag == "LightActivator")
        {
            // Removes the Darkness for Player 1
            Darkness.SetActive(false);

            // Turns on the Candles for Player 1
            Increment = 0;
            foreach (ParticleSystem P1Light in P1Lights)
            {
                P1Lights[Increment].Play();
                ParticleSystem.EmissionModule em = P1Lights[0].GetComponent<ParticleSystem>().emission;

                em.enabled = true;
                Increment++;
            }
        }
    }
}
