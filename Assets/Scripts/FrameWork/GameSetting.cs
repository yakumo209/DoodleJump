using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
public class GameSetting
{
    [Serializable]
    public class NormalTile
    {
        public float minHeight;
        public float maxHeight;
        public float weight;
    }
    [Serializable]
    public class BrokenTile
    {
        public float minHeight;
        public float maxHeight;
        public float weight;
    }
    [Serializable]
    public class OneTimeOnly
    {
        public float minHeight;
        public float maxHeight;
        public float weight;
    }
    [Serializable]
    public class SpringTile
    {
        public float minHeight;
        public float maxHeight;
        public float weight;
    }
    [Serializable]
    public class MovingHorizontally
    {
        public float minHeight;
        public float maxHeight;
        public float distance;
        public float speed;
        public float weight;
    }
    [Serializable]
    public class MovingVertiacally
    {
        public float minHeight;
        public float maxHeight;
        public float distance;
        public float speed;
        public float weight;
    }

    public NormalTile normalTile;
    public BrokenTile brokenTile;
    public OneTimeOnly oneTimeOnly;
    public SpringTile springTile;
    public MovingHorizontally movingHorizontally;
    public MovingVertiacally movingVertiacally;
    public float itemProbability;
    public float coinProbability;
    public float enemyProbability;
}
