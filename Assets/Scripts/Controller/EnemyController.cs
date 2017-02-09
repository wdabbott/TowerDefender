using System.Linq;
using Assets;
using UnityEngine;

public class EnemyController : MonoBehaviour, ISelectable
{
    public int health;
    public AudioClip splat;
    public GameObject Loot;

    private PlayerState playerState;
    private bool isSelected = false;

    void Start()
    {
        var playerObject = GameObject.FindGameObjectsWithTag("Player").FirstOrDefault();

        playerState = playerObject.GetComponent<PlayerState>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Bullet")
        {
            var bullet = coll.gameObject;
            ApplyBulletEffects(bullet);
            Destroy(coll.gameObject);
        }
    }

    void Update()
    {
        if (health <= 0)
        {
            GetComponent<AudioSource>().clip = splat;
            GetComponent<AudioSource>().Play();

            Instantiate(Loot, transform.position, Quaternion.identity);

            playerState.TotalKills += 1;
            
            Destroy(this.gameObject);
        }
    }

    public void SetSelected(bool select)
    {
        isSelected = select;
    }

    void OnGUI()
    {
        if (isSelected)
        {
            Vector2 targetPos;
            targetPos = Camera.main.WorldToScreenPoint(transform.position);

            GUI.Box(new Rect(targetPos.x, Screen.height - targetPos.y, 50, 20), health + "/" + 10);
        }
    }

    private void ApplyBulletEffects(GameObject bullet)
    {
        var bulletState = bullet.GetComponent<BulletState>();

        health -= bulletState.Damage;
    }
}
