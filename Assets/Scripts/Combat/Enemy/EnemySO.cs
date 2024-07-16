using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Limb", menuName = "Combat/Enemy/EnemyScriptable", order = 1)]
public class EnemySO : ScriptableObject
{

        public List<Limb> Limbs;
        public float Initiative;


}