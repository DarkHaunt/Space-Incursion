using System.Collections.Generic;
using Game.Code.Game.StaticData;
using Game.Code.Extensions;
using VContainer.Unity;
using UnityEngine;
using System;

namespace Game.Code.Game.Services
{
    public class PlayerColorProvider : IInitializable, IDisposable
    {
        private readonly GameStaticDataProvider _staticDataProvider;
        
        private Stack<Color> _availableColors;

        public PlayerColorProvider(GameStaticDataProvider staticDataProvider)
        {
            _staticDataProvider = staticDataProvider;
        }

        public void Initialize()
        {
            var availableColors = _staticDataProvider.GameConfig.AvailableColor.ShuffleIE();
            _availableColors = new Stack<Color>(availableColors);
            
            Debug.Log($"<color=white>Inited</color>");
        }

        public void Dispose() =>
            _availableColors.Clear();

        public Color GetAvailableColor() =>
            _availableColors.Pop();
    }
}