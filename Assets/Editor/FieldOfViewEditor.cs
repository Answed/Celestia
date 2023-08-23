using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyFieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyFieldOfView fov = (EnemyFieldOfView)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewingDistance);

        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.fieldOfView / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.fieldOfView / 2);

        Handles.color += Color.yellow;
        Handles.DrawLine(fov.transform.position,fov.transform.position + viewAngle01 * fov.viewingDistance);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.viewingDistance);

        if(fov.playerFound)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.player.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angeInDegrees)
    {
        angeInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angeInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angeInDegrees * Mathf.Deg2Rad));
    }
}
