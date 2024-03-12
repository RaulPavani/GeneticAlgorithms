using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour
{
    public List<int> genes = new List<int>();
    public int dnaLength = 0;
    public int maxvalues = 0;

    public void DNAInit(int lenght, int maxValues)
    {
        this.dnaLength = lenght;
        this.maxvalues = maxValues;
        SetRandom();
    }

    public void SetRandom()
    {
        genes.Clear();
        for (int i = 0; i < dnaLength; i++)
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
        //for (int i = 0; i < dnaLength; i++)
        //{
        //    if (i < dnaLength / 2)
        //    {
        //        int c = firstDNA.genes[i];
        //        genes[i] = c;
        //    }
        //    else
        //    {
        //        int c = secondDNA.genes[i];
        //        genes[i] = c;
        //    }
        //}

        // add the first 1 / 2 of dna1 genes
        genes = firstDNA.genes.GetRange(0, firstDNA.dnaLength / 2);
        // add the second 1/2 of dna2 genes
        genes.AddRange(secondDNA.genes.GetRange(secondDNA.dnaLength / 2, secondDNA.dnaLength / 2));
    }

    public void Mutate()
    {
        genes[Random.Range(0, dnaLength)] = Random.Range(0, maxvalues);
    }

    public int GetGene(int index)
    {
        return genes[index];
    }
}
