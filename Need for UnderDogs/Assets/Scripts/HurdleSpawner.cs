using System.Collections.Generic;
using UnityEngine;

public class HurdleSpawner : MonoBehaviour
{
    // state variables
    [SerializeField] GameObject wallPrefab;

    // member variables
    List<Transform> tracks = new List<Transform>();

    void Start()
    {
        FetchTracks();
        //SpawnWalls();
    }

    // Fetch all the track transforms
    void FetchTracks()
    {
        foreach (Transform track in GameObject.FindGameObjectWithTag("Track").transform.GetChild(0).transform)
        {
            tracks.Add(track);
        }

        // Remove first and last 3 elements from tracks for excluding wall spawn at locations
        tracks.RemoveRange(0, 3);
        int tracksCount = tracks.Count;
        int removeStartIndex = tracksCount - 3;
        tracks.RemoveRange(removeStartIndex, 3);

        SpawnWalls();
    }

    // spawn random walls 
    void SpawnWalls()
    {
        List<int> randomIndexList = new List<int>();
        int randomIndex;

        for (int i = 0; i < 9; i++)
        {
            do
            {
                randomIndex = Random.Range(0, tracks.Count);
            }
            while (randomIndexList.Contains(randomIndex));
            randomIndexList.Add(randomIndex);

            Transform trackTransform = tracks[randomIndex];
            // Instantiate wall
            GameObject wall = Instantiate(wallPrefab, trackTransform);
            wall.transform.parent = transform;
            //print("Track Details: " + trackTransform.name + " X == " + trackTransform.localRotation.x + " Z ==" + trackTransform.localRotation.z);
            if (trackTransform.rotation.x == 0f && trackTransform.rotation.z == -0.7071068f)
            {
                wall.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else
            {
                //IsVertical = decision[Random.Range(0, decision.Length)];
                wall.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            } 
        }
    }
}
