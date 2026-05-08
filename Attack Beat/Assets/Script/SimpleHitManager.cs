using UnityEngine;

public class SimpleHitManager : MonoBehaviour
{
    public KeyCode hitKey = KeyCode.Space;
    public float judgeX = -6f;
    public float judgeRange = 0.7f;

    void Update()
    {
        if (Input.GetKeyDown(hitKey))
        {
            TryHit();
        }
    }

    void TryHit()
    {
        SimpleNote[] notes = FindObjectsOfType<SimpleNote>();

        SimpleNote nearestNote = null;
        float nearestDistance = float.MaxValue;

        foreach (SimpleNote note in notes)
        {
            if (note == null) continue;

            float dist = Mathf.Abs(note.transform.position.x - judgeX);

            if (dist < nearestDistance)
            {
                nearestDistance = dist;
                nearestNote = note;
            }
        }

        if (nearestNote != null && nearestDistance <= judgeRange)
        {
            nearestNote.Hit();
            Debug.Log("Hit!");
        }
        else
        {
            Debug.Log("Miss");
        }
    }
}