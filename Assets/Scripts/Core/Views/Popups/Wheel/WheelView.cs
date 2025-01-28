using Core.Data.Entities;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Infrastructure.Helpers.GameObjectHelper;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Views.Popups.Wheel
{
    public class WheelView : MonoBehaviour
    {
        private const int FakeRotationsAmount = 5;
        private const float RotateDuration = 3f;
        
        [SerializeField]
        private Button _spinButton;
        [SerializeField]
        private PrizeView _prizePrefab;
        [SerializeField]
        private RectTransform _wheelTransform;
        [SerializeField]
        private RectTransform _prizeWallPrefab;

        private IGameObjectHelper _gameObjectHelper;
        private float _segmentAngle;
        private PrizeEntity[] _cachedPrizes;

        [Inject]
        public void Construct(IGameObjectHelper gameObjectHelper)
        {
            _gameObjectHelper = gameObjectHelper;
        }
        
        public void SetPrizes(PrizeEntity[] prizeEntities)
        { 
            if(prizeEntities.Length == 0)
                return;

            _cachedPrizes = prizeEntities;
            _segmentAngle = 360f / prizeEntities.Length;
            
            SpawnPrizes(prizeEntities);
        }

        public async UniTask Rotate(int prizeIndex)
        {
            if (_cachedPrizes.Length == 0 || prizeIndex >= _cachedPrizes.Length)
                return;
            
            _spinButton.interactable = false;
            
            float prizeAngle = prizeIndex * _segmentAngle;
            float currentAngle = _wheelTransform.localEulerAngles.z;
            float angleDifference = Mathf.DeltaAngle(currentAngle, prizeAngle);
            float totalAngle = angleDifference + 360f * FakeRotationsAmount;

            await _wheelTransform
                .DORotate(new Vector3(0, 0, currentAngle + totalAngle), RotateDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutQuart)
                .AsyncWaitForCompletion();

            _spinButton.interactable = true;
        }
        
        private void SpawnPrizes(PrizeEntity[] prizeEntities)
        {
            const float prizeRadiusDelta = 0.65f;
            float prizeSpawnRadius = WheelRadius * prizeRadiusDelta;
            
            for (int i = 0; i < prizeEntities.Length; i++)
            {
                float angle = i * _segmentAngle;

                PrizeView prizeView = _gameObjectHelper.InstantiateObjectWithComponentInScene<PrizeView>(_prizePrefab.gameObject, _wheelTransform.transform);
                SetPrizeTransform(prizeView, angle, prizeSpawnRadius);

                if (prizeEntities.Length > 1)
                {
                    RectTransform prizeWall = _gameObjectHelper.InstantiateObjectWithComponentInScene<RectTransform>(_prizeWallPrefab.gameObject, _wheelTransform.transform);
                    SetPrizeWallTransform(prizeWall, angle + _segmentAngle / 2);
                }

                prizeView.SetData(prizeEntities[i]);
            }
        }

        private void SetPrizeWallTransform(RectTransform prizeWall, float wallAngle)
        {
            prizeWall.sizeDelta = new Vector2(prizeWall.sizeDelta.x, WheelRadius);
            prizeWall.transform.position = _wheelTransform.transform.position;
            
            prizeWall.transform.localRotation = Quaternion.Euler(0, 0, wallAngle);
        }

        private void SetPrizeTransform(PrizeView prizeView, float prizeAngle, float prizeSpawnRadius)
        {
            Vector3 position = Quaternion.Euler(0, 0, -prizeAngle) * Vector3.up * prizeSpawnRadius;
            prizeView.transform.localPosition = position;
            
            prizeView.transform.rotation = GetPrizeRotation(_wheelTransform.position, prizeView.transform.position);
        }

        private Quaternion GetPrizeRotation(Vector3 center, Vector3 positionOnWheelInWorld)
        {
            Vector3 direction = center - positionOnWheelInWorld;
            float prizeViewAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            return Quaternion.Euler(0, 0, prizeViewAngle + 180);
        }

        public Button SpinButton => _spinButton;
        private float WheelRadius => _wheelTransform.rect.width / 2;
    }
}