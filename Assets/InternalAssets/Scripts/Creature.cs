using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public enum EnemyOrFriendly { friendly,enemy};
    public int level = 1;
    public int hp = 100;
    public int damage = 30;
    public EnemyOrFriendly enemyOrFriendly;
}
