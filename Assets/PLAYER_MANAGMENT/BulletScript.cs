using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public GameObject PrefabFire;
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        GameObject fire = Instantiate(PrefabFire);
        fire.transform.position = transform.position;
        Destroy(fire, 1);
        if (collision.gameObject.tag == "Wolf")
        {
            collision.gameObject.GetComponent<WolfLogic>().health--;
        }
    }
}
