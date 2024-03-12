using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Brain : MonoBehaviour
{
    public int DNALength = 6;
    public float timeAlive;
    public DNA dna;

    private ThirdPersonCharacter character;
    private Vector3 moveVector;
    private bool jump;
    bool alive = true;
    public float distanceTravelled;
    private Vector3 startPos;

    public void Init()
    {
        //DNA
        //0- forward, 1-back, 2-left, 3-right, 4-jump, 5 crouch

        dna.DNAInit(DNALength, 6);
        character = GetComponent<ThirdPersonCharacter>();
        timeAlive = 0;
        alive = true;
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        float h = 0;
        float v = 0;
        bool crouch = false;
        if (dna.GetGene(0) == 0) v = 1;
        if (dna.GetGene(1) == 1) v = -1;
        if (dna.GetGene(2) == 2) h = -1;
        if (dna.GetGene(3) == 3) h = 1;
        if (dna.GetGene(4) == 4) jump = true;
        if (dna.GetGene(5) == 5) crouch = true;

        moveVector = v * Vector3.forward + h * Vector3.right;
        character.Move(moveVector, crouch, jump);
        jump = false;

        if (alive)
        {
            timeAlive += Time.deltaTime;
            distanceTravelled = Vector3.Distance(transform.position, startPos);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Hazard"))
        {
            alive = false;
        }
    }
}
