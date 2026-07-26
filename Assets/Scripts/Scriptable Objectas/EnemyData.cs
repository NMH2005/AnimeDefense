using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public GameObject prefab;
    public float maxHealth = 100f;
    public float speed = 1f;
    public int value = 50;
}
