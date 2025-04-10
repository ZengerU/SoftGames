using UnityEngine;

namespace AceOfShadows
{
    public class BottomCardFacade : MonoBehaviour
    {
        const float CardHeight = .012f;
        [SerializeField] MeshRenderer meshRenderer;
        Material _material;
        Vector3 _baseScale;


        void Awake()
        {
            _material = meshRenderer.material;
            _baseScale = transform.localScale;
        }

        public void RenderAmount(uint cardAmount)
        {
            _material.mainTextureScale = new Vector2(1, cardAmount);
            transform.localScale = new Vector3(_baseScale.x, CardHeight * cardAmount, _baseScale.z);
        }
    }
}