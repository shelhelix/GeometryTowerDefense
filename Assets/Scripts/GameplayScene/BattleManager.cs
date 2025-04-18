﻿using System.Collections.Generic;
using TriInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace Game.GameplayScene {
	public enum State {
		Preparation,
		Battle,
		Lost,
		Won
	}
	
	public class BattleManager : MonoBehaviour {
		[Required] public DefeatScreen DefeatScreen;
		[Required] public DefeatScreen WinScreen;
		
		[Required] public List<PlayableDirector> BattleWaves;

		public float PreparationTimeSec;

		[Required] public PlayerTower PlayerTower;
		[ReadOnly] public float       LeftPreparationTime;
		[ReadOnly] public State       CurrentState;

		[ReadOnly] public int CurrentWaveIndex;

		int _activeMonstersCount;

		bool _isSpawnFinished;

		public bool IsPlaying => (CurrentState != State.Lost) && (CurrentState != State.Won);
		
		PlayableDirector CurrentDirector => BattleWaves[CurrentWaveIndex];
		
		public void RegisterMonsterSpawn() {
			_activeMonstersCount++;
		}

		public void RegisterMonsterDeath() {
			_activeMonstersCount--;
		}

		void Start() {
			StartPreparation(false);
		}

		protected void Update() {
			if ( !IsPlaying ) {
				return;
			}
			if ( PlayerTower.CurrentLives <= 0 ) {
				LoseGame();
			}
			if ( CurrentState == State.Preparation ) {
				ProcessPreparation();
			}
			if ( CurrentState == State.Battle ) {
				ProcessBattle();
			}
		}

		void ProcessPreparation() {
			LeftPreparationTime -= Time.deltaTime;
			if ( LeftPreparationTime <= 0 ) {
				StartBattle();
			}
		}

		void LoseGame() {
			CurrentState   = State.Lost;
			Time.timeScale = 1;
			CurrentDirector.Stop();
			DefeatScreen.Show();
		}

		void ProcessBattle() {
			if ( (_activeMonstersCount != 0) || !_isSpawnFinished ) {
				return;
			}
			if ((CurrentWaveIndex + 1) < BattleWaves.Count) {
				StartPreparation();
			} else {
				WinGame();
			}
		}

		void WinGame() {
			CurrentState = State.Won;
			Time.timeScale = 1;
			CurrentDirector.Stop();
			WinScreen.Show();
		}
		
		void StartBattle() {
			CurrentState = State.Battle;
			CurrentDirector.Play();
		}

		void StartPreparation(bool increaseWaveIndex = true) {
			if ( BattleWaves.Count == 0 ) {
				return;
			}
			CurrentState           =  State.Preparation;
			LeftPreparationTime     =  PreparationTimeSec;
			CurrentDirector.stopped -= OnTimelineFinished;
			if ( increaseWaveIndex ) {
				CurrentWaveIndex++;
			}
			_activeMonstersCount    =  0;
			_isSpawnFinished        =  false;
			CurrentDirector.stopped += OnTimelineFinished;
		}

		void OnTimelineFinished(PlayableDirector director) {
			_isSpawnFinished = true;
		}
	}
}