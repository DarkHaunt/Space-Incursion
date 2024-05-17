using System;
using UnityEngine;
using VContainer.Unity;
using WebSocketSharp;
using static Game.Code.Game.StaticData.Indents.GameIndents;

namespace Game.Code.Menu.View
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

            Debug.Log($"<color=white>Name - {_model.PlayerName} || Room - {_model.RoomName}</color>");
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