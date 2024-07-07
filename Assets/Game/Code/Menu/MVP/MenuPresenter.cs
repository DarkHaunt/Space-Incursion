using System;
using UnityEngine;
using VContainer.Unity;
using WebSocketSharp;

namespace Game.Code.Menu.MVP
{
    public class MenuPresenter : IInitializable, IDisposable
    {
        private readonly MenuModel _model;
        private readonly MenuView _view;

        public MenuPresenter(MenuView view, MenuModel model)
        {
            _model = model;
            _view = view;
        }

        public void Initialize() =>
            _view.StartButton.onClick.AddListener(CollectModelData);
        public void Dispose() =>
            _view.StartButton.onClick.RemoveListener(CollectModelData); 

        private void CollectModelData()
        {
            _model.SetPlayerName(GetPlayerViewName());
            _model.SetRoomName(GetPlayerViewRoom());
        }

        private string GetPlayerViewName()
        {
            var name = _view.NameField.text;
            return name.IsNullOrEmpty() ? _model.PlayerName : name;
        }   
        
        private string GetPlayerViewRoom()
        {
            var name = _view.RoomField.text;
            return name.IsNullOrEmpty() ? _model.RoomName : name;
        }
    }
}