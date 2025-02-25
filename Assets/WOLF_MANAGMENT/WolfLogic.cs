using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Ursaanimation.CubicFarmAnimals;

public class WolfLogic : MonoBehaviour
{
    public NavMeshAgent agent;
    public SheepLogic targetSheep;
    public int health;
    public float SinceLastAttack;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating(nameof(FindClosestSheep), 0, 1f);
    }

    void Update()
    {
        if (targetSheep != null)
        {
            agent.SetDestination(targetSheep.transform.position);
        }

        if (health <= 0)
        {
            GameManager.Instance.WolfDied(this);
            Destroy(gameObject);
        }
        SinceLastAttack += Time.deltaTime;
    }

    private void FindClosestSheep()
    {
        float minDistance = Mathf.Infinity;
        SheepLogic closestSheep = null;

        foreach (var sheep in GameManager.Instance.sheepList)
        {
            float distance = Vector3.Distance(transform.position, sheep.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestSheep = sheep;
            }
        }

        targetSheep = closestSheep;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Sheep") && SinceLastAttack > 1)
        {
            SheepLogic sheep = other.GetComponent<SheepLogic>();
            if (sheep != null)
            {
                other.gameObject.GetComponent<SheepLogic>().CurrentHealth--;
                SinceLastAttack = 0;
            }
        }
    }
}
