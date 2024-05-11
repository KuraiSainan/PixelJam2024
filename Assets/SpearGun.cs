using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearGun : MonoBehaviour
{
    Player player;
    public int damage;
    [SerializeField] GameObject ammo;
    [SerializeField] Transform gunTips;
    ObjectPool pool;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        pool = ObjectPool.instance;
       pool.CreatePool(ammo, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        GameObject game = pool.GetPooledObject(ammo);
        game.transform.position = gunTips.position;
        game.GetComponent<Ammo>().InitialiseProj(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position,gunTips);
        game.SetActive(true);

    }
}
