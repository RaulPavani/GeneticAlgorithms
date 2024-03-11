using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopulationManager : MonoBehaviour
{
    [SerializeField] private GameObject personPrefab;
    [SerializeField] private int populationSize = 10;

    List<GameObject> population = new List<GameObject>();

    public static float currentGenerationTime = 0;
    [SerializeField] private int maxGenerationTime = 5;
    int generation = 1;

    [Space]
    [Tooltip("Chance to ignore the characteristics of the parents")]
    [SerializeField] private int mutationChance = 25;

    GUIStyle guiStyle = new GUIStyle();
    void OnGUI()
    {
        guiStyle.fontSize = 36;
        guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "Generation: " + generation, guiStyle);
        GUI.Label(new Rect(10, 65, 100, 20), "Trial Time: " + (int)currentGenerationTime, guiStyle);
    }

    void Start()
    {
        for (int i = 0; i < populationSize; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-8, 8), Random.Range(-3.5f, 5f), 0);
            GameObject newMoth = Instantiate(personPrefab, pos, Quaternion.identity);

            Chromosome newChromo = newMoth.GetComponent<Chromosome>();
            newChromo.SetRandomColor();

            population.Add(newMoth);
        }
    }

    GameObject Breed(GameObject firstParent, GameObject secondParent)
    {
        Vector3 pos = new Vector3(Random.Range(-8, 8), Random.Range(-3.5f, 5f), 0);
        GameObject offspring = Instantiate(personPrefab, pos, Quaternion.identity);
        Chromosome breedChromo = offspring.GetComponent<Chromosome>();

        Chromosome firstChromo = firstParent.GetComponent<Chromosome>();
        Chromosome secondChromo = secondParent.GetComponent<Chromosome>();

        //Mutation chance - moth doesnt have the same characteristics as its parents
        if (Random.Range(0, 100) > mutationChance)
        {
            breedChromo.SetColor(0, Random.Range(0, 10) < 5 ? firstChromo.GetColor(0) : secondChromo.GetColor(0));
            breedChromo.SetColor(1, Random.Range(0, 10) < 5 ? firstChromo.GetColor(1) : secondChromo.GetColor(1));
            breedChromo.SetColor(2, Random.Range(0, 10) < 5 ? firstChromo.GetColor(2) : secondChromo.GetColor(2));
        }
        else
        {
            breedChromo.SetRandomColor();
        }

        return offspring;
    }

    void BreedNewPopulation()
    {
        List<GameObject> newPopulation = new List<GameObject>();
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<Chromosome>().timeToDie).ToList();

        population.Clear();

        //Breed upper half of sorted list
        for (int i = (int)(sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }

        for (int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }

        generation++;
    }

    void Update()
    {
        currentGenerationTime += Time.deltaTime;
        if (currentGenerationTime > maxGenerationTime)
        {
            BreedNewPopulation();
            currentGenerationTime = 0;
        }
    }
}
