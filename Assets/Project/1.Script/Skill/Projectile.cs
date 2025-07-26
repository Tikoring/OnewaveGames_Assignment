using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    private float speed;
    private float range;
    private List<EffectEntry> effects;
    private SkillContext context;

    [SerializeField] private int maxCount;
    private int count;
    

    public void Initialize(float speed, float range, List<EffectEntry> effects, SkillContext context)
    {
        count = 0;
        this.speed = speed;
        this.range = range;
        this.effects = effects;
        this.context = context;
    }

    private void FixedUpdate()
    {
        Vector3 dest = context.castedPosition + context.direction * range;
        if (Vector3.Distance(transform.position, dest) < 0.05f)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (count >= maxCount) { return; }
        Dummy dummy;
        if (dummy = other.GetComponentInParent<Dummy>())
        {
            count++;
            context.target = dummy.gameObject;
            foreach (EffectEntry effect in effects)
            {
                effect.EntryActive(context);
            }
            Destroy(gameObject);
        }

    }
}