using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano_Note : MonoBehaviour
{
    private const float NOTE_SPEED = 3.0f;

    private Transform noteTrm = null;
    private float curNoteSpeed = 3.0f;

    private void Awake()
    {
        InitValue();   
    }

    private void Update()
    {
        
    }

    public void InitValue()
    {
        noteTrm = GetComponent<Transform>();

        Piano_Management.destroyNote_Act += this.DestroyNote;
    }

    private void MoveNote()
    {
        noteTrm.position += Vector3.down * Time.deltaTime * curNoteSpeed;
    }

    private void DestroyNote()
    {
        Destroy(noteTrm.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Piano_Management.destroyNote_Act?.Invoke();
        Piano_Management.destroyNote_Act -= this.DestroyNote;
    }
}
