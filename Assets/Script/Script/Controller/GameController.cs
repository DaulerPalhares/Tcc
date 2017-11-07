﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameController.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RandomNameGeneratorLibrary;

/// <summary>
/// Class with all game autorities.
/// </summary>
public class GameController : MonoBehaviour {
    /// <summary>
    /// Singleton
    /// </summary>
    public static GameController Instance { get; protected set; }
    /// <summary>
    /// Prefabs for instanciate people.
    /// </summary>
    public GameObject PeoplePrefab;
    /// <summary>
    /// Current city data.
    /// </summary>
    public CityData City;

    /// <summary>
    /// GameData.
    /// </summary>
    public GameDataEditable GameData;

    public TypeBuilding SelectedTypeToBuild;
    public string SelectedBuildingName;

    void Awake () {
        if (Instance != null)
        {
            Debug.LogError("You can't have 2 Game Controllers");
        }
        Instance = this;
        NewCity("","Easy");
	    StartCoroutine("TimeController");
	}
    /// <summary>
    /// Create a new City
    /// </summary>
    /// <param name="cName">City Name if empty will generate a random one</param>
    /// <param name="difMode">Difficulty of the game</param>
    private void NewCity(string cName,string difMode)
    {
        City.Time = new DateTimeGame(GameData.Seasons);
        if (cName == "")
        {
            var rndName = new PlaceNameGenerator();
            City.CityName = rndName.GenerateRandomPlaceName();
        }
        else
        {
            City.CityName = cName;
        }
        City.CityHabitants = new List<GameObject>();
        City.name = cName;
        switch (difMode)
        {
            case "Easy":
                for (var i = 0; i < 15; i++)
                {
                    var cityTemp = Instantiate(PeoplePrefab, transform.position, Quaternion.identity);
                    cityTemp.GetComponent<Citzen>().Init(new System.Random(i + (int)(Time.deltaTime * 100)));
                    cityTemp.name = cityTemp.GetComponent<Citzen>().Name;
                    City.CityHabitants.Add(cityTemp);
                }
                City.CityResources.Wood = 400;
                City.CityResources.Stone = 400;
                City.CityResources.Iron = 100;
                City.CityResources.Food = 400;
                break;
            case "Normal":
                for ( var i = 0 ; i < 10 ; i++ ) {
                    var cityTemp = Instantiate(PeoplePrefab, transform.position, Quaternion.identity);
                    cityTemp.GetComponent<Citzen>().Init(new System.Random(i + (int)(Time.deltaTime * 100)));
                    cityTemp.name = cityTemp.GetComponent<Citzen>().Name;
                    City.CityHabitants.Add(cityTemp);
                }
                City.CityResources.Wood = 250;
                City.CityResources.Stone = 250;
                City.CityResources.Iron = 10;
                City.CityResources.Food = 300;
                break;
            case "Hard":
                for ( var i = 0 ; i < 8 ; i++ ) {
                    var cityTemp = Instantiate(PeoplePrefab, transform.position, Quaternion.identity);
                    cityTemp.GetComponent<Citzen>().Init(new System.Random(i+(int)(Time.deltaTime*100)));
                    cityTemp.name = cityTemp.GetComponent<Citzen>().Name;
                    City.CityHabitants.Add(cityTemp);
                }
                City.CityResources.Wood = 100;
                City.CityResources.Stone = 100;
                City.CityResources.Iron = 0;
                City.CityResources.Food = 200;
                break;
        }
    }

    private IEnumerator TimeController()
    {
        while (true)
        {
            City.Time.TimePass(City.Time.Speed);
            yield return new WaitForSeconds(1);
        }
        // ReSharper disable once IteratorNeverReturns
    }

}