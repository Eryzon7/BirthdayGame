using UnityEngine;

public class CakeStackResult : MonoBehaviour
{
    
    [SerializeField] private GameObject[] cakeLayers;
    [SerializeField] private GameObject Cake;

    private void Start()
    {
        if (GameManager.Instance.cakeGameCompleted)
        {
            foreach (var cakeLayer in cakeLayers)
            {
                cakeLayer.gameObject.SetActive(true);
            }
            RebuildCake();
        }
    }

    private void RebuildCake()
    {
        float cameraWidth = Camera.main.orthographicSize * 2f * Camera.main.aspect;
        float centerX = Camera.main.transform.position.x;

        for (int i = 1; i < GameManager.Instance.cakeLayers.Count; i++)
        {
            CakeLayerData data = GameManager.Instance.cakeLayers[i];

            float worldOffsetX = data.normalizedOffsetX * (cameraWidth / 2f);
            float x = centerX + worldOffsetX;

            Vector3 currentPos = cakeLayers[i].transform.position;

            cakeLayers[i].transform.position = new Vector3(
                (x / 4) + 1.96f,
                currentPos.y, // keep existing height
                currentPos.z
            );
        }
    }
}
