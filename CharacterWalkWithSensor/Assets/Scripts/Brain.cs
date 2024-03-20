using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Brain : MonoBehaviour
{
    int DNALength = 2;
    public float timeAlive;
    public float timeWalking;
    public DNA dna;
    public GameObject eyes;
    bool alive = true;
    public bool seeGround = true;
    public LayerMask layerToIgnore;

    void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.CompareTag("Hazard"))
        {
            alive = false;
            //gameObject.SetActive(false);
        }
    }

    public void Init()
    {
        //initialise DNA
        //0 forward
        //1 left
        //2 right
        dna.DNAInit(DNALength, 3);
        timeAlive = 0;
        timeWalking = 0;
        alive = true;
    }

    private void Update()
    {
        if (!alive) return;

        seeGround = false;
        RaycastHit hit;

        //Debug.DrawRay(eyes.transform.position, eyes.transform.forward * 10, Color.red, 10);
        if (Physics.Raycast(eyes.transform.position, eyes.transform.forward * 10, out hit, 10f, ~layerToIgnore))
        {
            if (hit.collider.gameObject.CompareTag("Platform"))
            {
                seeGround = true;
            }
        }

        timeAlive = PopulationManager.elapsed;
        // read DNA
        float turn = 0;
        float move = 0;

        if (seeGround)
        {
            //make v relative to character and always move forward
            if (dna.GetGene(0) == 0)
            {
                move = 1;
                timeWalking += 1;
            }
            else if (dna.GetGene(0) == 1) turn = -90;
            else if (dna.GetGene(0) == 2) turn = 90;
        }
        else
        {
            if (dna.GetGene(1) == 0)
            {
                move = 1;
                timeWalking += 1;
            }
            else if (dna.GetGene(1) == 1) turn = -90;
            else if (dna.GetGene(1) == 2) turn = 90;
        }

        transform.Translate(0, 0, move * 0.1f);
        transform.Rotate(0, turn, 0);
    }
}