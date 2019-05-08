using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

namespace CompleteProject
{
    public class PlayerHealth : MonoBehaviour
    {
        public int startingHealth = 100;                            // The amount of health the player starts the game with.
        public int currentHealth;                                   // The current health the player has.
        public Slider healthSlider;                                 // Reference to the UI's health bar.
        public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
        public AudioClip deathClip;                                 // The audio clip to play when the player dies.
        public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
        public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.


        Animator anim;                                              // Reference to the Animator component.
        AudioSource playerAudio;                                    // Reference to the AudioSource component.
        PlayerMovement playerMovement;                              // Reference to the player's movement.
        PlayerShooting playerShooting;                              // Reference to the PlayerShooting script.
        bool isDead;                                                // Whether the player is dead.
        bool damaged;                                               // True when the player gets damaged.

        float playerDeathMotionDuration = 0;

        void Awake ()
        {
            // Setting up the references.
            anim = GetComponent <Animator> ();
            playerAudio = GetComponent <AudioSource> ();
            playerMovement = GetComponent <PlayerMovement> ();
            playerShooting = GetComponentInChildren <PlayerShooting> ();

            // Set the initial health of the player.
            currentHealth = startingHealth;
        }

        private void Start()
        {
            AnimationStateEventBehaviour.onStateEnter += OnStateEnter;
        }

        private void OnStateEnter(AnimationStateEventBehaviour.EventArgs args)
        {
            playerDeathMotionDuration = args.stateInfo.length;
        }

        void Update ()
        {
            // If the player has just been damaged...
            if(damaged)
            {
                // ... set the colour of the damageImage to the flash colour.
                damageImage.color = flashColour;
            }
            // Otherwise...
            else
            {
                // ... transition the colour back to clear.
                damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }

            // Reset the damaged flag.
            damaged = false;
        }


        public void TakeDamage (int amount)
        {
            if (isDead) { return; }

            // Set the damaged flag so the screen will flash.
            damaged = true;

            // Reduce the current health by the damage amount.
            currentHealth -= amount;

            // Set the health bar's value to the current health.
            healthSlider.value = currentHealth;

            currentHealth = Mathf.Max(currentHealth, 1);

            // Play the hurt sound effect.
            playerAudio.Play ();

            // If the player has lost all it's health and the death flag hasn't been set yet...
            if(currentHealth <= 1 && !isDead)
            {
                // ... it should die.
                Death ();
            }
        }

        // 課題１　ゲームオーバー課題の実装、ここからです
        void Death()
        {
            // isDeadフラグを立てることで、この関数が繰り返し実行されないようにします
            isDead = true;
            // 別スクリプト(PlayerShooting.cs)にあるプレイヤーが撃った攻撃を消去する処理を実行します。
            playerShooting.DisableEffects();
        
            StartCoroutine(InvokeGameoverByCoroutine());
        }

        private IEnumerator InvokeGameoverByCoroutine()
        {
            InvokeDeathMotion();
            yield return new WaitForSeconds(0.5f);

            Debug.Log(playerDeathMotionDuration);
            yield return new WaitForSeconds(playerDeathMotionDuration);

            InvokeShowGameOver();
            yield return new WaitForSeconds(4.0f);

            SceneManager.LoadScene(0);

            yield break;
        }

        private void InvokeDeathMotion() {
            // 自身のAnimator Controllerパラメータの「Die」を発火させて、
            // Deathステートへ移行し、アニメーションを実行します
            // DeathステートのAnimation Clipには、タイムライン上に関数実行のイベントが埋め込まれており、
            // Deathのアニメーションが終了すると同時に、このスクリプトの末尾にあるRestartLevel()関数が実行されます
            anim.SetTrigger("Die");

            // Audio SourceコンポーネントのAudio Clipパラメータを、Death用のものに差し替えて、再生
            playerAudio.clip = deathClip;
            playerAudio.Play();

            // プレイヤーの移動と攻撃を、関連するフラグをfalseにすることで、実行できないようにする
            playerMovement.enabled = false;
            playerShooting.enabled = false;
        }

        private void InvokeShowGameOver()
        {
            currentHealth = 0;
        }



            // 元のコード
            //void Death ()
            //{
            //    // Set the death flag so this function won't be called again.
            //    isDead = true;

            //    // Turn off any remaining shooting effects.
            //    playerShooting.DisableEffects ();

            //    // Tell the animator that the player is dead.
            //    anim.SetTrigger ("Die");

            //    // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
            //    playerAudio.clip = deathClip;
            //    playerAudio.Play ();

            //    // Turn off the movement and shooting scripts.
            //    playerMovement.enabled = false;
            //    playerShooting.enabled = false;
            //}


            public void RestartLevel ()
        {
            // Reload the level that is currently loaded.
            //SceneManager.LoadScene (0);
        }
    }
}