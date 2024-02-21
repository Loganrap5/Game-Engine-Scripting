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

        //sound
        private AudioSource audio;

        //be able to set multiple audio sound wihtout making a million differnt components
        [SerializeField] AudioClip attackSound;
        [SerializeField] AudioClip damagedSound;

        private void Awake()
        {
            audio = GetComponent<AudioSource>();
        }

        private Enemy FindNewTarget()
        {
            //Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
            //int randomIndex = Random.Range(0, enemies.Length);
            //return enemies[randomIndex];

            //or use GameObject.FindGameObjectWithTag("Enemy");
            //search thru tags

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
            //for audio being able to use mulitple audio sources for the player
            //audio.PlayOneShot(attackSound);

            //use the audiomanager script
            AudioManager.PlaySound(AudioManager.SoundType.ATTACK);
            
        }
    }
}
