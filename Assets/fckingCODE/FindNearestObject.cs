using System.Collections.Generic;
using UnityEngine;

namespace fckingCODE
{
    public static class FindNearest
    {
        public static GameObject FindNearestObject(Transform thisTransform, List<GameObject> listGO)
        {
            GameObject nearestGO = listGO[0];

            float distance = Vector3.Distance(thisTransform.position, listGO[0].transform.position);

            foreach (var go in listGO)
            { 
                if (Vector3.Distance(thisTransform.position, go.transform.position) < distance)
                {
                    distance = Vector3.Distance(thisTransform.position, go.transform.position);
                    nearestGO = go;
                }
            }
            return nearestGO;
        }
    }
}