using UnityEngine;

public class FollowMob : MonoBehaviour
{
    public Transform MobTransform; // 몹의 Transform을 에디터에서 할당

    void Update()
    {
            if (MobTransform != null)
        {
            // 현재 오브젝트의 위치를 몹의 위치로 따라가되, Y축만 +0.8
            Vector3 targetPosition = new Vector3(
                MobTransform.position.x,
                MobTransform.position.y + 0.8f,
                MobTransform.position.z
            );

            transform.position = targetPosition;
        }
    }
}