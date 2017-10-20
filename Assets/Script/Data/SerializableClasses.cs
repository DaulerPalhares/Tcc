﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializableClasses.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used for the mesh generation script.
/// </summary>
public class MeshData
{
    /// <summary>
    /// Array with all vertices.
    /// </summary>
    public Vector3[] Vertices;
    /// <summary>
    /// Array with all triangles.
    /// </summary>
    public int[] Triangles;
    /// <summary>
    /// Mesh uv map
    /// </summary>
    public Vector2[] Uvs;
    /// <summary>
    /// I DONT FUCKING KNOW HOW TO EXPLAIN SEE MESHDATA.
    /// </summary>
    private int _triangleIndex;
    /// <summary>
    /// Bool to use activate the flat shading.
    /// </summary>

    /// <summary>
    /// MeshData constructor.
    /// </summary>
    /// <param name="meshWidth">Mesh width</param>
    /// <param name="meshHeight">Mesh Height</param>
    /// <param name="flat">Will use flat shading?</param>
    public MeshData(int meshWidth, int meshHeight)
    {
        Vertices = new Vector3[meshWidth * meshHeight];
        Uvs = new Vector2[meshWidth * meshHeight];
        Triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
    }

    /// <summary>
    /// To add triangles in the trinangles array.
    /// </summary>
    /// <param name="a">Position a</param>
    /// <param name="b">Position b</param>
    /// <param name="c">Position c</param>
    public void AddTriangle(int a, int b, int c)
    {
        Triangles[_triangleIndex] = a;
        Triangles[_triangleIndex + 1] = b;
        Triangles[_triangleIndex + 2] = c;
        _triangleIndex += 3;
    }

    /// <summary>
    /// Flat shading function. This will put all triangles independent.
    /// </summary>
    public void FlatShading()
    {
        Vector3[] flatShadedVertices = new Vector3[Triangles.Length];
        Vector2[] flatShaderUv = new Vector2[Triangles.Length];
        for (int i = 0; i < Triangles.Length; i++)
        {
            flatShadedVertices[i] = Vertices[Triangles[i]];
            flatShaderUv[i] = Uvs[Triangles[i]];

            Triangles[i] = i;
        }
        Vertices = flatShadedVertices;
        Uvs = flatShaderUv;

    }
    /// <summary>
    /// Function to mesh creation.
    /// </summary>
    /// <returns>New Mesh</returns>
    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh
        {
            vertices = Vertices,
            triangles = Triangles,
            uv = Uvs
        };
        mesh.RecalculateNormals();
        return mesh;
    }
}
/// <summary>
/// Class used to manager the resources in-game.
/// </summary>
public class GameResources
{
    /// <summary>
    /// Ammout of wood.
    /// </summary>
    public int Wood;
    /// <summary>
    /// Ammout of stone.
    /// </summary>
    public int Stone;
    /// <summary>
    /// Ammout of Iron.
    /// </summary>
    public int Iron;
    /// <summary>
    /// Ammout of food.
    /// </summary>
    public int Food;
}

/// <summary>
/// Class used for monitoring the game time.
/// </summary>
[System.Serializable]
public class DateTimeGame
{
    /// <summary>
    /// In-game hour (0-23 format).
    /// </summary>
    [Range(0,23)]
    public int Hour;
    /// <summary>
    /// In-game minutes (0-59 format).
    /// </summary>
    [Range(0,59)]
    public int Minutes;
    /// <summary>
    /// Speed that game will run (1 = 1min/second).
    /// </summary>
    [Range(1,4)]
    public int Speed;
}

/// <summary>
/// Class used for seed plantation variables.
/// </summary>
[System.Serializable]
public class PlantationSeeds
{
    /// <summary>
    /// Seed Name.
    /// </summary>
    public string SeedName;
    /// <summary>
    /// Days to full grow the seed and give food.
    /// </summary>
    public int DaysToGrow;
    /// <summary>
    /// Time to harverst the plantation (Farm size will affect the total time).
    /// </summary>
    public int DaysToHarvest;
    /// <summary>
    /// Minimum temperature that the plantation will resist.
    /// </summary>
    public int MinTemperatureResistence;
    /// <summary>
    /// Ammount of food given after havesting the grow seed.
    /// </summary>
    public int AmmountFood;
}

/// <summary>
/// Class used for in-game season.
/// </summary>
[System.Serializable]
public class Season {
    /// <summary>
    /// Season Name.
    /// </summary>
    public string SeasonName;
    /// <summary>
    /// Number of days that this season will have.
    /// </summary>
    public int Days;
    /// <summary>
    /// Season minimum temperature.
    /// </summary>
    public float MinTemp;
    /// <summary>
    /// Season maximum temperature.
    /// </summary>
    public float MaxTemp;
}

/// <summary>
/// Base class for job.
/// </summary>
[System.Serializable]
public class Job {
    /// <summary>
    /// Job Name.
    /// </summary>
    public string JobName;
    /// <summary>
    /// Moviment speed.
    /// </summary>
    public float Speed;
    /// <summary>
    /// Famale can work on this job?
    /// </summary>
    public bool Female;
    /// <summary>
    /// Male can work on this job?
    /// </summary>
    public bool Male;
}

/// <summary>
/// Base class for buildings.
/// </summary>
[System.Serializable]
public class Building
{
    /// <summary>
    /// Building Name.
    /// </summary>
    public string BuildingName;
    /// <summary>
    /// Building type (used for diferent scripts to work).
    /// </summary>
    public TypeBuilding Type;
    /// <summary>
    /// Game object that will spaw this building.
    /// </summary>
    public GameObject BuildingGameObject;
    /// <summary>
    /// Lumber cost to build.
    /// </summary>
    public int LumberCost;
    /// <summary>
    /// Rock cost to build.
    /// </summary>
    public int RockCost;
    /// <summary>
    /// Metal cost to build.
    /// </summary>
    public int MetalCost;
    /// <summary>
    /// Time to finish the construction.
    /// </summary>
    public float TimeToBuild;
    /// <summary>
    /// Timer that will decrease (used fo the construction
    /// and if the building produces something this will
    /// be the cooldown.
    /// </summary>
    public float Timer { get; protected set; }
    /// <summary>
    /// Is the building alread constructed.
    /// </summary>
    public bool IsFinished { get; protected set; }
    /// <summary>
    /// How many Citzens that building will support.
    /// </summary>
    public int MaxCitzenInside;
    /// <summary>
    /// Position x on the world.
    /// </summary>
    public int Xpos { get; protected set; }
    /// <summary>
    /// Position y on the world.
    /// </summary>
    public int Ypos { get; protected set; }
    /// <summary>
    /// Position z on the world.
    /// </summary>
    public int Zpos { get; protected set; }
    /// <summary>
    /// Rotation on X axis.
    /// </summary>
    public int Xrot { get; protected set; }
    /// <summary>
    /// Rotation on Y axis.
    /// </summary>
    public int Yrot { get; protected set; }
    /// <summary>
    /// Rotation on Z axis.
    /// </summary>
    public int Zrot { get; protected set; }
    /// <summary>
    /// Default Constructor.
    /// </summary>
    public Building()
    {
    }
}

//--------------------Ignorar isso-------------------//
//A-B-C-D-E-F-G-H-I-J-K-L-M-N-O-P-Q-R-S-T-U-V-W-X-Y-Z//
//---------------------------------------------------//
/// <summary>
/// Enum with all building types.
/// </summary>
public enum TypeBuilding {
    Blacksmith,
    Church,
    Decoration,
    Docks,
    Farm,
    Graveyard,
    Hosipital,
    House,
    LivingFarm,
    Market,
    Mine,
    Orchard,
    School,
    Storage,
    Tailor,
    Tavern,
    TownHall,
    Woodcutter,
}

/// <summary>
/// Enum with filter organizers to sort some arrays in-game.
/// </summary>
public enum OrganizerFilter
{
    Name,
    AgeAsc,
    AgeDesc,
    HappyAsc,
    HappyDesc,
    Job,
    GenereFm,
    GenereMf,
}

/// <summary>
/// Enum that handle all general building events.
/// </summary>
public enum BuildingEventsHandler
{
    Complete,
    NoLumber,
    NoStone,
    NoIron,
    InvalidPos,
}

/// <summary>
/// Enum that handle all house events.
/// </summary>
public enum HouseEventsHandler
{
    Sucess,
    HabitantesFull,
    EnoughtFamilies,
    TooCold,
    EmptyHouse,
}

public enum FarmEventsHandler
{
    Idle,
    Planting,
    Growing,
    Harvesting,
    Decaying,
    Plage,
}

/// <summary>
/// Citzen genere.
/// </summary>
public enum CitzenGenere
{
    Female,
    Male,
}