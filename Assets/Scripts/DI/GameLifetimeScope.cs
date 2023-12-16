using Bullets;
using Character;
using Enemy;
using Game;
using GameInput;
using Level;
using LifecycleEvents;
using Pool;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class GameLifetimeScope : LifetimeScope
    {
        [Header("Player")]
        [SerializeField]
        private GameObject _character;

        [SerializeField]
        private BulletConfig _characterBulletConfig;

        [Header("Level")]
        [SerializeField]
        private LevelBounds _levelBounds;

        [SerializeField]
        private Transform _worldRoot;

        [Header("Enemies")]
        [SerializeField]
        private Transform _enemiesPoolRoot;

        [SerializeField]
        private BulletConfig _enemyBulletConfig;

        [SerializeField]
        private EnemyPositions _enemyPositions;

        [SerializeField]
        private GameObject _enemyPrefab;

        [Header("Bullets")]
        [SerializeField]
        private Transform _bulletsPoolRoot;

        [SerializeField]
        private Bullet _bulletPrefab;

        [Header("UI")]
        [SerializeField]
        private Text _countdownText;

        [SerializeField]
        private GameObject _gameOverPanel;

        [SerializeField]
        private Button _pauseButton;

        [SerializeField]
        private Button _resumeButton;

        [SerializeField]
        private Button _startGameButton;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterCharacter(builder);
            RegisterEnemySystem(builder);
            RegisterBulletSystem(builder);
            RegisterOther(builder);
        }

        private void RegisterBulletSystem(IContainerBuilder builder)
        {
            builder.Register<PrefabPool<Bullet>>(Lifetime.Singleton)
                .WithParameter(_bulletPrefab).WithParameter(_bulletsPoolRoot);
            builder.Register<BulletSystem>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<BulletSpawner>(Lifetime.Singleton).WithParameter(_worldRoot);
        }

        private void RegisterCharacter(IContainerBuilder builder)
        {
            builder.Register<CharacterMoveController>(Lifetime.Singleton).AsImplementedInterfaces()
                .WithParameter(_character);
            builder.Register<CharacterShootController>(Lifetime.Singleton).AsImplementedInterfaces()
                .WithParameter(_characterBulletConfig).WithParameter(_character);
        }

        private void RegisterEnemySystem(IContainerBuilder builder)
        {
            builder.RegisterInstance<EnemyPositions>(_enemyPositions);

            builder.Register<EnemyManager>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EnemyShooter>(Lifetime.Singleton).WithParameter(_enemyBulletConfig);
            builder.Register<EnemySpawner>(Lifetime.Singleton).WithParameter(_character).WithParameter(_worldRoot);
            builder.Register<GameObjectPool>(Lifetime.Singleton).WithParameter(_enemyPrefab).WithParameter(_enemiesPoolRoot);
        }

        private void RegisterOther(IContainerBuilder builder)
        {
            builder.RegisterInstance<LevelBounds>(_levelBounds);

            builder.Register<InputManager>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.RegisterEntryPoint<LifecycleManager>().AsSelf();

            builder.RegisterEntryPoint<CharacterDeathObserver>().WithParameter(_character);
            builder.RegisterEntryPoint<StartButtonObserver>().WithParameter(_startGameButton);
            builder.RegisterEntryPoint<PauseResumeButtonsObserver>().WithParameter((_pauseButton, _resumeButton));

            builder.Register<CountdownGameStarter>(Lifetime.Singleton).WithParameter(_countdownText);
            builder.Register<GameOverListener>(Lifetime.Singleton).AsImplementedInterfaces()
                .WithParameter(_gameOverPanel);
            builder.Register<PauseResumeButtonStateController>(Lifetime.Singleton).AsImplementedInterfaces()
                .WithParameter((_pauseButton, _resumeButton));
        }
    }
}
