using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;



namespace Week4
{
    public class Player : MonoBehaviour
    {
        public int health
        {
            get { return _health; }
            private set { _health = value; }
        }
        [SerializeField ] private int _health = 10;

        private Enemy FindNewTarget()
        {
            //Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
            //int randomIndex = Random.Range(0, enemies.Length);
            //return enemies[randomIndex];

            GameObject enemyObj = GameObject.Find("Enemy");
            Enemy enemyComp = enemyObj.GetComponent<Enemy>();
            return enemyComp;


        }
        public void Damage(int amt)
        {
            
            health -= amt;
        }
        [ContextMenu("Attack")]
        void Attack()
        {
            Enemy target = FindNewTarget();
            target.Damage(4);
        }
    }
}
