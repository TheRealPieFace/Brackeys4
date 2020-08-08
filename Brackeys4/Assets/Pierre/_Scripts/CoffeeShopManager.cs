using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeShopManager : MonoBehaviour
{
    public bool rewinding = false;

    public GameObject barrista;
    public GameObject you;
    public GameObject date;
    public GameObject man;
    public GameObject girl;

    public List<PointOfInterest> barristaPath1 = new List<PointOfInterest>();
    public List<PointOfInterest> barristaPath2 = new List<PointOfInterest>();

    public List<PointOfInterest> youPath1 = new List<PointOfInterest>();
    public List<PointOfInterest> youPath2 = new List<PointOfInterest>();

    public List<PointOfInterest> datePath1 = new List<PointOfInterest>();
    public List<PointOfInterest> datePath2 = new List<PointOfInterest>();

    public List<PointOfInterest> manPath1 = new List<PointOfInterest>();
    public List<PointOfInterest> manPath2 = new List<PointOfInterest>();

    public List<PointOfInterest> girlPath1 = new List<PointOfInterest>();
    public List<PointOfInterest> girlPath2 = new List<PointOfInterest>();

    private void Start()
    {
        TogglePath1();
    }

    public void TogglePath1()
    {
        if (barrista.GetComponent<TimelineController>().locations.Contains(barristaPath2[0]))
        {
            RemovePaths(barrista.GetComponent<TimelineController>(), barristaPath2);
            RemovePaths(you.GetComponent<TimelineController>(), youPath2);
            RemovePaths(date.GetComponent<TimelineController>(), datePath2);
            RemovePaths(man.GetComponent<TimelineController>(), manPath2);
            RemovePaths(girl.GetComponent<TimelineController>(), girlPath2);
        }

        AddPaths(barrista.GetComponent<TimelineController>(), barristaPath1);
        AddPaths(you.GetComponent<TimelineController>(), youPath1);
        AddPaths(date.GetComponent<TimelineController>(), datePath1);
        AddPaths(man.GetComponent<TimelineController>(), manPath1);
        AddPaths(girl.GetComponent<TimelineController>(), girlPath1);
    }

    public void TogglePath2()
    {
        if (barrista.GetComponent<TimelineController>().locations.Contains(barristaPath1[0]))
        {
            RemovePaths(barrista.GetComponent<TimelineController>(), barristaPath1);
            RemovePaths(you.GetComponent<TimelineController>(), youPath1);
            RemovePaths(date.GetComponent<TimelineController>(), datePath1);
            RemovePaths(man.GetComponent<TimelineController>(), manPath1);
            RemovePaths(girl.GetComponent<TimelineController>(), girlPath1);
        }

        AddPaths(barrista.GetComponent<TimelineController>(), barristaPath2);
        AddPaths(you.GetComponent<TimelineController>(), youPath2);
        AddPaths(date.GetComponent<TimelineController>(), datePath2);
        AddPaths(man.GetComponent<TimelineController>(), manPath2);
        AddPaths(girl.GetComponent<TimelineController>(), girlPath2);
    }

    public void RemovePaths(TimelineController character, List<PointOfInterest> paths)
    {
        foreach(var path in paths)
        {
            character.locations.Remove(path);
        }
    }

    public void AddPaths(TimelineController character, List<PointOfInterest> paths)
    {
        foreach (var path in paths)
        {
            character.locations.Add(path);
        }
    }

}
