using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(order = 1, fileName = "NewLeaderboardContainer", menuName = "My Containers/Leaderboard Container")]
public class LeaderboardContainer : ScriptableObject
{

    [SerializeField] private string[] playerCountries;
    [SerializeField] private string[] playerUsernames;


    public string RandomPlayerCountry()
    {
        return playerCountries[Random.Range(0, playerCountries.Length)];
    }
    public string RandomPlayerUsername()
    {
        return playerUsernames[Random.Range(0, playerUsernames.Length)];
    }
}
