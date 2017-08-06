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
                    CmdShoot();
                }
            }            
		    m_shootCooldown.x = m_shootCooldown.y;
		}
    }

	[Command]
	void CmdShoot() {
		GameObject newBullet = Instantiate(m_bullet, m_firePosition.position, Quaternion.identity);
		newBullet.GetComponent<Bullet>().m_bulletOwner = this.gameObject.GetComponent<PlayerHealth>();
		Vector2 velocity =  transform.forward * 10f;
		newBullet.GetComponent<Rigidbody2D>().velocity = velocity;
		newBullet.transform.GetChild(0).rotation = transform.rotation;
		NetworkServer.Spawn(newBullet);
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
