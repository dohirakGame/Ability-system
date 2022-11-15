using System.Collections.Generic;
using Materials.Scripts.Abilities;
using UnityEngine;
using UnityEngine.UI;

namespace Materials.Scripts
{
    public class AbilitiesInteraction : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private GameObject abilityButtonsUI;
        private bool _isVisible;
        
        [Header("Ability Scripts")]
        [SerializeField] private List<Ability> abilitiesList;
        
        [Header("Ability Components")]
        [SerializeField] private int abilityActiveID;
        [SerializeField] private int abilityPassiveID;
        [SerializeField] private bool isActiveCast;
        [SerializeField] private bool isPassiveCast;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private GameObject abilityPoint;
        private Ray _pointForAbility;
        private RaycastHit _abilityHit;

        [Header("Pressed Keys")]
        public KeyCode keyAvailableAbilities = KeyCode.R;
        public KeyCode keyFirstAbility = KeyCode.Alpha1;
        public KeyCode keySecondAbility = KeyCode.Alpha2;
        public KeyCode keyThirdAbility = KeyCode.Alpha3;

        [Header("Ability Fills")] 
        [SerializeField] private List<Image> fillsList;
        [Range(0f,1f)] private float[] fills;
        [SerializeField] private float[] _reloadAbilityTime;
        [SerializeField] private string[] _abilityAciton;

        private void Start()
        {
            fills = new float[fillsList.Count];
            _reloadAbilityTime = new float[fillsList.Count];
            _abilityAciton = new string[fillsList.Count];
            HideAbilities();
            SetReloadAbilityTime();
        }

        private void Update()
        { 
            if (Input.GetKeyDown(keyAvailableAbilities))
            {
                if (!_isVisible) ShowAbilities();
                else
                {
                    EndCast(_abilityAciton[abilityActiveID]);
                    HideAbilities();
                }
            }
            
            if (_isVisible) CheckingVisibleAbilities();
            
            if (isActiveCast) CastActiveAbility();
            if (isPassiveCast) CastPassiveAbility();

            ReloadAbility();
        }

        private void ShowAbilities()
        {
            _isVisible = true;
            abilityButtonsUI.SetActive(true);
        }

        private void HideAbilities()
        {
            _isVisible = false;
            abilityButtonsUI.SetActive(false);
        }

        private void SetReloadAbilityTime()
        {
            _reloadAbilityTime[0] = GetComponent<FirstAbility>().abilityParameters.ReloadTime;
            _reloadAbilityTime[1] = GetComponent<SecondAbility>().abilityParameters.ReloadTime;
            _reloadAbilityTime[2] = GetComponent<ThirdAbility>().abilityParameters.ReloadTime;

            _abilityAciton[0] = GetComponent<FirstAbility>().abilityParameters.ActionAbility.ToString();
            _abilityAciton[1] = GetComponent<SecondAbility>().abilityParameters.ActionAbility.ToString();
            _abilityAciton[2] = GetComponent<ThirdAbility>().abilityParameters.ActionAbility.ToString();
        }

        private void CheckingVisibleAbilities()
        {
            if (Input.GetKeyDown(keyFirstAbility))
            {
                if (CheckActiveAction(0))
                {
                    abilityActiveID = 0;
                    if (fills[abilityActiveID] == 0f) isActiveCast = true;
                }
                else
                {
                    abilityPassiveID = 0;
                    if (fills[abilityPassiveID] == 0) isPassiveCast = true;
                }
            }

            if (Input.GetKeyDown(keySecondAbility))
            {
                if (CheckActiveAction(1))
                {
                    abilityActiveID = 1;
                    if (fills[abilityActiveID] == 0f) isActiveCast = true;
                }
                else
                {
                    abilityPassiveID = 1;
                    if (fills[abilityPassiveID] == 0) isPassiveCast = true;
                }
            }

            if (Input.GetKeyDown(keyThirdAbility))
            {
                if (CheckActiveAction(2))
                {
                    abilityActiveID = 2;
                    if (fills[abilityActiveID] == 0f) isActiveCast = true;
                }
                else
                {
                    abilityPassiveID = 2;
                    if (fills[abilityPassiveID] == 0) isPassiveCast = true;
                }
            }
        }

        private void CastActiveAbility()
        {
            if (PointMousePosition("Ground"))
            {
                abilityPoint.SetActive(true);
                abilityPoint.transform.position =
                    new Vector3(_abilityHit.point.x, _abilityHit.point.y, _abilityHit.point.z);
            }

            if (Input.GetMouseButton(0))
            {
                EndCast(_abilityAciton[abilityActiveID]);
                if (abilityActiveID == 0)
                    abilitiesList[abilityActiveID].AbilityUse(_abilityHit.point, gameObject.transform.position);
                fills[abilityActiveID] = 1;
            }

            if (Input.GetMouseButton(1))
            {
                EndCast(_abilityAciton[abilityActiveID]);
            }
        }

        private void CastPassiveAbility()
        {
            abilitiesList[abilityPassiveID].AbilityUse();
            fills[abilityPassiveID] = 1f;
            EndCast(_abilityAciton[abilityPassiveID]);
        }

        private bool CheckActiveAction(int id)
        {
            if (_abilityAciton[id] == "Active")
            {
                return true;
            }
            return false;
        }
        
        private void EndCast(string action)
        {
            if (action == "Active")
            {
                isActiveCast = false;
                abilityPoint.SetActive(false);
            }
            else isPassiveCast = false;
        }
        
        private void ReloadAbility()
        {
            for (int i = 0; i < fills.Length; i++)
            {
                if (fills[i] > 0f)
                {
                    fills[i] -= Time.deltaTime / _reloadAbilityTime[i];
                }
                else fills[i] = 0f;
                if (abilityButtonsUI.activeInHierarchy)
                {
                    fillsList[i].fillAmount = fills[i];
                }
            }
        }

        private bool PointMousePosition(string pointTag)
        {
            int _layermask = 1 << 6;
            _layermask = ~_layermask;
            _pointForAbility = playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_pointForAbility,out _abilityHit, 100f,_layermask) && _abilityHit.collider.CompareTag(pointTag))
            {
                return true;
            }
            return false;
        }
    }
}
