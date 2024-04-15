using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EnemyCollision : MonoBehaviour
{
    private Rigidbody2D rb;
    private ZombieController zombieController;
    private MimicController mimicController;
    private MageController mageController;
    private SkeletonController skeletonController;
    private GameManager man;
    public MissedBeat mBeat;
    private bool enemyAttack = false;
    public VolumeProfile profile;
    UnityEngine.Rendering.Universal.FilmGrain grain;
    // Start is called before the first frame update
    private void Start()
    {
        man = GameManager.Instance;
        if (profile.TryGet<FilmGrain>(out grain))
        {
            grain.intensity.Override(0f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.tag == "TriggerZone") return;

        if (collision.gameObject.tag == "Enemy")
        {
            enemyAttack = true;
            PlayerHealth.TakeDamage();
            if (profile.TryGet<FilmGrain>(out grain))
            {
                grain.intensity.Override(1f);
            }
            Invoke("ScaryEffect", 3);
            if (collision.gameObject.name.Contains("Mage"))
            {
                mageController = collision.gameObject.GetComponent<MageController>();
                mageController.enabled = false;
            }
            else if(collision.gameObject.name.Contains("Zombie"))
            {
                zombieController = collision.gameObject.GetComponent<ZombieController>();
                //zombieController.TurnBox();
                zombieController.enabled = false;
            }
            else if(collision.gameObject.name.Contains("Mimic"))
            {
                mimicController = collision.gameObject.GetComponent<MimicController>();
                mimicController.enabled = false;
            }
            else if (collision.gameObject.name.Contains("Skeleton"))
            {
                skeletonController = collision.gameObject.GetComponent<SkeletonController>();
                skeletonController.enabled = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (this.gameObject.tag == "TriggerZone") return;
        if (collision.gameObject.tag == "Enemy")
        {
            enemyAttack = false;
            if (collision.gameObject.name.Contains("Mage"))
            {
                Invoke("StartMage", 3f);
            }
            else if (collision.gameObject.name.Contains("Zombie"))
            {
                Invoke("StartZombie", 3f);
            }
            else if (collision.gameObject.name.Contains("Mimic"))
            {
                Invoke("StartMimic", 3f);
            }
            else if (collision.gameObject.name.Contains("Skeleton"))
            {
                Invoke("StartSkeleton", 3f);
            }
        }
    }
    void StartMage()
    {
        mageController.enabled = true;
    }
    void StartZombie()
    {
        zombieController.enabled = true;
        //zombieController.TurnBox();
    }
    void StartMimic()
    {
        mimicController.enabled = true;
    }
    void StartSkeleton()
    {
        skeletonController.enabled = true;
    }
    void ScaryEffect()
    {
        if (profile.TryGet<FilmGrain>(out grain))
        {
            grain.intensity.Override(0f);
        }
    }
}
