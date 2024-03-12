using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour
{
    List<int> genes = new List<int>();
    int dnaLenght = 0;
    int maxvalues = 0;

    public DNA(int lenght, int maxValues)
    {
        this.dnaLenght = lenght;
        this.maxvalues = maxValues;
    }

    public void SetRandom()
    {
        genes.Clear();
        for (int i = 0; i < dnaLenght; i++)
        {
            genes.Add(Random.Range(0, maxvalues));
        }
    }

    public void SetInt(int index, int value)
    {
        genes[index] = value;
    }

    public void Combine(DNA firstDNA, DNA secondDNA)
    {
        //Combines first half of parent1 and second half of parent2
        for (int i = 0; i < dnaLenght; i++)
        {
            if(i < dnaLenght / 2)
            {
                int c = firstDNA.genes[i];
                genes[i] = c;
            }
            else
            {
                int c = secondDNA.genes[i];
                genes[i] = c;
            }
        }
    }

    public void Mutate()
    {
        genes[Random.Range(0, dnaLenght)] = Random.Range(0, maxvalues);
    }

    public int GetGene(int index)
    {
        return genes[index];
    }
}
