using System;
using VContainer.Unity;

namespace Game.Code.Menu.View
{
    public class MenuPresenter : IStartable, IDisposable
    {
        private readonly MenuModel _model;
        private readonly MenuView _view;

        public MenuPresenter(MenuView view, MenuModel model)
        {
            _model = model;
            _view = view;
        }

        public void Start()
        {
            _view.NameField.onSubmit.AddListener(_model.SetPlayerName);
            _view.NameField.onSubmit.AddListener(_model.SetRoomName);
        }

        public void Dispose()
        {
            _view.NameField.onSubmit.RemoveListener(_model.SetPlayerName);
            _view.NameField.onSubmit.RemoveListener(_model.SetRoomName);
        }
    }
}