namespace Statsystem
{
    public enum StatModifierType
    {
        Flat,
        PercentAdditive,
        PercentMultiplicative
    }

    public class StatModifier
    {
        private readonly float value;
        private readonly StatModifierType type;
        private readonly int order;
        private readonly object source;

        public StatModifier(float value, StatModifierType type, int order, object source)
        {
            this.value = value;
            this.type = type;
            this.order = order;
            this.source = source;
        }
        public StatModifier(float value, StatModifierType type, int order) : this(value, type, order, null)
        {

        }
        public StatModifier(float value, StatModifierType type, object source) : this(value, type, (int)type, source)
        {

        }
        public StatModifier(float value, StatModifierType type) : this(value, type, (int)type, null)
        {
        }

        public object Source
        {
            get
            {
                return source;
            }
        }
        public int Order
        {
            get
            {
                return order;
            }
        }
        public float Value
        {
            get
            {
                return value;
            }
        }
        public StatModifierType Type
        {
            get
            {
                return type;
            }
        }
    }
}
