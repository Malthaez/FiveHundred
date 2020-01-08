using UnityEngine;

namespace Assets.Scripts.Game.Factories
{
    public static class CardPositionFactory
    {
        const float _depthConstant = 0.15f;
        const float _spacingConstant = 0.5f;

        public static Vector3 SpacingModifier => new Vector3(_spacingConstant, _spacingConstant, _depthConstant);

        public static Vector3 StartPositionBase(int count) => new Vector3(0.5f, 0.5f, 0f) * count;

        public static Vector3 CardPositionBase(int index) => new Vector3(1f, 1f, -1f) * index;
    }
}
