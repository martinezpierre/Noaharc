using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour {

    public GameInfos gameInfos;

    public GameObject animalPrefab;

    public static AnimalManager instance;

    private void Awake()
    {
        instance = this;
    }
    
    public void GenerateAnimal(Vector3 position)
    {
        int animalIndex = Random.Range(0, gameInfos.currentAnimals.Count);

        AnimalInfo animalInfo = gameInfos.currentAnimals[animalIndex];

        GenerateAnimal(position, animalInfo);
    }

    public void GenerateAnimal(Vector3 position, AnimalInfo animalInfo)
    {
        GameObject animal = null;

        animal = Instantiate(animalPrefab, position, Quaternion.identity) as GameObject;

        GameObject body = Instantiate(Resources.Load("Prefab/Bodies/" + animalInfo.body), position, Quaternion.identity) as GameObject;
        body.transform.SetParent(animal.transform);
        GameObject head = Instantiate(Resources.Load("Prefab/Heads/" + animalInfo.head), body.GetComponent<BodyPartsPosition>().headPosition.position, Quaternion.identity) as GameObject;
        head.transform.SetParent(body.transform);
        GameObject tail = Instantiate(Resources.Load("Prefab/Tails/" + animalInfo.tail), body.GetComponent<BodyPartsPosition>().tailPosition.position, Quaternion.identity) as GameObject;
        tail.transform.SetParent(body.transform);

        foreach (Transform t in body.GetComponent<BodyPartsPosition>().otherPositions)
        {
            GameObject other = Instantiate(Resources.Load("Prefab/Others/" + animalInfo.other), t.position, Quaternion.identity) as GameObject;
            other.transform.SetParent(body.transform);
        }

        animal.GetComponent<Animal>().animalInfo = animalInfo;
    }

    public AnimalInfo MixAnimals(AnimalInfo animal1, AnimalInfo animal2)
    {
        AnimalInfo res = new AnimalInfo();

        res.body = Random.Range(0f, 1f) > 0.5f ? animal1.body : animal2.body;
        res.head = Random.Range(0f, 1f) > 0.5f ? animal1.head : animal2.head;
        res.tail = Random.Range(0f, 1f) > 0.5f ? animal1.tail : animal2.tail;
        res.other = Random.Range(0f, 1f) > 0.5f ? animal1.other : animal2.other;

        res.weight = Random.Range(animal1.weight, animal2.weight) + Random.Range(0, 50);

        res.name = new List<string>();
        
        for(int i = 0; i < animal1.name.Count/2; i++)
        {
            res.name.Add(animal1.name[i]);
        }

        for (int i = animal2.name.Count/2; i < animal2.name.Count; i++)
        {
            res.name.Add(animal2.name[i]);
        }

        return res;
    }

    public void SaveAnimals(List<AnimalInfo> newAnimals)
    {
        gameInfos.currentAnimals = newAnimals;
    }
}
