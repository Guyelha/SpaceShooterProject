using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Parameters", menuName = "Enemy Parameters")]
public class EnemyParameterSO : ScriptableObject
{
    public Sprite sprite;
    public int health;
}
