using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;

public class PopulationManager : MonoBehaviour
{
    public GameObject botPrefab;
    public int populationSize = 50;
    public int mutationChance = 10;
    List<GameObject> population = new List<GameObject>();
    public static float elapsed = 0;
    public float trialTime = 5;
    int generation = 1;

    GUIStyle guiStyle = new GUIStyle();

    private void OnGUI()
    {
        guiStyle.fontSize = 25;
        guiStyle.normal.textColor = Color.white;
        GUI.BeginGroup(new Rect(10, 10, 250, 250));
        GUI.Box(new Rect(0, 0, 140, 140), "Stats:", guiStyle);
        GUI.Label(new Rect(10, 25, 200, 30), $"Gen: {generation}", guiStyle);
        GUI.Label(new Rect(10, 50, 200, 30), string.Format("Time: {0:0.00}", elapsed), guiStyle);
        GUI.Label(new Rect(10, 75, 200, 30), $"Population: {population.Count}", guiStyle);
        GUI.EndGroup();
    }

    private void Start()
    {
        for (int i = 0; i < populationSize; i++)
        {
            float randomX = Random.Range(-2f, 2f);
            float randomZ = Random.Range(-2f, 2f);

            Vector3 startingPos = transform.position + new Vector3(randomX, 0, randomZ);
            GameObject newBot = Instantiate(botPrefab, startingPos, Quaternion.identity);
            newBot.GetComponent<Brain>().Init();
            population.Add(newBot);
        }
    }

    GameObject Breed(GameObject firstParent, GameObject secondParent)
    {
        float randomX = Random.Range(-2f, 2f);
        float randomZ = Random.Range(-2f, 2f);
        Vector3 startingPos = transform.position + new Vector3(randomX, 0, randomZ);

        GameObject breededBot = Instantiate(botPrefab, startingPos, Quaternion.identity);
        Brain botBrain = breededBot.GetComponent<Brain>();
        if (Random.Range(0, 100) < mutationChance)
        {
            botBrain.Init();
            botBrain.dna.Mutate();
        }
        else
        {
            Brain firstParentBrain = firstParent.GetComponent<Brain>();
            Brain secondParentBrain = secondParent.GetComponent<Brain>();

            botBrain.Init();
            botBrain.dna.Combine(firstParentBrain.dna, secondParentBrain.dna);
        }

        return breededBot;
    }

    void BreedNewPopulaton()
    {
        List<GameObject> sortedList = population.OrderBy(x => x.GetComponent<Brain>().timeAlive).ThenBy(x => x.GetComponent<Brain>().distanceTravelled).ToList();
        population.Clear();

        for (int i = (sortedList.Count / 2) - 1; i < sortedList.Count - 1; i++)
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

    private void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= trialTime)
        {
            BreedNewPopulaton();
            elapsed = 0;
        }
    }
}
