using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSHMN_Background : MonoBehaviour {

    public BackgroundPart[] backgroundParts;

    private bool first = true;

    void Start ()
    {
        backgroundParts = transform.GetComponentsInChildren<BackgroundPart>();

        foreach (BackgroundPart part in backgroundParts)
        {
            part.OnPositionReached.AddListener(HandleBackgroundRan);
        }
	}
	
	
	void Update ()
    {
		
	}

    private void HandleBackgroundRan(Transform bpTransform, float tileSize)
    {
        bpTransform.position = new Vector3(0, tileSize);
    }


}


