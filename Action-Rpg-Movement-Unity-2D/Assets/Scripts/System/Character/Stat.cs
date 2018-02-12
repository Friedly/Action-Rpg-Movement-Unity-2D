using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Statsystem
{
    //Hardcoded Stattypes - find a better solution ... maybe scriptable objects?
    public enum StatType
    {
        health,
        mana
    }

    //Min and Max Values for Health
    //Timed buffs/debuffs

    [System.Serializable]
    public class Stat
    {
        [SerializeField]
        protected float baseValue;
        [SerializeField]
        protected StatType type;
        protected float modifiedValue;
        protected bool recalculate;

        protected readonly List<StatModifier> statModifiers;

        public Stat()
        {
            recalculate = true;
            statModifiers = new List<StatModifier>();
        }
        public Stat(float baseValue) : this ()
        {
            this.baseValue = baseValue;
        }

        public virtual void addModifier(StatModifier statModifier)
        {
            statModifiers.Add(statModifier);
            statModifiers.Sort(compareOrder);
            recalculate = true;
        }

        public virtual bool removeModifier(StatModifier statModifier)
        {
            if(statModifiers.Remove(statModifier))
            {
                recalculate = true;
                return true;
            }
            return false;
        }
        public virtual bool removeModifierFromSource(object source)
        {
            bool removed = false;

           for(int index=statModifiers.Count - 1; index >= 0;  index-- )
           {
                if(statModifiers[index].Source == source)
                {
                    statModifiers.RemoveAt(index);
                    recalculate = true;
                    removed = true;
                }
           }

            return removed;
        }

        public virtual float BaseValue
        {
            set
            {
                baseValue = value;
                recalculate = true;
            }

            get
            {
                return baseValue;
            }
        }
        public virtual float ModifiedValue
        {
            get
            {
                if(recalculate == true)
                {
                    calculateModifiedValue();
                }

                return modifiedValue;
            }
        }
        public virtual ReadOnlyCollection<StatModifier> StatModifiers
        {
            get
            {
                return statModifiers.AsReadOnly();
            }
        }

        protected virtual int compareOrder(StatModifier a, StatModifier b)
        {
            if(a.Order < b.Order)
            {
                return -1;
            }
            else if(a.Order > b.Order)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        protected virtual void calculateModifiedValue()
        {
            float finalValue = BaseValue;
            float sumPercentAdditive = 0;

            for(int index = 0; index < statModifiers.Count; index++)
            {
                StatModifier modifier = statModifiers[index];

                if(modifier.Type == StatModifierType.Flat)
                {
                    finalValue = finalValue + modifier.Value;
                }
                else if(modifier.Type == StatModifierType.PercentMultiplicative)
                {
                    finalValue = finalValue * (1 + modifier.Value);
                }
                else if(modifier.Type == StatModifierType.PercentAdditive)
                {
                    sumPercentAdditive = sumPercentAdditive + modifier.Value;

                    if(index >= statModifiers.Count - 1 || statModifiers[index + 1].Type != StatModifierType.PercentAdditive)
                    {
                        finalValue = finalValue * (1 + sumPercentAdditive);
                        sumPercentAdditive = 0;
                    }
                }
                else
                {
                    Debug.Log("Stat::calculateModifiedValue - StatModifierType '" + modifier.Type.ToString() + "' not supported");
                }
            }

            modifiedValue = Mathf.Round(finalValue);
            recalculate = false;
        }
    }
}
