using UnityEngine;

public class FinalPursuit : MonoBehaviour
{
    public AlienEntry alien;
    public Transform player;
    public float pursuitSpeed = 3f;

    public bool isPursuing = false;

    public FinalSequenceController sequenceController;

    public void StartPursuit()
    {
        isPursuing = true;
        if (alien != null)
            alien.EnterHouse();
    }

    void Update()
    {
        if (!isPursuing || player == null || alien == null) return;

        alien.PursuePlayer(player);

        float dist = Vector3.Distance(alien.transform.position, player.position);
        if (dist < 1.2f)
        {
            isPursuing = false;
            sequenceController.OnPlayerCaught();
        }
    }
}
