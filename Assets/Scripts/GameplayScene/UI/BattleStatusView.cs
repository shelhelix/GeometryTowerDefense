using TMPro;
using TriInspector;
using UnityEngine;
using VContainer;

namespace Game.GameplayScene {
	public class BattleStatusView : MonoBehaviour {
		[Required] public TMP_Text      Text;

		BattleManager _battleManager;

		bool _isInited;
		
		[Inject]
		public void Init(BattleManager battleManager) {
			_battleManager = battleManager;
			_isInited      = true;
		}
		
		public void Update() {
			if ( !_isInited ) {
				return;
			}
			if ( _battleManager.CurrentState == State.Preparation ) {
				Text.text = $"{_battleManager.CurrentState}. Left time: {(int)_battleManager.LeftPreparationTime} sec";
			}
			if ( _battleManager.CurrentState == State.Battle ) {
				Text.text = $"{_battleManager.CurrentState}. Wave {_battleManager.CurrentWaveIndex + 1} / {_battleManager.BattleWaves.Count}";
			}
			if ( (_battleManager.CurrentState == State.Won) || (_battleManager.CurrentState == State.Lost) ) {
				Text.text = $"Battle finished. Result: {_battleManager.CurrentState}";
			}
		}
	}
}