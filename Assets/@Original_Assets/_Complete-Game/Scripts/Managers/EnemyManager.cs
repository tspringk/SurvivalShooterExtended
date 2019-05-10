using UnityEngine;

namespace CompleteProject
{
    public class EnemyManager : MonoBehaviour
    {
        public enum Status
        {
            Normal,
            Boss,
            GameEnd
        };

        public PlayerHealth playerHealth;       // Reference to the player's heatlh.
        public GameObject enemy;                // The enemy prefab to be spawned.
        public float spawnTime = 3f;            // How long between each spawn.
        public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.

        private Status _status = Status.Normal;
        [SerializeField]
        private GameObject boss;

        void Start ()
        {
            Managers.ContentManager.ChangeEnemyStatus += ChangeStatus;
            // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
            InvokeRepeating ("Spawn", spawnTime, spawnTime);
        }

        private void ChangeStatus(Status status = Status.Normal)
        {
            if (!status.Equals(_status))
            {
                _status = status;
                if (status.Equals(Status.Boss) && boss != null)
                { 
                    boss.SetActive(true);
                }
            }
        }

        void Spawn ()
        {
            if (!_status.Equals(Status.Normal))
            {
                return;
            }

            // If the player has no health left...
            if (playerHealth.currentHealth <= 0f)
            {
                // ... exit the function.
                return;
            }

            // Find a random index between zero and one less than the number of spawn points.
            int spawnPointIndex = Random.Range (0, spawnPoints.Length);

            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
    }
}