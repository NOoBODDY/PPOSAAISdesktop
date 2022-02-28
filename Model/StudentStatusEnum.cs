using System.ComponentModel;

namespace ClientDB.Model
{
    public enum StudentStatusEnum
    {
        [Description("Состоит")]
        CONSISTS,
        [Description("Вышел")]
        LEFT,
        [Description("Отчислен")]
        EXPELLED,
        [Description("Выпустился")]
        GRADUATED
    }
}
