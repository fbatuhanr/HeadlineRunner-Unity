using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(order = 0, fileName = "NewSentenceContainer", menuName = "My Containers/Sentence Container")]
public class SentenceContainer : ScriptableObject
{
    [SerializeField] private string[] names;

    public enum points
    {
        ordinary=1,
        interesting=2,
        amazing=3,
        wow=4
    };
    
    [Serializable] public struct verb {
        public string verbName;
        public points verbPoint;
    }
    public verb[] verbs;
    
    [Serializable] public struct place {
        public string placeName;
        public points placePoint;
    }
    public place[] places;
    
    [Serializable] public struct action {
        public string actionName;
        public points actionPoint;
    }
    public action[] actions;

    
    public string RandomName()
    {
        return names[Random.Range(0, names.Length)];
    }

    public verb RandomVerbs()
    {
        verb randomVerb = new verb();
        
        int randomIndex = Random.Range(0, verbs.Length);
        randomVerb.verbName = verbs[randomIndex].verbName;
        randomVerb.verbPoint = verbs[randomIndex].verbPoint;

        return randomVerb;
    }
    
    public place RandomPlaces()
    {
        place randomPlace = new place();
        
        int randomIndex = Random.Range(0, places.Length);
        randomPlace.placeName = places[randomIndex].placeName;
        randomPlace.placePoint = places[randomIndex].placePoint;

        return randomPlace;
    }
    
    public action RandomActions()
    {
        action randomAction = new action();
        
        int randomIndex = Random.Range(0, actions.Length);
        randomAction.actionName = actions[randomIndex].actionName;
        randomAction.actionPoint = actions[randomIndex].actionPoint;

        return randomAction;
    }
}
