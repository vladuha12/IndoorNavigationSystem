using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

/// <summary>
/// Class <c>SetNavigationTarget</c> purpose is to calculate navigation path using NavMesh
/// as well as handling line renderer to draw a visual guide line.
/// </summary>

public class SetNavigationTarget : MonoBehaviour
{
    // Variables declarations
    [SerializeField]
    private TMP_Dropdown navigationTargetDropDown;

    [SerializeField]
    private List<Target> navigationTargetObjects = new List<Target>();

    private NavMeshPath path;
    private LineRenderer line;
    private Vector3 targetPosition = Vector3.zero;
    
    private bool lineToogle = false;

    // Start is called before the first frame update
    void Start()
    {
        path = new NavMeshPath();
        line = transform.GetComponent<LineRenderer>();
        line.enabled = lineToogle;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if line toogle button is pushed and draws guiding line
        if (lineToogle && targetPosition != Vector3.zero)
        {
            NavMesh.CalculatePath(transform.position, targetPosition, NavMesh.AllAreas, path);
            line.positionCount = path.corners.Length;
            line.SetPositions(path.corners);
        }
    }

    // Sets current navigation target relied on dropdown menu
    public void SetCurrentNavigationTarget (int selectedValue)
    {
        targetPosition = Vector3.zero;
        string selectedText = navigationTargetDropDown.options[selectedValue].text;
        Target currentTarget = navigationTargetObjects.Find(x => x.Name.ToLower().Equals(selectedText.ToLower()));
        if (currentTarget != null)
        {
            targetPosition = currentTarget.PositionObject.transform.position;
        }
    }

    // Hides/unhides guidance line
    public void ToogleVisibility()
    {
        lineToogle = !lineToogle;
        line.enabled = lineToogle;
    }
}
