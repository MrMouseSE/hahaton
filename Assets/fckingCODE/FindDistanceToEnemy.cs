using System.Collections.Generic;
using UnityEngine;

namespace fckingCODE
{
    public class FindDistanceToEnemy : MonoBehaviour
    {
        public static GameObject FindNearestEnemy(Transform thisTransform, List<GameObject> listGO)
        {
            float listCount = listGO.Count;
            GameObject nearestGO = null;

            float distance = Vector3.Distance(thisTransform.position, listGO[0].transform.position);

            for (int i = 0; i < listCount; i++)
            {
                var go = listGO[i];
                if (go == null) continue;
                
                if (Vector3.Distance(thisTransform.position, go.transform.position)<distance)
                {
                    distance = Vector3.Distance(thisTransform.position, go.transform.position);
                    nearestGO = go;
                }
            }

            return nearestGO;
        }
    }
}