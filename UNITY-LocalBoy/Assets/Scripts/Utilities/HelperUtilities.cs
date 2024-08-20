using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperUtilities 
{
    public static Camera mainCamera;

    /// <summary>
    /// Get the mouse world position.
    /// </summary>
    public static Vector3 GetMouseWorldPosition()
    {
        if (mainCamera == null) mainCamera = Camera.main;

        Vector3 mouseScreenPosition = Input.mousePosition;

        // Clamp mouse position to screen size
        mouseScreenPosition.x = Mathf.Clamp(mouseScreenPosition.x, 0f, Screen.width);
        mouseScreenPosition.y = Mathf.Clamp(mouseScreenPosition.y, 0f, Screen.height);

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        worldPosition.z = 0f;

        return worldPosition;

    }

    //ESTO DE ACA ARRIBA LO PUSE PARA PODER HACER LA PRUEBA CON EL ASTARTEST



    ///<summary>
    ///Empty String Debug Check
    /// </summary>
    /// 


    public static bool ValidateCheckEmptySTring(Object thisObject, string fieldName, string stringToCheck)
    {
        if (stringToCheck == "")
        {
            Debug.Log(fieldName + " is empty and must contain a value in object " + thisObject.name.ToString());
            return true;
        }
        return false;
    }

    ///<summary>
    ///List empty or contains null value check - returns true if there is an error
    /// </summary>
     
    public static bool ValidateCheckEnumerableValues(Object thisObject, string fieldName, IEnumerable enumerableObjectsToCheck) 
    {
        bool error = false;
        int count = 0;

        if (enumerableObjectsToCheck == null)
        {
            Debug.LogError(fieldName + " is null in object " + thisObject.name.ToString());
            return true;
        }

        foreach (var item in enumerableObjectsToCheck)
        {

            if (item == null)
            {
                Debug.LogError(fieldName + " has null values in object " + thisObject.name.ToString());
                error = true;

            }
            else
            {
                count++;
            }
        }
        if (count == 0)
        {
            Debug.LogError(fieldName + " has no values in object " + thisObject.name.ToString());
            error = true;
        }
        return error;
    }

    /// <summary>
    /// null value debug check
    /// </summary>
    public static bool ValidateCheckNullValue(Object thisObject, string fieldName, UnityEngine.Object objectToCheck)
    {
        if (objectToCheck == null)
        {
            Debug.Log(fieldName + " is null and must contain a value in object " + thisObject.name.ToString());
            return true;
        }
        return false;
    }
}
