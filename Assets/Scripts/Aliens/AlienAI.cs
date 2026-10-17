using UnityEngine;

public class AlienAI : MonoBehaviour
{
    public AlienInvisibleController controller;
    public AlienInteraction interaction;
    public Transform player;

    public float actionCooldown = 5f;
    private float timer;

    public bool isNight = false;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= actionCooldown)
        {
            DecideAction();
            timer = 0f;
            actionCooldown = isNight ? Random.Range(2f, 4f) : Random.Range(6f, 12f);
        }
    }

    void DecideAction()
    {
        // Prioritize recent player interactions
        // Move closer, manipulate object, make sound, project light
        if (isNight)
            AggressiveBehavior();
        else
            SubtleBehavior();
    }

    void SubtleBehavior()
    {
        // Day: rare interactions
    }

    void AggressiveBehavior()
    {
        // Night: more frequent
    }

    public void SetNightMode(bool night)
    {
        isNight = night;
    }
}
