using UnityEngine;

namespace Infrastructure.Data.SceneDto
{
    public class GameSceneDto
    {
        public GameSceneDto(Transform characterPrefab)
        {
            CharacterPrefab = characterPrefab;
        }

        public Transform CharacterPrefab { get; }
    }
}