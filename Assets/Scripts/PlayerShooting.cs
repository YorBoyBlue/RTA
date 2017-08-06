using UnityEngine;
using UnityEngine.Networking;

public class PlayerShooting : NetworkBehaviour {
    [SerializeField] Transform m_firePosition;
	[SerializeField] GameObject m_bullet;
	public Vector2 m_shootCooldown;
    [SerializeField] PlayerManager m_playerManager;
	[SerializeField] HUDManager m_hud;
    bool canShoot;

    void Start() {
        if (isLocalPlayer) {
            canShoot = true;
		}
    }

    void Update() {        
        if (!canShoot) {
            return;
		}
		if (m_shootCooldown.x > 0) {
			m_shootCooldown.x -= Time.deltaTime;
		} else {
			if(isLocalPlayer) {
				if (Input.GetAxis("Fire1") > 0) {
                    float speedFactor = (GetComponent<PlayerUpgrades>().ConsumeUpgrade(PickupType.WeaponBoost) ? 3f : 1f);
                    m_shootCooldown.x = m_shootCooldown.y / speedFactor;

                    int mode = (GetComponent<PlayerUpgrades>().ConsumeUpgrade(PickupType.WeaponDouble) ? 1 : 0);

					CmdShoot(50f * speedFactor, mode);
				}
			}
		}
    }

	[Command]
	void CmdShoot(float speed, int mode) {
        m_playerManager.AudioManager.PlayAudioClip(AudioClips.MAIN_WEAPON, 1);
        if (mode == 0) {
            GameObject newBullet = Instantiate(m_bullet, transform.position, Quaternion.identity);
            newBullet.GetComponent<Bullet>().m_bulletOwner = this.gameObject.GetComponent<PlayerHealth>();
            newBullet.GetComponent<Rigidbody2D>().velocity = transform.forward * speed;
            newBullet.transform.GetChild(0).rotation = transform.rotation;
            NetworkServer.Spawn(newBullet);
        } else if (mode == 1) {
            GameObject newBullet1 = Instantiate(m_bullet, transform.position + transform.right, Quaternion.identity);
            GameObject newBullet2 = Instantiate(m_bullet, transform.position - transform.right, Quaternion.identity);
            newBullet1.GetComponent<Bullet>().m_bulletOwner = this.gameObject.GetComponent<PlayerHealth>();
            newBullet1.GetComponent<Rigidbody2D>().velocity = transform.forward * speed;
            newBullet1.transform.GetChild(0).rotation = transform.rotation;
            NetworkServer.Spawn(newBullet1);
            newBullet2.GetComponent<Bullet>().m_bulletOwner = this.gameObject.GetComponent<PlayerHealth>();
            newBullet2.GetComponent<Rigidbody2D>().velocity = transform.forward * speed;
            newBullet2.transform.GetChild(0).rotation = transform.rotation;
            NetworkServer.Spawn(newBullet2);
        }
	}

    // [Command]
    // void CmdFireShot(Vector3 origin, Vector3 direction)
    // {
    //     RaycastHit hit;

    //     Ray ray = new Ray (origin, direction);
    //     Debug.DrawRay (ray.origin, ray.direction * 3f, Color.red, 1f);

    //     bool result = Physics.Raycast (ray, out hit, 50f);

    //     if (result) 
    //     {
    //         PlayerHealth enemy = hit.transform.GetComponent<PlayerHealth> ();

    //         if (enemy != null) {
    //             bool wasKillShot = enemy.TakeDamage();          
    //         }
    //     }

    //     RpcProcessShotEffects (result, hit.point);
    // }

    // [ClientRpc]
    // void RpcProcessShotEffects(bool playImpact, Vector3 point)
    // {
    //     shotEffects.PlayShotEffects ();

    //     if (playImpact)
    //         shotEffects.PlayImpactEffect (point);
    // }
}
