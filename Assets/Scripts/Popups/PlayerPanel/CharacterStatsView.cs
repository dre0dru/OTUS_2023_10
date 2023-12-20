using System.Collections.Generic;
using Presenters.PlayerPanel;
using UnityEngine;

namespace Popups.PlayerPanel
{
    public class CharacterStatsView : MonoBehaviour
    {
        [SerializeField]
        private CharacterStatView _statViewPrefab;

        [SerializeField]
        private Transform _content;

        private readonly Dictionary<ICharacterStatPresenter, CharacterStatView> _views = new();

        private ICharacterStatsPresenter _characterStatsPresenter;

        private void OnDestroy()
        {
            Unsubscribe();
        }

        public void Initialize(ICharacterStatsPresenter presenter)
        {
            _characterStatsPresenter = presenter;

            CreateViews();

            Subscribe();
        }

        public void ReleasePresenters()
        {
            ClearViews();
            Unsubscribe();
            _characterStatsPresenter.Dispose();
        }

        private void ClearViews()
        {
            foreach (var view in _views.Values)
            {
                view.ReleasePresenters();
                DestroyStatView(view);
            }

            _views.Clear();
        }

        private void Unsubscribe()
        {
            _characterStatsPresenter.OnStatAdded += OnStatAdded;
            _characterStatsPresenter.OnStatRemoved += OnStatRemoved;
        }

        private void Subscribe()
        {
            _characterStatsPresenter.OnStatAdded += OnStatAdded;
            _characterStatsPresenter.OnStatRemoved += OnStatRemoved;
        }

        private void OnStatRemoved(ICharacterStatPresenter presenter)
        {
            if (_views.Remove(presenter, out var view))
            {
                DestroyStatView(view);
            }
        }

        private void OnStatAdded(ICharacterStatPresenter presenter)
        {
            InstantiateStatView(presenter);
        }

        private void CreateViews()
        {
            foreach (var presenter in _characterStatsPresenter.StatPresenters)
            {
                InstantiateStatView(presenter);
            }
        }

        private void InstantiateStatView(ICharacterStatPresenter presenter)
        {
            var view = Instantiate(_statViewPrefab, _content);
            view.Initialize(presenter);

            _views.Add(presenter, view);
        }

        private void DestroyStatView(CharacterStatView view)
        {
            view.transform.SetParent(null);
            Destroy(view.gameObject);
        }
    }
}
