using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int score;

    public static implicit operator PlayerScore(ScoreBoard v)
    {
        throw new NotImplementedException();
    }
}
